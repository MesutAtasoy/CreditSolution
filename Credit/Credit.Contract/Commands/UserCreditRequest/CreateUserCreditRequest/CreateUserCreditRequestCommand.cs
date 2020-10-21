using Credit.Contract.Base;

namespace Credit.Contract.Commands.UserCreditRequest.CreateUserCreditRequest
{
    public class CreateUserCreditRequestCommand : IBaseRequest
    {
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal MonthlyIncome { get; set; }
    }
}