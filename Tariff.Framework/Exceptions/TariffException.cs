using System;
using Tariff.Framework.Types;

namespace Tariff.Framework.Exceptions
{
    public abstract class TariffException : Exception
    {
        public TariffException(string message)
            : base(message)
        {  }

        public abstract int ErrorCode { get; }
    }


    public class InvalidConsumptionException : TariffException
    {
        public InvalidConsumptionException(string consumption)
        : base($"This entered {consumption} value is not correct. Please enter a valid integer value")
        { }

        public override int ErrorCode => 
            TariffErrorCode.InvalidConsumptionError;
    }
}
