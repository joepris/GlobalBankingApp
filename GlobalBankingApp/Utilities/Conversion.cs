using GlobalBankingApp.Models;

namespace GlobalBankingApp.Utilities
{
    public static class Conversion
    {
        public static decimal ToCAD(decimal amount, Currency currency)
        {
            switch (currency)
            {
                case Currency.CAD:
                    return amount;
                case Currency.USD:
                    return amount * 2.0m;
                case Currency.MXN:
                    return amount / 10.0m;
                case Currency.EURO:
                    return amount * 4.0m;
                default:
                    throw new ArgumentException($"Unsupported currency: {currency}", nameof(currency));
            }
        }
    }
}
