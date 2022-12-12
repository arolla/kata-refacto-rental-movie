using System;
using System.Collections.Generic;

namespace MovieRental.Tests
{
    public class CustomerBuilder
    {

        public static readonly String NAME = "Roberts";

    private String name = NAME;
        private List<Rental> rentals = new List<Rental>();

        public Customer build()
        {
            Customer result = new Customer(name);
            foreach (Rental rental in rentals)
            {
                result.AddRental(rental);
            }
            return result;
        }

        public CustomerBuilder withName(String name)
        {
            this.name = name;
            return this;
        }

        public CustomerBuilder withRentals(params Rental[] rentals)
        {
            this.rentals.AddRange(rentals);
            return this;
        }
    }

}
