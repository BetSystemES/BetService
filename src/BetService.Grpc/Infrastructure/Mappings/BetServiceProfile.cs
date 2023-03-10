using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using BusinessEnums = BetService.BusinessLogic.Enums;
using BusinessModels = BetService.BusinessLogic.Entities;

namespace BetService.Grpc.Infrastructure.Mappings
{
    /// <summary>Profile of grpc layer</summary>
    public class BetServiceProfile : Profile
    {
        public BetServiceProfile()
        {
            CreateMap<string, Guid>()
                .ConvertUsing((x, res) => res = Guid.TryParse(x, out var id) ? id : Guid.Empty);
            CreateMap<Guid?, string>()
                .ConvertUsing((x, res) => res = x?.ToString() ?? string.Empty);
            CreateMap<DateTime, Timestamp>()
                .ConvertUsing(x => Timestamp.FromDateTime(x));
            CreateMap<Timestamp, DateTime>()
                .ConvertUsing(x => x.ToDateTime());
            CreateMap<BusinessModels.Bet, Bet>()
                .ReverseMap();
            CreateMap<BusinessModels.BetStatusUpdateModel, BetStatusUpdateModel>()
                .ReverseMap();
            CreateMap<BetCreateModel, BusinessModels.Bet>();
            CreateMap<BusinessEnums.BetPayoutStatus, BetPayoutStatus>()
                .ReverseMap();
            CreateMap<BusinessEnums.BetStatusType, BetStatusType>()
                .ReverseMap();
        }
    }
}
