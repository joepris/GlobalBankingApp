using Microsoft.AspNetCore.Mvc;
using GlobalBankingApp.Models;

namespace GlobalBankingApp.Controllers
{
    public class HomeController : Controller
    {
        private static int _selectedAccount;

        public enum AccountActionType
        {
            Deposit,
            Withdraw
        }

        private static AccountBalance[] _accountBalances = new AccountBalance[]
        {
            new AccountBalance(123456789, 1000.00m),
            new AccountBalance(987654321, 4080.00m)
        };

        public ActionResult Index()
        {
            var model = new BankDetailsViewModel
            {
                AccountHolderName = "John Smith",
                AccountNumber = _accountBalances,
                SelectedAccount = _selectedAccount
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateAccount(int accountNumber, decimal amount, Currency currency, AccountActionType actionType)
        {
            var account = _accountBalances.FirstOrDefault(acc => acc.Number == accountNumber);

            if (account == null)
            {
                return View("AccountNotFound");
            }

            _selectedAccount = account.Number;

            if (actionType == AccountActionType.Deposit)
            {
                account.Deposit(amount, currency);
            }
            else if (actionType == AccountActionType.Withdraw)
            {
                bool success = account.Withdraw(amount, currency);
                if (!success)
                {
                    return View("InsufficientFunds");
                }
            }
            else
            {
                return View("InvalidAction");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Login");
        }
    }
}
