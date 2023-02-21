using Grpc.Core;

namespace BetService.Grpc.Services
{
    public class BetService : Grpc.BetService.BetServiceBase
    {
        public BetService()
        {

        }

        public override Task<CreateBetResponse> CreateBet(CreateBetRequset request, ServerCallContext context)
        {
            return base.CreateBet(request, context);
        }

        public override Task<CreateBetRangeResponse> CreateBetRange(CreateBetRangeRequest request, ServerCallContext context)
        {
            return base.CreateBetRange(request, context);
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
