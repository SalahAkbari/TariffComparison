namespace Tariff.Framework.Types
{
    public class AppConstants
    {
        // PRODUCT A
        public const int PRODUCT_A_BASE_COST_PER_MONTH = 5;
        public const int MONTHS_PER_YEAR = 12;
        public const double PRODUCT_A_CONSUMPTION_COST_PER_kWh = 0.22;
        public const int PRODUCT_A_ANNUAL_COST = PRODUCT_A_BASE_COST_PER_MONTH * MONTHS_PER_YEAR;

        // PRODUCT B
        public const int PRODUCT_B_BASE_COST = 800;
        public const int PRODUCT_B_MAXIMUM_ALLOWED = 4000;
        public const double PRODUCT_B_COST_PER_kWh = 0.3;
    }
}
