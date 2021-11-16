namespace MoviesAndSeries
{
  public class Serie : BaseEntity
  {
    private int Season { get; set; }

    public Serie(int id, string title, int year, Genre genre, int season)
    {
      this.ID = id;
      this.Title = title;
      this.Year = year;
      this.Genre = genre;
      this.Season = season;
      this.Situation = false;
    }

    public int getSeason()
    {
      return Season;
    }
  }
}