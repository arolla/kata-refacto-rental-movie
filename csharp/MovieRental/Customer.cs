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
                frequentRenterPoints = ComputeFrequentRenterPoints(frequentRenterPoints, rental);
                // show figures for this rental
                result = AppendRentalFigure(result, rental, thisAmount);
                totalAmount += thisAmount;
            }
            // add footer lines
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points";
            return result;
        }

        private static string AppendRentalFigure(string result, Rental rental, double thisAmount)
        {
            result += "\t" + rental.GetMovie().GetTitle() + "\t" + thisAmount + "\n";
            return result;
        }

        private static int ComputeFrequentRenterPoints(int frequentRenterPoints, Rental rental)
        {
            // add frequent renter points
            frequentRenterPoints++;
            // add bonus for a two day new release rental
            if (rental.GetMovie().GetPriceCode() == Movie.NEW_RELEASE && rental.GetDaysRented() > 1)
            {
                frequentRenterPoints++;
            }

            return frequentRenterPoints;
        }

        /// <summary>
        /// Determines movie rental amount from its price code
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        private static double CalculateMovieRentalAmount(Rental rental)
        {
            switch (rental.GetMovie().GetPriceCode())
            {
                case Movie.REGULAR:
                    return CalculateRegularMoviePrice(rental);

                case Movie.NEW_RELEASE:
                    return CalculateNewReleaseMoviePrice(rental);

                case Movie.CHILDRENS:
                    return CalculateChildrenMoviePrice(rental);
                    
                default:
                    // TODO: check with PO if this case is correct or if we should throw.
                    return 0;
            }
        }

        private static double CalculateChildrenMoviePrice(Rental rental)
        {
            double thisAmount = 1.5;
            if (rental.GetDaysRented() > 3)
            {
                thisAmount += (rental.GetDaysRented() - 3) * 1.5;
            }

            return thisAmount;
        }

        private static double CalculateNewReleaseMoviePrice(Rental rental)
        {
            return rental.GetDaysRented() * 3;
        }

        private static double CalculateRegularMoviePrice(Rental rental)
        {
            double thisAmount = 2;
            if (rental.GetDaysRented() > 2)
            {
                thisAmount += (rental.GetDaysRented() - 2) * 1.5;
            }

            return thisAmount;
        }
    }
}