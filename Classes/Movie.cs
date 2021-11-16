namespace MoviesAndSeries
{
  public class Movie : BaseEntity
  {    
    private string Duration { get; set; }    

    public Movie(int id, string title, int year, Genre genre, string duration)
    {
      this.ID = id;
      this.Title = title;
      this.Year = year;
      this.Genre = genre;
      this.Duration = duration;
      this.Situation = false;
    }

    public string getDuration()
    {
      return Duration;
    }
  }
}