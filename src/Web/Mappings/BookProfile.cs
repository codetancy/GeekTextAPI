using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Matching;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Requests.Queries;
using Web.Contracts.V1.Responses;
using Web.Models;

namespace Web.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<GetBooksQuery, BookSearchFilter>(MemberList.Source);

            CreateMap<CreateBookRequest, Book>(MemberList.None)
                .ForMember(dest => dest.PublicationDate,
                    opts => opts.MapFrom(src => DateTime.Parse(src.PublicationDate)))
                .ForMember(dest => dest.GenreName, opts => opts.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Genre, opts => opts.Ignore());

            CreateMap<Book, SimpleBookResponse>(MemberList.Destination);

            CreateMap<Book, BookResponse>(MemberList.Destination)
                .ForMember(dest => dest.Genre, opts => opts.MapFrom(src => src.GenreName))
                .ForMember(dest => dest.PublicationDate,
                    opts => opts.MapFrom(src => src.PublicationDate.ToShortDateString()))
                .ForMember(dest => dest.Authors,
                    opts => opts.MapFrom(
                        src => src.Authors.Select(a => new SimpleAuthorResponse(a.Id, a.PenName))));
        }
    }
}
