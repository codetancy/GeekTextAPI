using System;

namespace Web.Services.Interfaces
{
    public interface IUriService
    {
        Uri GetPaginatedUri(int pageNumber, int pageSize);
    }
}
