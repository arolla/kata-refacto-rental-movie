using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MovieRental.Tests
{
    [TestClass]
    public class CustomerTest
    {
        public CustomerTest()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("fr");
        }

        [TestMethod]
        public void TestCustomer()
        {
            Customer c = new CustomerBuilder().build();
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void TestAddRental()
        {
            Customer customer2 = new CustomerBuilder().withName("Julia").build();
            Movie movie1 = new Movie("Gone with the Wind", Movie.REGULAR);
            Rental rental1 = new Rental(movie1, 3); // 3 day rental
            customer2.AddRental(rental1);
        }

        [TestMethod]
        public void TestGetName()
        {
            Customer c = new Customer("David");
            Assert.AreEqual("David", c.GetName());
        }
        [TestMethod]
        public void StatementForRegularMovie()
        {
            Movie movie1 = new Movie("Gone with the Wind", Movie.REGULAR);
            Rental rental1 = new Rental(movie1, 3); // 3 day rental
            Customer customer2 =
                    new CustomerBuilder()
                            .withName("Sallie")
                            .withRentals(rental1)
                            .build();
            string expected = "Rental Record for Sallie\n" +
                    "\tGone with the Wind\t3,5\n" +
                    "Amount owed is 3,5\n" +
                    "You earned 1 frequent renter points";
            string statement = customer2.PrintRentalRecord();
            Assert.AreEqual(expected, statement);
        }

        [TestMethod]
        public void StatementForNewReleaseMovie()
        {
            Movie movie1 = new Movie("Star Wars", Movie.NEW_RELEASE);
            Rental rental1 = new Rental(movie1, 3); // 3 day rental
            Customer customer2 =
                    new CustomerBuilder()
                            .withName("Sallie")
                            .withRentals(rental1)
                            .build();
            string expected = "Rental Record for Sallie\n" +
                    "\tStar Wars\t9\n" +
                    "Amount owed is 9\n" +
                    "You earned 2 frequent renter points";
            string statement = customer2.PrintRentalRecord();
            Assert.AreEqual(expected, statement);
        }

        [TestMethod]
        public void StatementForChildrensMovie()
        {
            Movie movie1 = new Movie("Madagascar", Movie.CHILDRENS);
            Rental rental1 = new Rental(movie1, 3); // 3 day rental
            Customer customer2
                    = new CustomerBuilder()
                    .withName("Sallie")
                    .withRentals(rental1)
                    .build();
            string expected = "Rental Record for Sallie\n" +
                    "\tMadagascar\t1,5\n" +
                    "Amount owed is 1,5\n" +
                    "You earned 1 frequent renter points";
            string statement = customer2.PrintRentalRecord();
            Assert.AreEqual(expected, statement);
        }

        [TestMethod]
        public void StatementForManyMovies()
        {
            Movie movie1 = new Movie("Madagascar", Movie.CHILDRENS);
            Rental rental1 = new Rental(movie1, 6); // 6 day rental
            Movie movie2 = new Movie("Star Wars", Movie.NEW_RELEASE);
            Rental rental2 = new Rental(movie2, 2); // 2 day rental
            Movie movie3 = new Movie("Gone with the Wind", Movie.REGULAR);
            Rental rental3 = new Rental(movie3, 8); // 8 day rental
            Customer customer1
                    = new CustomerBuilder()
                    .withName("David")
                    .withRentals(rental1, rental2, rental3)
                    .build();
            string expected = "Rental Record for David\n" +
                    "\tMadagascar\t6\n" +
                    "\tStar Wars\t6\n" +
                    "\tGone with the Wind\t11\n" +
                    "Amount owed is 23\n" +
                    "You earned 4 frequent renter points";
            string statement = customer1.PrintRentalRecord();
            Assert.AreEqual(expected, statement);
        }

        //TODO make test for price breaks in code.
    }

}
