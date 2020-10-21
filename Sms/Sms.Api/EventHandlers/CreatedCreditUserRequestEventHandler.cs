using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sms.Api.Events;
using Sms.Api.Persistence;
using Sms.Api.Persistence.Models;

namespace Sms.Api.EventHandlers
{
    public class CreatedCreditUserRequestEventHandler : IRequestHandler<CreatedCreditUserRequestEvent>
    {
        private readonly ILogger<CreatedCreditUserRequestEventHandler> _logger;
        private readonly SmsApplicationContext _context;

        public CreatedCreditUserRequestEventHandler(ILogger<CreatedCreditUserRequestEventHandler> logger, 
            SmsApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(CreatedCreditUserRequestEvent request, CancellationToken cancellationToken)
        {
            //Todo:Sms Send with Sms Service Provider

            var message = request.Approved
                ? $"Sayın {request.Name}, {request.CreditLimit} limitli krediniz onaylandı"
                : $"Sayın {request.Name}, krediniz onaylanmadı";
            
            
            await _context.SmsLogs.AddAsync(new SmsLog
            {
                MessageType = "CreatedCreditUserRequestEvent",
                Message = message,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                IsDeleted = false,
                IsSend = "OK",
                CreatedOnUtc = DateTime.UtcNow,
                CreatedBy = default
            });

            await _context.SaveChangesAsync();
            
            
            return Unit.Value;
        }
    }
}