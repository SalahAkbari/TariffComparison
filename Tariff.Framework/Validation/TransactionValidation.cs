using System.Threading.Tasks;
using Tariff.Framework.Exceptions;

namespace Tariff.Framework.Validation
{
    public static class TariffValidation
    {
        public static async Task<int> Validate(this string consumption)
        {
            var result = int.TryParse(consumption, out int value);

            if (!result)
            {
                throw new InvalidConsumptionException(consumption);
            }

            await Task.CompletedTask;

            return value;
        }
    }
}
