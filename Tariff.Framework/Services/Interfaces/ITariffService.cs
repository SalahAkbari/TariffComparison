using System.Collections.Generic;
using System.Threading.Tasks;
using Tariff.Framework.Models;

namespace Tariff.Framework.Services.Interface
{
    public interface ITariffService
    {
        Task<IEnumerable<TariffResult>> GetProducts(string usage);
    }
}
