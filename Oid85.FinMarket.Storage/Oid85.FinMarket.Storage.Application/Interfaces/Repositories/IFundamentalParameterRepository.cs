using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    /// <summary>
    /// Репозиторий фундаментальных параметров
    /// </summary>
    public interface IFundamentalParameterRepository
    {
        /// <summary>
        /// Создание/изменение фундаментального параметра
        /// </summary>
        Task<Guid?> CreateOrUpdateFundamentalParameterAsync(FundamentalParameter fundamentalParameter);

        /// <summary>
        /// Получить список фундаментальных параметров
        /// </summary>
        /// <returns></returns>
        Task<List<FundamentalParameter>?> GetFundamentalParametersAsync();
    }
}
