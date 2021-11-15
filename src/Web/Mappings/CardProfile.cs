using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Models;

namespace Web.Mappings
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CreateCardRequest, Card>(MemberList.Source);

            CreateMap<Card, SimpleCardResponse>(MemberList.Destination)
                .ForMember(dest => dest.ExpirationDate,
                    opts => opts.MapFrom(
                        src => FormatExpiration(src.ExpirationMonth, src.ExpirationYear)))
                .ForMember(dest => dest.CardNumber,
                    opts => opts.MapFrom(
                        src => HideCardNumber(src.CardNumber)));
        }

        private static string FormatExpiration(string month, string year)
            => string.Create(5, string.Empty, (span, _) =>
            {
                span[0] = month[0];
                span[1] = month[1];
                span[2] = '/';
                span[3] = year[2];
                span[4] = year[3];
            });

        private static string HideCardNumber(string cardNumber)
            => string.Create(cardNumber.Length, cardNumber, (span, value) =>
            {
                value.AsSpan().CopyTo(span);
                span[..^4].Fill('*');
            });
    }
}
