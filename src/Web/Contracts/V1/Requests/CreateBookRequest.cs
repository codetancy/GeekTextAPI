using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Requests
{
    public class CreateBookRequest
    {
        /// <summary>
        /// Title of the book.
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// The ISBN code of the book.
        /// </summary>
        public string Isbn { get; init; }

        /// <summary>
        /// The synopsis of the book.
        /// </summary>
        public string Synopsis { get; init; }

        /// <summary>
        /// The unit price of the book.
        /// </summary>
        public decimal UnitPrice { get; init; }

        /// <summary>
        /// The number of copies sold of the book.
        /// </summary>
        public int CopiesSold { get; init; }

        /// <summary>
        /// The publication date of the book.
        /// </summary>
        public string PublicationDate { get; init; }

        /// <summary>
        /// The genre of the book.
        /// </summary>
        public string Genre { get; init; }

        /// <summary>
        /// The publisher of the book.
        /// </summary>
        public string Publisher { get; init; }

        /// <summary>
        /// The rating of the book.
        /// </summary>
        public decimal Rating { get; init; }

        /// <summary>
        /// The IDs of the book's author(s).
        /// </summary>
        public List<Guid> AuthorsIds { get; init; }
    }


}
