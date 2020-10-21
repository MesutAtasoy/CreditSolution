using Credit.Contract.Models.Base;

namespace Credit.Contract.Models.UserCreditRequest
{
    public class UserCreditRequestViewModel : BaseViewModel
    {
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal? CreditLimit { get; set; }
        public int? CreditLimitMultiplier { get; set; }
        public string Status { get; set; }
    }
}