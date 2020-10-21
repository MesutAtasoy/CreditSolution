using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Credit.Application.Repositories.Contract;
using Credit.Application.Services.CreditEvaluation;
using Credit.Application.Services.CreditScoreServices;
using Credit.Contract.Commands.UserCreditRequest.CreateUserCreditRequest;
using Credit.Contract.IntegrationEvents;
using Framework.EventBusRabbitMQ;
using Framework.Exceptions;
using Framework.Shared.Models.Base;
using Framework.Shared.Models.Enums;
using MediatR;

namespace Credit.Application.Commands.UserCreditRequest.CreateUserCreditRequest
{
    public class  CreateUserCreditRequestCommandHandler : IRequestHandler<CreateUserCreditRequestCommand, BaseResponseModel>,
            ICommand
    {
        private readonly IUserCreditRequestRepository _userCreditRequestRepository;
        private readonly ICreditScoreService _scoreService;
        private readonly IRabbitMqProducer<CreatedCreditUserRequestEvent> _producer;

        public CreateUserCreditRequestCommandHandler(IUserCreditRequestRepository userCreditRequestRepository,
            ICreditScoreService scoreService, 
            IRabbitMqProducer<CreatedCreditUserRequestEvent> producer)
        {
            _userCreditRequestRepository = userCreditRequestRepository;
            _scoreService = scoreService;
            _producer = producer;
        }

        public async Task<BaseResponseModel> Handle(CreateUserCreditRequestCommand request,
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseModel();

            var scoreResponse = await _scoreService.GetScoreByIdentityNumberAsync(request.IdentityNumber);

            if (scoreResponse.IsError)
            {
                var messageList = scoreResponse.Messages.ToList();
                throw  new ApiException(messageList);
            }

            var creditEvaluation = new CreditEvaluation()
                .WithCreditScore(scoreResponse.Payload.Score)
                .WithMonthlyIncome(request.MonthlyIncome)
                .WithMultipleFactor(4)
                .Evaluate();

            var status = creditEvaluation.Approved ? "Approved" : "Rejected";
            await _userCreditRequestRepository.AddRequestAsync(new Domain.UserCreditRequest
            {
                Name = request.Name,
                IdentityNumber = request.IdentityNumber,
                PhoneNumber = request.PhoneNumber,
                Status = status,
                CreditLimit = creditEvaluation.CreditLimit,
                MonthlyIncome = request.MonthlyIncome,
                CreditLimitMultiplier = 4,
                CreatedOnUtc = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = default(Guid)
            });

            response.Payload = new CreateUserCreditRequestViewModel
            {
                Approved = creditEvaluation.Approved,
                Status = status,
                CreditLimit = creditEvaluation.CreditLimit,
                Name = request.Name,
                CreditScore = scoreResponse.Payload.Score,
                IdentityNumber = request.IdentityNumber,
                MonthlyIncome = request.MonthlyIncome,
                PhoneNumber = request.PhoneNumber
            };

            _producer.Publish(new CreatedCreditUserRequestEvent(request.Name, request.PhoneNumber, creditEvaluation.CreditLimit, creditEvaluation.Approved));
            
            return response;
        }
    }
}