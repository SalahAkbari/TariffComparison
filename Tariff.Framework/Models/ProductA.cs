using Tariff.Framework.Types;

namespace Tariff.Framework.Models
{
    public class ProductA : Product
    {
        public ProductA(int consumption)
        {
            AnnualCost = AppConstants.PRODUCT_A_ANNUAL_COST
                + (consumption * AppConstants.PRODUCT_A_CONSUMPTION_COST_PER_kWh);
        }

        public override string Name { get => "basic electricity tariff"; }
    }
}
