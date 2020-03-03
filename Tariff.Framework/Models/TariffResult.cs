namespace Tariff.Framework.Models
{
    public class TariffResult
    {
        public double AnnualCost { get; set; }
        public bool IsSuccessful { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }        
    }
}
