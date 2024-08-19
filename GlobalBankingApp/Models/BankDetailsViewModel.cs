namespace GlobalBankingApp.Models
{
    public class BankDetailsViewModel
    {
        public string AccountHolderName { get; set; }
        public AccountBalance[] AccountNumber { get; set; }
        public int SelectedAccount { get; set; }
    }
}
