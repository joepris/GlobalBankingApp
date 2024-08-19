using Microsoft.AspNetCore.Mvc;
using GlobalBankingApp.Models;

namespace GlobalBankingApp.Controllers
{
    public class HomeController : Controller
    {

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
            var selectedAccount = TempData.Peek("SelectedAccount") as int? ?? 0;

            var model = new BankDetailsViewModel
            {
                AccountHolderName = "John Smith",
                AccountNumber = _accountBalances,
                SelectedAccount = selectedAccount
            };

            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

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

            TempData["SelectedAccount"] = account.Number;

            if (actionType == AccountActionType.Deposit)
            {
                account.Deposit(amount, currency);
            }
            else if (actionType == AccountActionType.Withdraw)
            {
                bool success = account.Withdraw(amount, currency);
                if (!success)
                {
                    TempData["ErrorMessage"] = "Insufficient funds";
                    return RedirectToAction("Index");
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
