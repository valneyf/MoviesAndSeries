namespace MoviesAndSeries
{
  public abstract class BaseEntity
  {
    public int ID { get; protected set; }
    public string Title { get; protected set; }
    public int Year { get; protected set; }
    public Genre Genre { get; protected set; }
    public bool Situation { get; protected set; }

    public int getID()
    {
      return ID;
    }

    public string getTitle()
    {
      return Title;
    }

    public int getYear()
    {
      return Year;
    }

    public Genre getGenre()
    {
      return Genre;
    }

    public bool getSituation()
    {
      return Situation;
    }

    public void RemoveMedia()
    {
      Situation = true;
    }
  }
}