using AutoMapper;
using Google.Protobuf.WellKnownTypes;

using BusinessModels = BetService.BusinessLogic.Models;
using BusinessEnums = BetService.BusinessLogic.Enums;

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

            CreateMap<BusinessEnums.BetPaidType, BetPaidType>()
                .ReverseMap();
            CreateMap<BusinessEnums.BetStatusType, BetStatusType>()
                .ReverseMap();
        }
    }
}
