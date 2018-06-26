package movierental

class Movie(var title: String, var priceCode: Int) {

  def getPriceCode(): Int = {
    return priceCode
  }

  def setPriceCode(arg: Int): Unit = {
    this.priceCode = arg
  }

  def getTitle(): String = {
    return title
  }


}

object Movie {
  val CHILDRENS: Int = 2
  val NEW_RELEASE: Int = 1
  val REGULAR: Int = 0
}
