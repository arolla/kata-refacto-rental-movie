package movierental

import org.scalatest.{FunSuite, Matchers}

class CustomerTest extends FunSuite with Matchers {

  test("testCustomer") {
    val c = new CustomerBuilder().build()
    c should not be null
  }

  test("testAddRental") {
    val customer2 = new CustomerBuilder().withName("Julia").build();
    val movie1 = new Movie("Gone with the Wind", Movie.REGULAR);
    val rental1 = new Rental(movie1, 3); // 3 day rental
    customer2.addRental(rental1);
  }

  test("testGetName") {
    val c = new Customer("David");
    c.getName() shouldEqual "David"
  }

  test("statementForRegularMovie") {
    val movie1 = new Movie("Gone with the Wind", Movie.REGULAR);
    val rental1 = new Rental(movie1, 3); // 3 day rental
    val customer2 =
      new CustomerBuilder()
        .withName("Sallie")
        .withRentals(rental1)
        .build();
    val expected = "Rental Record for Sallie\n" +
      "\tGone with the Wind\t3.5\n" +
      "Amount owed is 3.5\n" +
      "You earned 1 frequent renter points";
    val statement = customer2.statement();
    statement shouldEqual expected
  }

  test("statementForNewReleaseMovie") {
    val movie1 = new Movie("Star Wars", Movie.NEW_RELEASE);
    val rental1 = new Rental(movie1, 3); // 3 day rental
    val customer2 =
      new CustomerBuilder()
        .withName("Sallie")
        .withRentals(rental1)
        .build();
    val expected = "Rental Record for Sallie\n" +
      "\tStar Wars\t9.0\n" +
      "Amount owed is 9.0\n" +
      "You earned 2 frequent renter points";
    val statement = customer2.statement();
    statement shouldEqual expected
  }

  test("statementForChildrensMovie") {
    val movie1 = new Movie("Madagascar", Movie.CHILDRENS);
    val rental1 = new Rental(movie1, 3); // 3 day rental
    val customer2
      = new CustomerBuilder()
      .withName("Sallie")
      .withRentals(rental1)
      .build();
    val expected = "Rental Record for Sallie\n" +
      "\tMadagascar\t1.5\n" +
      "Amount owed is 1.5\n" +
      "You earned 1 frequent renter points";
    val statement = customer2.statement();
    statement shouldEqual expected
  }

  test("statementForManyMovies") {
    val movie1 = new Movie("Madagascar", Movie.CHILDRENS);
    val rental1 = new Rental(movie1, 6); // 6 day rental
    val movie2 = new Movie("Star Wars", Movie.NEW_RELEASE);
    val rental2 = new Rental(movie2, 2); // 2 day rental
    val movie3 = new Movie("Gone with the Wind", Movie.REGULAR);
    val rental3 = new Rental(movie3, 8); // 8 day rental
    val customer1
      = new CustomerBuilder()
      .withName("David")
      .withRentals(rental1, rental2, rental3)
      .build();
    val expected = "Rental Record for David\n" +
      "\tMadagascar\t6.0\n" +
      "\tStar Wars\t6.0\n" +
      "\tGone with the Wind\t11.0\n" +
      "Amount owed is 23.0\n" +
      "You earned 4 frequent renter points";
    val statement = customer1.statement();
    statement shouldEqual expected;
  }

  //TODO make test for price breaks in code.
}
