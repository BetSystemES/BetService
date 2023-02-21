using AutoMapper;
using BetService.BusinessLogic.Contracts.Services;
using Grpc.Core;

using BusinessModels = BetService.BusinessLogic.Models;

namespace BetService.Grpc.Services
{
    public class BetService : Grpc.BetService.BetServiceBase
    {
        private readonly ILogger<BetService> _logger;
        private readonly IBetService _betService;
        private readonly IMapper _mapper;

        public BetService(ILogger<BetService> logger,
            IBetService betService,
            IMapper mapper)
        {
            _logger = logger;
            _betService = betService;
            _mapper = mapper;
        }

        public override async Task<CreateBetResponse> CreateBet(CreateBetRequset request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var bet = _mapper.Map<BusinessModels.Bet>(request.Bet);

            await _betService.Create(bet, token);

            var response = new CreateBetResponse();

            return response;
        }

        public override async Task<CreateBetRangeResponse> CreateBetRange(CreateBetRangeRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var bets = _mapper.Map<List<BusinessModels.Bet>>(request.Bets);

            await _betService.CreateRange(bets, token);

            var response = new CreateBetRangeResponse();

            return response;
        }

        public override Task<GetUserBetByIdResponse> GetUserBetById(GetUserBetByIdRequset request, ServerCallContext context)
        {
            return base.GetUserBetById(request, context);
        }

        public override Task<GetUsersBetsResponse> GetUsersBets(GetUsersBetsRequset request, ServerCallContext context)
        {
            return base.GetUsersBets(request, context);
        }

        public override Task<UpdateBetStatusesResponse> UpdateBetStatuses(UpdateBetStatusesRequest request, ServerCallContext context)
        {
            return base.UpdateBetStatuses(request, context);
        }
    }
}
