namespace Credit.Contract.Commands.UserCreditRequest.CreateUserCreditRequest
{
    public class CreateUserCreditRequestViewModel
    {
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal MonthlyIncome { get; set; }
        public int CreditScore { get; set; }
        public bool Approved { get; set; }
        public string Status { get; set; }
        public decimal? CreditLimit { get; set; }
    }
}