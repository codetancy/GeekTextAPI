using AutoMapper;

namespace Web.Mappings
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartBook, CartBookResponse>(MemberList.None)
                .ForMember(dest => dest.Book,
                    opts => opts.MapFrom((source, _, _, context) =>
                    context.Mapper.Map<Book, SimpleBookResponse>(source.Book)));

            CreateMap<Cart, CartResponse>(MemberList.None)
                .ForMember(dest => dest.Cartbooks,
                    opts => opts.MapFrom((source, _, _, context) =>
                        source.CartBooks.Select(
                            cartBook => context.Mapper.Map<CartBook, CartBookResponse>(cartBook))));
        }
    }
}