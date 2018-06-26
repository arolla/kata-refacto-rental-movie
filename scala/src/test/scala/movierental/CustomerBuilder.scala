package movierental

import scala.collection.mutable

class CustomerBuilder {
  var name: String = CustomerBuilder.NAME
  val rentals: mutable.MutableList[Rental] = mutable.MutableList[Rental]()

  def build(): Customer = {
    val result = new Customer(name)
    for (each <- rentals) {
      result.addRental(each)
    }
    return result
  }

  def withName(name: String): CustomerBuilder = {
    this.name = name
    return this
  }

  def withRentals(rentals: Rental*): CustomerBuilder = {
    this.rentals ++= rentals
    return this
  }
}

object CustomerBuilder {
  val NAME = "Roberts"
}