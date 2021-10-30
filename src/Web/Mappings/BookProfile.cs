using AutoMapper;
using Web.Contracts.V1.Requests.Queries;
using Web.Models;

namespace Web.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<GetBooksQuery, BookSearchFilter>();
        }
    }
}
