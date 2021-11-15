using System;
using static System.Console;
using MoviesAndSeries;

namespace MoviesAndSeries
{
  public class Movie : BaseEntity
  {
    private string Title { get; set; }
    private int Year { get; set; }
    private Genre Genre { get; set; }
    private string Duration { get; set; }
    private bool Situation { get; set; }

    public Movie(int id, string title, int year, Genre genre, string duration)
    {
      this.ID = id;
      this.Title = title;
      this.Year = year;
      this.Genre = genre;
      this.Duration = duration;
      this.Situation = false;
    }

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

    public string getDuration()
    {
      return Duration;
    }

    public bool getSituation()
    {
      return Situation;
    }

    public void RemoveMovie()
    {
      Situation = true;
    }
  }
}