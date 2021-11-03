using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web.Contracts.V1.Responses;
using Web.Models;

namespace Web.Mappings
{
    public class WishListProfile : Profile
    {
        public WishListProfile()
        {
            CreateMap<WishListBook, WishListBookResponse>(MemberList.None)
                .ForMember(x => x.Book, x => x.MapFrom(
                    (source, _, _, context) => context.Mapper.Map<Book, SimpleBookResponse>(source.Book)));

            CreateMap<WishList, WishListResponse>(MemberList.None)
                .ForMember(x => x.WishListBooks, x => x.MapFrom(
                    (source, _, _, context) => source.WishListBooks.Select(wb => context.Mapper.Map<WishListBook, WishListBookResponse>(wb))));
        }
    }
}
