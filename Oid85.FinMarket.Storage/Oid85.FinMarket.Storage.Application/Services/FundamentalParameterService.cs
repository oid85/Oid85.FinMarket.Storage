using System.Collections.Generic;
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

                await SaveParameterAsync("revenue", "Revenue", 1.0 / 1_000.0); // Выручка
                await SaveParameterAsync("earnings", "NetProfit", 1.0 / 1_000.0); // Чистая прибыль
                await SaveParameterAsync("ebitda", "Ebitda", 1.0 / 1_000.0); // EBITDA
                await SaveParameterAsync("total_assets", "Assets", 1.0 / 1_000.0); // Активы
                await SaveParameterAsync("total_liabilities", "Liabilities", 1.0 / 1_000.0); // Обязательства
                await SaveParameterAsync("net_debt", "NetDebt", 1.0 / 1_000.0); // Чистый долг
                await SaveParameterAsync("equity", "OwnCapital", 1.0 / 1_000.0); // Собственный капитал
                await SaveParameterAsync("equity_stock_holders", "EquityStockHolders", 1.0 / 1_000.0); // Капитал акционеров
                await SaveParameterAsync("fcf", "Fcf", 1.0 / 1_000.0); // Свободный денежный поток
                await SaveParameterAsync("earnings_ps", "Eps", 1.0); // Прибыль на акцию                
                await SaveParameterAsync("capital", "MarketCap", 1.0 / 1_000_000_000.0); // Капитализация
                await SaveParameterAsync("num1", "NumberShares", 1.0 / 1_000_000.0); // Количество акций
                await SaveEvAsync();
                await SavePeAsync();
                await SavePbvAsync();
                await SaveRoeAsync();
                await SaveRoaAsync();

                async Task SaveParameterAsync(string fileAlias, string dataBaseAlias, double coeff)
                {
                    if (dictionary.TryGetValue(fileAlias, out var parameter))
                    {
                        await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                            new FundamentalParameter
                            {
                                Ticker = ticker,
                                Type = dataBaseAlias,
                                Period = year,
                                Value = StringUtils.ToDouble(parameter) * coeff,
                                ExtData = string.Empty
                            });
                    }
                }

                async Task SaveEvAsync()
                {
                    if (dictionary.TryGetValue("capital", out var marketCap) &&
                        dictionary.TryGetValue("net_debt", out var netDebt))
                    {
                        double marketCapValue = StringUtils.ToDouble(marketCap) * 1.0 / 1_000_000_000.0;
                        double netDebtValue = StringUtils.ToDouble(netDebt) * 1.0 / 1_000.0;

                        if (marketCapValue == 0.0 || netDebtValue == 0.0)
                            return;

                        var ev = marketCapValue + netDebtValue;

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
                }

                async Task SavePeAsync()
                {
                    if (dictionary.TryGetValue("capital", out var marketCap) &&
                        dictionary.TryGetValue("earnings", out var netProfit))
                    {
                        double marketCapValue = StringUtils.ToDouble(marketCap) * 1.0 / 1_000_000_000.0;
                        double netProfitValue = StringUtils.ToDouble(netProfit) * 1.0 / 1_000.0;

                        if (marketCapValue == 0.0 || netProfitValue == 0.0)
                            return;

                        var pe = marketCapValue / netProfitValue;

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
                }

                async Task SavePbvAsync()
                {
                    if (dictionary.TryGetValue("capital", out var marketCap) &&
                        dictionary.TryGetValue("equity_stock_holders", out var equityStockHolders))
                    {
                        double marketCapValue = StringUtils.ToDouble(marketCap) * 1.0 / 1_000_000_000.0;
                        double equityStockHoldersValue = StringUtils.ToDouble(equityStockHolders) * 1.0 / 1_000.0;

                        if (marketCapValue == 0.0 || equityStockHoldersValue == 0.0)
                            return;

                        var pbv = marketCapValue / equityStockHoldersValue;

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
                }

                async Task SaveRoeAsync()
                {
                    if (dictionary.TryGetValue("earnings", out var netProfit) &&
                        dictionary.TryGetValue("equity", out var ownCapital))
                    {
                        double netProfitValue = StringUtils.ToDouble(netProfit) * 1.0 / 1_000.0;
                        double ownCapitalValue = StringUtils.ToDouble(ownCapital) * 1.0 / 1_000.0;

                        if (netProfitValue == 0.0 || ownCapitalValue == 0.0)
                            return;

                        var roe = netProfitValue / ownCapitalValue * 100.0;

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
                }

                async Task SaveRoaAsync()
                {
                    if (dictionary.TryGetValue("earnings", out var netProfit) &&
                        dictionary.TryGetValue("total_assets", out var assets))
                    {
                        double netProfitValue = StringUtils.ToDouble(netProfit) * 1.0 / 1_000.0;
                        double assetsValue = StringUtils.ToDouble(assets) * 1.0 / 1_000.0;

                        if (netProfitValue == 0.0 || assetsValue == 0.0)
                            return;

                        var roa = netProfitValue / assetsValue * 100.0;

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
}
