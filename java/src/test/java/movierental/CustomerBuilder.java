package movierental;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class CustomerBuilder {

    public static final String NAME = "Roberts";

    private String name = NAME;
    private final List<Rental> rentals = new ArrayList<>();

    public Customer build() {
        Customer result = new Customer(name);
        for (Rental rental : rentals) {
            result.addRental(rental);
        }
        return result;
    }

    public CustomerBuilder withName(String name) {
        this.name = name;
        return this;
    }

    public CustomerBuilder withRentals(Rental... rentals) {
        Collections.addAll(this.rentals, rentals);
        return this;
    }
}
