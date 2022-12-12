using System.Collections.Generic;

namespace MovieRental
{
    public class Customer
    {
        private readonly string _name;
        private readonly List<Rental> _rentals = new List<Rental>();
        public Customer(string name)
        {
            _name = name;
        }
        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }
        public string GetName()
        {
            return _name;
        }
        public string PrintRentalRecord()
        {
            double totalAmount = 0;
            var frequentRenterPoints = 0;
            var result = "Rental Record for " + GetName() + "\n";
            foreach (var rental in _rentals)
            {
                var thisAmount = CalculateMovieRentalAmount(rental);

                // add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if (rental.getMovie().getPriceCode() == Movie.NEW_RELEASE && rental.getDaysRented() > 1)
                {
                    frequentRenterPoints++;
                }
                // show figures for this rental
                result += "\t" + rental.getMovie().getTitle() + "\t" + thisAmount + "\n";
                totalAmount += thisAmount;
            }
            // add footer lines
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points";
            return result;
        }

        private static double CalculateMovieRentalAmount(Rental rental)
        {
            double thisAmount = 0;

            //determine amounts for each line
            switch (rental.getMovie().getPriceCode())
            {
                case Movie.REGULAR:
                    return CalculateRegularMoviePrice(rental);

                case Movie.NEW_RELEASE:
                    return CalculateNewReleaseMoviePrice(rental);

                case Movie.CHILDRENS:
                    return CalculateChildrenMoviePrice(rental);
            }

            return thisAmount;
        }

        private static double CalculateChildrenMoviePrice(Rental rental)
        {
            double thisAmount = 1.5;
            if (rental.getDaysRented() > 3)
            {
                thisAmount += (rental.getDaysRented() - 3) * 1.5;
            }

            return thisAmount;
        }

        private static double CalculateNewReleaseMoviePrice(Rental rental)
        {
            return rental.getDaysRented() * 3;
        }

        private static double CalculateRegularMoviePrice(Rental rental)
        {
            double thisAmount = 2;
            if (rental.getDaysRented() > 2)
            {
                thisAmount += (rental.getDaysRented() - 2) * 1.5;
            }

            return thisAmount;
        }
    }
}