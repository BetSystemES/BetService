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

        public override async Task<GetUserBetByIdResponse> GetUserBetById(GetUserBetByIdRequset request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var id = _mapper.Map<Guid>(request.UserId);

            var bet = await _betService.GetById(id, token);

            var grpcBet = _mapper.Map<Bet>(bet);

            var response = new GetUserBetByIdResponse()
            {
                Bet = grpcBet
            };

            return response;
        }

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

        public override async Task<UpdateBetStatusesResponse> UpdateBetStatuses(UpdateBetStatusesRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var betStatusUpdateModels = _mapper.Map<List<BusinessModels.BetStatusUpdateModel>>(request.BetStatusUpdateModels);

            await _betService.UpdateStatuses(betStatusUpdateModels, token);

            var response = new UpdateBetStatusesResponse();

            return response;
        }
    }
}
