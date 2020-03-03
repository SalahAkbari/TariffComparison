namespace Tariff.Framework.Models
{
    public abstract class Product
    {
        public abstract string Name { get; }
        public double AnnualCost { get; set; }
    }
}
