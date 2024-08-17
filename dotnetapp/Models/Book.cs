using System;
using System.Collections.Generic;

namespace dotnetapp.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int GenreID { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }

        // Navigation properties
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        
        // Add this line to resolve the error
        public ICollection<Loan> Loans { get; set; }
    }
}
