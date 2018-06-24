import {Customer} from "../../../main/typescript/movierental/customer";
import {Rental} from "../../../main/typescript/movierental/rental";

const NAME = "Roberts";

export class CustomerBuilder {
    private name: string = NAME;
    private rentals: Rental[] = [];

    public build(): Customer {
        const result = new Customer(this.name);
        for (const rental of this.rentals) {
            result.addRental(rental);
        }
        return result;
    }

    public withName(name: string): CustomerBuilder {
        this.name = name;
        return this;
    }

    public withRentals(...rentals: Rental[]): CustomerBuilder {
        this.rentals.push(...rentals);
        return this;
    }
}
