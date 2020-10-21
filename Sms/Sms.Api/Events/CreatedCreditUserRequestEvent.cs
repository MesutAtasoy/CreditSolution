using System;
using MediatR;

namespace Sms.Api.Events
{
    public class CreatedCreditUserRequestEvent : IRequest<Unit>
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool Approved { get; set; }
    }
}