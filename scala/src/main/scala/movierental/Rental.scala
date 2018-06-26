package movierental

class Rental(var movie: Movie,
           var daysRented: Int) {

  def getDaysRented(): Int = {
    return this.daysRented
  }

  def getMovie(): Movie = {
    return this.movie
  }
}
