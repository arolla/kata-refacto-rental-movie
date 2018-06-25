import {Customer} from "../../../main/typescript/movierental/customer";
import {Movie} from "../../../main/typescript/movierental/movie";
import {Rental} from "../../../main/typescript/movierental/rental";
import {CustomerBuilder} from "./customer-builder";

describe("Customer", () => {
    it("testCustomer", () => {
        const c = new CustomerBuilder().build();
        expect(c).toBeDefined();
    });

    it("testAddRental", () => {
        const customer2 = new CustomerBuilder().withName("Julia").build();
        const movie1 = new Movie("Gone with the Wind", Movie.REGULAR);
        const rental1 = new Rental(movie1, 3); // 3 day rental
        customer2.addRental(rental1);
    });

    it("testGetName", () => {
        const c = new Customer("David");
        expect(c.getName()).toBe("David");
    });

    it("statementForRegularMovie", () => {
        const movie1 = new Movie("Gone with the Wind", Movie.REGULAR);
        const rental1 = new Rental(movie1, 3); // 3 day rental
        const customer2 =
            new CustomerBuilder()
                .withName("Sallie")
                .withRentals(rental1)
                .build();
        const expected = "Rental Record for Sallie\n" +
            "\tGone with the Wind\t3.5\n" +
            "Amount owed is 3.5\n" +
            "You earned 1 frequent renter points";
        const statement = customer2.statement();
        expect(statement).toBe(expected);
    });

    it("statementForNewReleaseMovie", () => {

        const movie1 = new Movie("Star Wars", Movie.NEW_RELEASE);
        const rental1 = new Rental(movie1, 3); // 3 day rental
        const customer2 =
            new CustomerBuilder()
                .withName("Sallie")
                .withRentals(rental1)
                .build();
        const expected = "Rental Record for Sallie\n" +
            "\tStar Wars\t9.0\n" +
            "Amount owed is 9.0\n" +
            "You earned 2 frequent renter points";
        const statement = customer2.statement();
        expect(statement).toBe(expected);
    });

    it("statementForChildrensMovie", () => {

        const movie1 = new Movie("Madagascar", Movie.CHILDRENS);
        const rental1 = new Rental(movie1, 3); // 3 day rental
        const customer2
            = new CustomerBuilder()
            .withName("Sallie")
            .withRentals(rental1)
            .build();
        const expected = "Rental Record for Sallie\n" +
            "\tMadagascar\t1.5\n" +
            "Amount owed is 1.5\n" +
            "You earned 1 frequent renter points";
        const statement = customer2.statement();
        expect(statement).toBe(expected);
    });

    it("statementForManyMovies", () => {
        const movie1 = new Movie("Madagascar", Movie.CHILDRENS);
        const rental1 = new Rental(movie1, 6); // 6 day rental
        const movie2 = new Movie("Star Wars", Movie.NEW_RELEASE);
        const rental2 = new Rental(movie2, 2); // 2 day rental
        const movie3 = new Movie("Gone with the Wind", Movie.REGULAR);
        const rental3 = new Rental(movie3, 8); // 8 day rental
        const customer1 = new CustomerBuilder()
            .withName("David")
            .withRentals(rental1, rental2, rental3)
            .build();
        const
            expected = "Rental Record for David\n" +
                "\tMadagascar\t6.0\n" +
                "\tStar Wars\t6.0\n" +
                "\tGone with the Wind\t11.0\n" +
                "Amount owed is 23.0\n" +
                "You earned 4 frequent renter points";
        const
            statement = customer1.statement();
        expect(statement).toBe(expected);
    });

    // TODO make test for price breaks in code.
});
