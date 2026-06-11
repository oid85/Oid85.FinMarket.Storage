using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.Metadata;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Common.Utils;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    /// <inheritdoc/>
    public class FundamentalParameterService(
        IFundamentalParameterRepository fundamentalParameterRepository) 
        : IFundamentalParameterService
    {
        /// <inheritdoc/>
        public async Task<CreateOrUpdateFundamentalParameterResponse> CreateOrUpdateFundamentalParameterAsync(CreateOrUpdateFundamentalParameterRequest request)
        {
            foreach (var item in request.FundamentalParameters)
                await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                    new FundamentalParameter
                    {
                        Ticker = item.Ticker,
                        Type = item.Type,
                        Period = item.Period,
                        Value = item.Value,
                        ExtData = item.ExtData
                    });

            return new();
        }

        /// <inheritdoc/>
        public async Task<DeleteFundamentalParameterResponse> DeleteFundamentalParameterAsync(DeleteFundamentalParameterRequest request)
        {
            foreach (var item in request.FundamentalParameters)
                await fundamentalParameterRepository.DeleteFundamentalParameterAsync(
                    new FundamentalParameter
                    {
                        Ticker = item.Ticker,
                        Type = item.Type,
                        Period = item.Period
                    });

            return new();
        }

        /// <inheritdoc/>
        public async Task<GetFundamentalParameterListResponse> GetFundamentalParameterListAsync(GetFundamentalParameterListRequest request)
        {
            var models = await fundamentalParameterRepository.GetFundamentalParametersAsync(request.Ticker, request.Periods);

            if (models is null)
                return new GetFundamentalParameterListResponse { FundamentalParameters = [] };

            var response = new GetFundamentalParameterListResponse
            {
                FundamentalParameters = models
                .Select(x => new GetFundamentalParameterListItemResponse
                {
                    Id = x.Id,
                    Ticker = x.Ticker,
                    Type = x.Type,
                    Period = x.Period,
                    Value = x.Value,
                    ExtData = x.ExtData
                })
                .ToList()
            };

            return response;
        }

        /// <inheritdoc/>
        public async Task FinanceMarkerImportAsync()
        {
            await FinanceMarkerImportAsync("2021", "Y");
            await FinanceMarkerImportAsync("2022", "Y");
            await FinanceMarkerImportAsync("2023", "Y");
            await FinanceMarkerImportAsync("2024", "Y");
            await FinanceMarkerImportAsync("2025", "Y");
            await FinanceMarkerImportAsync("2026", "YTM");
        }

        private async Task FinanceMarkerImportAsync(string year, string period)
        {
            string directoryPath = @"c:\Users\79131\Downloads";

            var files = Directory.GetFiles(directoryPath)
                .Where(x => x.Contains("_financemarker"))
                .Select(x => new FileInfo(x))
                .ToList();

            var fileNames = files.Select(x => x.Name).ToList();

            var tickers = fileNames.Select(x => x.Substring(0, x.IndexOf("_"))).Distinct().ToList();

            foreach (var ticker in tickers)
            {
                var tickerFiles = files.Where(x => x.Name.Contains($"{ticker}_financemarker")).ToList();

                var dictionary = new Dictionary<string, string>();

                foreach (var tickerFile in tickerFiles)
                {
                    var lines = File.ReadAllLines(tickerFile.FullName);
                    var headerLine = lines[0];
                    var valuesLine = lines.FirstOrDefault(x => x.Contains($",{year},") && x.Contains($",{period},") && x.Contains(",МСФО,"));

                    if (valuesLine is null)
                        continue;

                    var headers = headerLine.Split(',');
                    var values = valuesLine.Split(',');

                    for (var i = 0; i < values.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(values[i].Trim()))
                            dictionary.TryAdd(headers[i], values[i]);
                    }
                }

                double? revenue = null;             // Выручка
                double? netProfit = null;           // Чистая прибыль
                double? ebitda = null;              // EBITDA
                double? assets = null;              // Активы
                double? liabilities = null;         // Обязательства
                double? netDebt = null;             // Чистый долг
                double? ownCapital = null;          // Собственный капитал
                double? equityStockHolders = null;  // Капитал акционеров
                double? fcf = null;                 // Свободный денежный поток
                double? eps = null;                 // Прибыль на акцию
                double? marketCap = null;           // Капитализация
                double? numberShares = null;        // Количество акций

                dictionary.TryGetValue("amount", out var amount);

                revenue             = GetFromDictionary("revenue");
                netProfit           = GetFromDictionary("earnings");
                ebitda              = GetFromDictionary("ebitda");
                assets              = GetFromDictionary("total_assets");
                liabilities         = GetFromDictionary("total_liabilities");
                netDebt             = GetFromDictionary("net_debt");
                ownCapital          = GetFromDictionary("equity");
                equityStockHolders  = GetFromDictionary("equity_stock_holders");
                fcf                 = GetFromDictionary("fcf");
                eps                 = GetFromDictionary("earnings_ps");
                marketCap           = GetFromDictionary("capital");
                numberShares        = GetFromDictionary("num1");

                var amountValue = StringUtils.ToDouble(amount);

                if (amountValue == 1_000_000_000.0)
                {
                    revenue             *= 1.0;
                    netProfit           *= 1.0;
                    ebitda              *= 1.0;
                    assets              *= 1.0;
                    liabilities         *= 1.0;
                    netDebt             *= 1.0;
                    ownCapital          *= 1.0;
                    equityStockHolders  *= 1.0;
                    fcf                 *= 1.0;
                    eps                 *= 1.0;
                    marketCap           *= 1.0 / 1_000_000_000.0;
                    numberShares        *= 1.0 / 1_000_000.0;
                }

                else if (amountValue == 1_000_000.0)
                {
                    revenue             *= 1.0 / 1_000.0;
                    netProfit           *= 1.0 / 1_000.0;
                    ebitda              *= 1.0 / 1_000.0;
                    assets              *= 1.0 / 1_000.0;
                    liabilities         *= 1.0 / 1_000.0;
                    netDebt             *= 1.0 / 1_000.0;
                    ownCapital          *= 1.0 / 1_000.0;
                    equityStockHolders  *= 1.0 / 1_000.0;
                    fcf                 *= 1.0 / 1_000.0;
                    eps                 *= 1.0;
                    marketCap           *= 1.0 / 1_000_000_000.0;
                    numberShares        *= 1.0 / 1_000_000.0;
                }

                else if (amountValue == 1_000.0)
                {
                    revenue             *= 1.0;
                    netProfit           *= 1.0;
                    ebitda              *= 1.0;
                    assets              *= 1.0;
                    liabilities         *= 1.0;
                    netDebt             *= 1.0;
                    ownCapital          *= 1.0;
                    equityStockHolders  *= 1.0;
                    fcf                 *= 1.0;
                    eps                 *= 1.0;
                    marketCap           *= 1.0;
                    numberShares        *= 1.0;
                }

                await SaveAsync("Revenue", revenue); 
                await SaveAsync("NetProfit", netProfit); 
                await SaveAsync("Ebitda", ebitda); 
                await SaveAsync("Assets", assets); 
                await SaveAsync("Liabilities", liabilities); 
                await SaveAsync("NetDebt", netDebt); 
                await SaveAsync("OwnCapital", ownCapital); 
                await SaveAsync("EquityStockHolders", equityStockHolders); 
                await SaveAsync("Fcf", fcf); 
                await SaveAsync("Eps", eps);                 
                await SaveAsync("MarketCap", marketCap); 
                await SaveAsync("NumberShares", numberShares); 

                await SaveEvAsync();
                await SavePeAsync();
                await SavePbvAsync();
                await SaveRoeAsync();
                await SaveRoaAsync();

                double? GetFromDictionary(string key)
                {
                    dictionary.TryGetValue(key, out var stringValue);
                    return StringUtils.ToDouble(stringValue);
                }

                async Task SaveAsync(string type, double? value)
                {
                    if (value.HasValue)
                        await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                            new FundamentalParameter
                            {
                                Ticker = ticker,
                                Type = type,
                                Period = year,
                                Value = value.Value,
                                ExtData = string.Empty
                            });
                }

                async Task SaveEvAsync()
                {
                    if (!marketCap.HasValue || !netDebt.HasValue)
                        return;

                    var ev = marketCap.Value + netDebt.Value;

                    await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                        new FundamentalParameter
                        {
                            Ticker = ticker,
                            Type = "Ev",
                            Period = year,
                            Value = ev,
                            ExtData = string.Empty
                        });
                }

                async Task SavePeAsync()
                {
                    if (!marketCap.HasValue || !netProfit.HasValue)
                        return;

                    if (netProfit.Value == 0.0)
                        return;

                    var pe = marketCap.Value / netProfit.Value;

                    await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                        new FundamentalParameter
                        {
                            Ticker = ticker,
                            Type = "Pe",
                            Period = year,
                            Value = pe,
                            ExtData = string.Empty
                        });
                }

                async Task SavePbvAsync()
                {
                    if (!marketCap.HasValue || !equityStockHolders.HasValue)
                        return;

                    if (equityStockHolders.Value == 0.0)
                        return;

                    var pbv = marketCap.Value / equityStockHolders.Value;

                    await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                        new FundamentalParameter
                        {
                            Ticker = ticker,
                            Type = "Pbv",
                            Period = year,
                            Value = pbv,
                            ExtData = string.Empty
                        });
                }

                async Task SaveRoeAsync()
                {
                    if (!netProfit.HasValue || !ownCapital.HasValue)
                        return;

                    if (ownCapital.Value == 0.0)
                        return;

                    var roe = netProfit.Value / ownCapital.Value * 100.0;

                    await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                        new FundamentalParameter
                        {
                            Ticker = ticker,
                            Type = "Roe",
                            Period = year,
                            Value = roe,
                            ExtData = string.Empty
                        });
                }

                async Task SaveRoaAsync()
                {
                    if (!netProfit.HasValue || !assets.HasValue)
                        return;

                    if (assets.Value == 0.0)
                        return;

                    var roa = netProfit.Value / assets.Value * 100.0;

                    await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                        new FundamentalParameter
                        {
                            Ticker = ticker,
                            Type = "Roa",
                            Period = year,
                            Value = roa,
                            ExtData = string.Empty
                        });
                }
            }
        }
    }
}
