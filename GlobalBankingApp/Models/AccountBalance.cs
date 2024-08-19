namespace GlobalBankingApp.Models
{
    public class AccountBalance
    {
        public int Number { get; set; }
        public decimal Balance { get; set; }

        public AccountBalance(int accountNumber, decimal balance)
        {
            Number = accountNumber;
            Balance = balance;
        }

        public bool Withdraw(decimal amount, Currency currency)
        {
            decimal convertedAmount = Utilities.Conversion.ToCAD(amount, currency);

            if (convertedAmount <= Balance)
            {
                Balance -= convertedAmount;
                return true;
            }

            return false;
        }

        public void Deposit(decimal amount, Currency currency)
        {
            decimal convertedAmount = Utilities.Conversion.ToCAD(amount, currency);
            Balance += convertedAmount;
        }
    }
}
