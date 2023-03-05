using AutoMapper;
using BetService.BusinessLogic.Contracts.Services;
using BetService.BusinessLogic.Extensions;
using BetService.Grpc.Infrastructure.Clients.CashService;
using Grpc.Core;
using BusinessModels = BetService.BusinessLogic.Models;

namespace BetService.Grpc.Services
{
    /// <summary>
    /// Implementation of bet grpc service.
    /// </summary>
    /// <seealso cref="BetService.Grpc.BetService.BetServiceBase" />
    public class BetService : Grpc.BetService.BetServiceBase
    {
        // TODO: logger was not used in the service. Use it or remove.
        private readonly ILogger<BetService> _logger;
        private readonly IBetService _betService;
        private readonly ICashService _cashService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="betService">The bet service.</param>
        /// <param name="mapper">The mapper.</param>
        public BetService(ILogger<BetService> logger,
            IBetService betService,
            ICashService cashService,
            IMapper mapper)
        {
            _logger = logger;
            _betService = betService;
            _cashService = cashService;
            _mapper = mapper;
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

            var response = new GetUserBetByIdResponse()
            {
                Bet = grpcBet
            };

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

            var betStatusUpdateModels = _mapper.Map<IEnumerable<BusinessModels.BetStatusUpdateModel>>(request.BetStatusUpdateModels);

            var updatedBets = await _betService.UpdateStatuses(betStatusUpdateModels, token);

            var positiveUnpaidBets = updatedBets.ToPositiveProcessingBets();

            await _cashService.DepositRange(positiveUnpaidBets, token);

            await _betService.CompletePayoutStatues(positiveUnpaidBets, token);

            var response = new UpdateBetStatusesResponse();

            return response;
        }
    }
}
