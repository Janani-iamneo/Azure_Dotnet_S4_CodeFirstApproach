using System;
using System.Collections.Generic;

namespace dotnetapp.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Add this line to resolve the error
        public ICollection<Loan> Loans { get; set; }
    }
}
