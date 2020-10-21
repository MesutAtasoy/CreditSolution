using System;

namespace Credit.Contract.IntegrationEvents
{
    public class CreatedCreditUserRequestEvent
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool Approved { get; set; }

        public CreatedCreditUserRequestEvent(string name, string phone, decimal? creditLimit, bool approved)
        {
            Name = name;
            CreditLimit = creditLimit;
            Approved = approved;
            PhoneNumber = phone;
            EventId = Guid.NewGuid();
        }
    }
}