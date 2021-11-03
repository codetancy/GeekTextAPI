using System;
using System.Collections.Generic;
using System.Linq;
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
                .ForMember(dest => dest.CardNumber,
                    opts => opts.MapFrom(
                        src => HideCardNumber(src.CardNumber)));

            CreateMap<List<Card>, UserCardResponse>(MemberList.Destination)
                .ForMember(dest => dest.UserId,
                    opts => opts.MapFrom(
                        src => src.First().UserId))
                .ForMember(dest => dest.Cards,
                    opts => opts.MapFrom((source, _, _, context) =>
                        source.Select(card => context.Mapper.Map<Card, SimpleCardResponse>(card))));
        }

        private static string HideCardNumber(string cardNumber)
            => string.Create(cardNumber.Length, cardNumber, (span, value) =>
            {
                value.AsSpan().CopyTo(span);
                span[..^4].Fill('*');
            });
    }
}