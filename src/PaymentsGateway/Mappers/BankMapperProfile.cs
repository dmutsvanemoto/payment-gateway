using AutoMapper;
using PaymentsGateway.Models;
using PaymentsGateway.Models.BankAPI;

namespace PaymentsGateway.Mappers
{
    public class BankMapperProfile : Profile
    {
        public BankMapperProfile()
        {
            CreateMap<PaymentRequest, ProcessPaymentRequest>()
                .ForMember(x => x.MerchantId, opts => opts.MapFrom(x => x.MerchantId))
                .ForMember(x => x.Amount, opts => opts.MapFrom(x => x.Amount))
                .ForMember(x => x.Currency, opts => opts.MapFrom(x => x.Currency))
                .ForMember(x => x.CardDetails, opts => opts.MapFrom(x => x));
            CreateMap<PaymentRequest, CardDetails>()
                .ForMember(x => x.CardNumber, opts => opts.MapFrom(x => x.CardNumber))
                .ForMember(x => x.ExpiryMonth, opts => opts.MapFrom(x => x.ExpiryMonth))
                .ForMember(x => x.ExpiryYear, opts => opts.MapFrom(x => x.ExpiryYear))
                .ForMember(x => x.Cvv, opts => opts.MapFrom(x => x.Cvv));
        }
    }
}
