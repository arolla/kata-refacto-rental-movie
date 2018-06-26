package movierental

import scala.collection.mutable
import scala.util.control.Breaks._

class Customer(var name: String, var rentals: mutable.MutableList[Rental] = mutable.MutableList[Rental]()) {


  def addRental(arg: Rental): Unit = {
    rentals += arg
  }

  def getName(): String = {
    return name
  }

  def statement(): String = {
    var totalAmount: Double = 0
    var frequentRenterPoints: Int = 0
    var result = "Rental Record for " + getName() + "\n"

    for (each <- rentals) {

      var thisAmount: Double = 0

      //determine amounts for each line
      breakable {
        each.getMovie().getPriceCode() match {
          case Movie.REGULAR => {
            thisAmount += 2
            if (each.getDaysRented() > 2)
              thisAmount += (each.getDaysRented() - 2) * 1.5
            break
          }
          case Movie.NEW_RELEASE => {
            thisAmount += each.getDaysRented() * 3
            break
          }
          case Movie.CHILDRENS => {
            thisAmount += 1.5
            if (each.getDaysRented() > 3)
              thisAmount += (each.getDaysRented() - 3) * 1.5
            break
          }
        }
      }

      // add frequent renter points
      frequentRenterPoints += 1
      // add bonus for a two day new release rental
      if ((each.getMovie().getPriceCode() == Movie.NEW_RELEASE) && each.getDaysRented() > 1)
        frequentRenterPoints += 1

      // show figures for this rental
      result += "\t" + each.getMovie().getTitle() + "\t" + String.valueOf(thisAmount) + "\n"
      totalAmount += thisAmount
    }

    // add footer lines
    result += "Amount owed is " + String.valueOf(totalAmount) + "\n"
    result += "You earned " + String.valueOf(frequentRenterPoints) + " frequent renter points"

    return result
  }

}
