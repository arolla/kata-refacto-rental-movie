from movierental.customer import Customer
from movierental.movie import Movie
from movierental.rental import Rental


def test_add_rental():
    customer = Customer("Julia")
    movie = Movie("Gone with the Wind", price_code=Movie.REGULAR)
    rental = Rental(movie, days_rented=3)
    customer.rentals.append(rental)

    assert len(customer.rentals) == 1


def test_statement_for_regular_movie():
    customer = Customer("Sallie")
    movie = Movie("Gone with the Wind", price_code=Movie.REGULAR)
    rental = Rental(movie, days_rented=3)
    customer.rentals.append(rental)

    expected = (
        "Rental Record for Sallie\n"
        + "\tGone with the Wind\t3.5\n"
        + "Amount owed is 3.5\n"
        + "You earned 1 frequent renter points"
    )

    actual = customer.statement()

    assert actual == expected


def test_statement_for_new_release_movie():
    customer = Customer("Sallie")
    movie = Movie("Star Wars", price_code=Movie.NEW_RELEASE)
    rental = Rental(movie, days_rented=3)
    customer.rentals.append(rental)

    expected = (
        "Rental Record for Sallie\n"
        + "\tStar Wars\t9.0\n"
        + "Amount owed is 9.0\n"
        + "You earned 2 frequent renter points"
    )

    actual = customer.statement()

    assert actual == expected


def test_statement_for_childrens_movie():
    customer = Customer("Sallie")
    movie = Movie("Madagascar", price_code=Movie.CHILDRENS)
    rental = Rental(movie, days_rented=3)
    customer.rentals.append(rental)

    expected = (
        "Rental Record for Sallie\n"
        + "\tMadagascar\t1.5\n"
        + "Amount owed is 1.5\n"
        + "You earned 1 frequent renter points"
    )

    actual = customer.statement()

    assert actual == expected


def test_statement_for_many_movies():
    movie1 = Movie("Madagascar", price_code=Movie.CHILDRENS)
    rental1 = Rental(movie1, days_rented=6)
    movie2 = Movie("Star Wars", price_code=Movie.NEW_RELEASE)
    rental2 = Rental(movie2, days_rented=2)
    movie3 = Movie("Gone with the Wind", price_code=Movie.REGULAR)
    rental3 = Rental(movie3, days_rented=8)

    customer = Customer("David")
    customer.rentals = [rental1, rental2, rental3]

    expected = (
        "Rental Record for David\n"
        + "\tMadagascar\t6.0\n"
        + "\tStar Wars\t6.0\n"
        + "\tGone with the Wind\t11.0\n"
        + "Amount owed is 23.0\n"
        + "You earned 4 frequent renter points"
    )

    assert customer.statement() == expected
