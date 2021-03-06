using AutoMapper;
using Web.Contracts.V1.Requests.Queries;
using Web.Models;

namespace Web.Mappings
{
    public class PaginationProfile : Profile
    {
        public PaginationProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
