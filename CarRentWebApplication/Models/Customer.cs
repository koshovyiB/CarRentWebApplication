using System;
using System.Collections.Generic;

namespace CarRentWebApplication
{
    public partial class Customer
    {
        public Customer()
        {
            Rentals = new HashSet<Rental>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
