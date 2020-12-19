using System;
using Framework.EntityFrameworkCore.Models;

namespace Credit.Domain
{
    public class UserCreditRequest : BaseEntityModel
    {
        public UserCreditRequest(string identityNumber,
            string name,
            string phoneNumber,
            decimal monthlyIncome,
            decimal creditLimit,
            int? creditLimitMultiplier,
            string status)
        {
            Name = name;
            IdentityNumber = identityNumber;
            PhoneNumber = phoneNumber;
            Status = status;
            CreditLimit = creditLimit;
            MonthlyIncome = monthlyIncome;
            CreditLimitMultiplier = creditLimitMultiplier;
            CreatedOnUtc = DateTime.UtcNow;
            IsActive = true;
            IsDeleted = false;
            CreatedBy = default;
        }

        public string IdentityNumber { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public decimal MonthlyIncome { get; private set; }
        public decimal? CreditLimit { get; private set; }
        public int? CreditLimitMultiplier { get; private set; }
        public string Status { get; private set; }
    }
}