namespace MoviesAndSeries
{
  public class Serie : BaseEntity
  {
    private string Season { get; set; }

    public Serie(int id, string title, int year, Genre genre, string season)
    {
      this.ID = id;
      this.Title = title;
      this.Year = year;
      this.Genre = genre;
      this.Season = season;
      this.Situation = false;
    }

    public string getSeason()
    {
      return Season;
    }
  }
}