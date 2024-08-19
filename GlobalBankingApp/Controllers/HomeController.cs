using Microsoft.AspNetCore.Mvc;

namespace GlobalBankingApp.Controllers
{

    public static class Conversion
    {
        public static decimal CanadianDollars(decimal amount, Currency currency)
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
            decimal convertedAmount = Conversion.CanadianDollars(amount, currency);

            if (convertedAmount <= Balance)
            {
                Balance -= convertedAmount;
                return true;
            }

            return false;
        }
    }

    public class BankDetailsViewModel
    {
        public string AccountHolderName { get; set; }
        public AccountBalance[] AccountNumber { get; set; }
    }

    public enum Currency { CAD, USD, MXN, EURO }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new BankDetailsViewModel
            {
                AccountHolderName = "John Smith",
                AccountNumber = GetAccountBalances()
            };
            return View(model);
        }

        protected static AccountBalance[] _accountBalances = new AccountBalance[]
        {
        new AccountBalance(123456789, 1000.00m),
        new AccountBalance(987654321, 4080.00m)
        };

        private AccountBalance[] GetAccountBalances()
        {
            return _accountBalances;
        }

        public ActionResult Deposit(int accountNumber, decimal amount, Currency currency)
        {
            var accountBalances = GetAccountBalances();
            var account = accountBalances.FirstOrDefault(acc => acc.Number == accountNumber);

            if (account != null)
            {
                decimal convertedAmount = Conversion.CanadianDollars(amount, currency);
                account.Balance += convertedAmount;

                return RedirectToAction("Index");
            }

            return View("Error");
        }

        public ActionResult WithdrawFunds(int accountNumber, decimal amount, Currency currency)
        {
            var accountBalances = GetAccountBalances();
            var account = accountBalances.FirstOrDefault(acc => acc.Number == accountNumber);

            if (account != null)
            {
                bool success = account.Withdraw(amount, currency);

                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("InsufficientFunds");
                }
            }

            return View("AccountNotFound");
        }

        [HttpPost]
        public ActionResult UpdateAccount(int accountNumber, decimal amount, Currency currency, string action)
        {
            if (action == "Deposit")
                {
                    Deposit(accountNumber, amount, currency);
                }
            else if (action == "Withdraw")
                {
                    WithdrawFunds(accountNumber, amount, currency); 
                }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Login");
        }
    }


}