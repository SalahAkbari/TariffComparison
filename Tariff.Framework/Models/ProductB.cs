using Tariff.Framework.Types;

namespace Tariff.Framework.Models
{
    public class ProductB : Product
    {
        public ProductB(int consumption)
        {
            AnnualCost = AppConstants.PRODUCT_B_BASE_COST;
            if (consumption > AppConstants.PRODUCT_B_MAXIMUM_ALLOWED)
            {
                AnnualCost += (consumption - AppConstants.PRODUCT_B_MAXIMUM_ALLOWED) * AppConstants.PRODUCT_B_COST_PER_kWh;
            }
        }

        public override string Name { get => "Packaged tariff"; }
    }
}
