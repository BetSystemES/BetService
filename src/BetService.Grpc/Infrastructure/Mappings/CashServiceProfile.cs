using AutoMapper;
using BetService.Grpc.Infrastructure.Mappings.Extensions;
using CashService.GRPC;
using Google.Protobuf.WellKnownTypes;

using BusinessModels = BetService.Grpc.Models.CashService;

namespace BetService.Grpc.Infrastructure.Mappings
{
    public class CashServiceProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="TransactionModelApiMap" /> class.</summary>
        public CashServiceProfile()
        {
            CreateMap<DateTimeOffset, Timestamp>()
                .ConvertUsing((x, res) => res = x.ToTimestamp());
            CreateMap<Timestamp, DateTimeOffset>()
                .ConvertUsing((x, res) => res = x.ToDateTimeOffset());

            CreateMap<Transaction, BusinessModels.TransactionCreateModel>()
                .ReverseMap()
                .Ignore(e => e.Id);

            CreateMap<BusinessModels.TransactionModelCreateModel, TransactionModel>()
                .ForMember(x => x.Transactions, y => y.MapFrom(z => z.Transactions))
                .Ignore(e => e.Amount)
                .Ignore(e => e.ProfileId)
                .ReverseMap();

            CreateMap<BusinessModels.TransactionModelCreateModel, TransactionRequestModel>()
                .Ignore(e => e.ProfileId);

            CreateMap<CashType, BusinessModels.Enums.CashType>()
                .ReverseMap();
        }
    }
}
