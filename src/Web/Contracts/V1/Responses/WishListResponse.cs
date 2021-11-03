using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Contracts.V1.Responses
{
    public class WishListResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<WishListBookResponse> WishListBooks { get; set; }
    }
}
