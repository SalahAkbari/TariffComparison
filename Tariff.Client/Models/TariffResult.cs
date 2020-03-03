namespace Tariff.Client.Models
{
    public class TariffResult
    {
        public bool IsSuccessful { get; set; }
        public string Name { get; set; }
        public double AnnualCost { get; set; }
        public string Message { get; set; }

    }
}
