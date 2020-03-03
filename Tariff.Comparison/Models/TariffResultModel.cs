namespace Tariff.Comparison.Models
{
    public class TariffResultModel
    {
        public string Name { get; set; }
        public double AnnualCost { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
