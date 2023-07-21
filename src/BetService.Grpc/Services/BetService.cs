using AutoMapper;
using BetService.BusinessLogic.Contracts.Services;
using BetService.Grpc.Extensions;
using BetService.Grpc.Models.CashService;
using CashService.GRPC;
using Grpc.Core;
using Grpc.Net.ClientFactory;
using Newtonsoft.Json.Linq;
using BusinessModels = BetService.BusinessLogic.Entities;

namespace BetService.Grpc.Services
{
    /// <summary>
    /// Implementation of bet grpc service.
    /// </summary>
    /// <seealso cref="Grpc.BetService.BetServiceBase" />
    public class BetService : Grpc.BetService.BetServiceBase
    {
        private readonly IBetService _betService;
        private readonly IMapper _mapper;
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly ILogger<BetService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetService"/> class.
        /// </summary>
        /// <param name="betService">The bet service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="grpcClientFactory">The GRPC client factory.</param>
        public BetService
        (
            IBetService betService,
            IMapper mapper,
            GrpcClientFactory grpcClientFactory
,
            ILogger<BetService> logger)
        {
            _betService = betService;
            _mapper = mapper;
            _grpcClientFactory = grpcClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Creates the bet.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns> <seealso cref="CreateBetResponse"/></returns>
        public override async Task<CreateBetResponse> CreateBet(CreateBetRequset request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            var client = _grpcClientFactory.GetGrpcClient<CashService.GRPC.CashService.CashServiceClient>();

            try
            {
                var transactionModel = new TransactionModelCreateModel()
                {
                    Transactions = new List<TransactionCreateModel>()
                    {
                        new TransactionCreateModel()
                        {
                            Amount = request.BetCreateModel.Amount,
                            CashType = Models.CashService.Enums.CashType.Cash
                        }
                    }
                };
                var grpcTransactionModel = _mapper.Map<TransactionRequestModel>(transactionModel);
                grpcTransactionModel.ProfileId = request.BetCreateModel.UserId;
            
                var withdrawRequest = new WithdrawRequest
                {
                    Withdrawrequest = grpcTransactionModel
                };
                await client.WithdrawAsync(withdrawRequest);
            }
            catch (Exception)
            {
                throw;
            }

            var bet = _mapper.Map<BusinessModels.Bet>(request.BetCreateModel);
            await _betService.Create(bet, token);

            var response = new CreateBetResponse();
            return response;
        }

        /// <summary>
        /// Creates the bet range.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns><seealso cref="CreateBetRangeResponse"/></returns>
        public override async Task<CreateBetRangeResponse> CreateBetRange(CreateBetRangeRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var bets = _mapper.Map<List<BusinessModels.Bet>>(request.BetCreateModels);
            await _betService.CreateRange(bets, token);

            var response = new CreateBetRangeResponse();
            return response;
        }

        /// <summary>
        /// Gets the user bet by identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns><seealso cref="GetUserBetByIdResponse"/></returns>
        public override async Task<GetUserBetByIdResponse> GetUserBetById(GetUserBetByIdRequset request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var id = _mapper.Map<Guid>(request.Id);

            var bet = await _betService.GetById(id, token);
            var grpcBet = _mapper.Map<Bet>(bet);

            var response = new GetUserBetByIdResponse() { Bet = grpcBet };
            return response;
        }

        /// <summary>
        /// Gets the users bets.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns><seealso cref="GetUsersBetsResponse"/></returns>
        public override async Task<GetUsersBetsResponse> GetUsersBets(GetUsersBetsRequset request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var id = _mapper.Map<Guid>(request.UserId);

            var bets = await _betService.GetRangeByUserId(id, request.Page, request.PageSize, token);

            var grpcBets = _mapper.Map<IEnumerable<Bet>>(bets);

            var response = new GetUsersBetsResponse();
            response.Bets.AddRange(grpcBets);

            return response;
        }

        /// <summary>
        /// Updates the bet statuses.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns><seealso cref="UpdateBetStatusesResponse"/></returns>
        public override async Task<UpdateBetStatusesResponse> UpdateBetStatuses(UpdateBetStatusesRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var betStatusUpdateModels =
                _mapper.Map<IEnumerable<BusinessModels.BetStatusUpdateModel>>(request.BetStatusUpdateModels);
            var existingProcessingBets = await _betService.HandleUpdateStatuses(betStatusUpdateModels, token);

            var client = _grpcClientFactory.GetGrpcClient<CashService.GRPC.CashService.CashServiceClient>();
            var cashRequest = existingProcessingBets.ToDepositRangeRequest();
            await client.DepositRangeAsync(cashRequest);

            await _betService.CompletePayoutStatues(existingProcessingBets, token);

            var response = new UpdateBetStatusesResponse();
            return response;
        }

        public override async Task<UpdateBetStatusResponse> UpdateBetStatus(UpdateBetStatusRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            var betStatusUpdateModel =
                _mapper.Map<BusinessModels.BetStatusUpdateModel>(request.BetStatusUpdateModel);
            _logger.LogDebug($"!! betStatusUpdateModel: {betStatusUpdateModel.CoefficientId} {betStatusUpdateModel.StatusType}");
            var existingProcessingBets = await _betService.HandleUpdateStatus(betStatusUpdateModel, token);
            _logger.LogDebug($"!! existingProcessingBets length {existingProcessingBets.Count()}");
            var client = _grpcClientFactory.GetGrpcClient<CashService.GRPC.CashService.CashServiceClient>();
            var cashRequest = existingProcessingBets.ToDepositRangeRequest();
            _logger.LogDebug($"!! cashRequest count {cashRequest.DepositRangeRequests.Count}");
            await client.DepositRangeAsync(cashRequest);

            await _betService.CompletePayoutStatues(existingProcessingBets, token);

            var response = new UpdateBetStatusResponse();
            return response;
        }
    }
}
