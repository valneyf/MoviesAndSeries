using System;
using MoviesAndSeries;

namespace MoviesAndSeries
{
  public class Movie : BaseEntity
  {
    private string Title { get; set; }
    private int Year { get; set; }
    private Genre Genre { get; set; }
    private string Situation { get; set; }

    public Movie(int id, string title, int year, Genre genre, string situation)
    {
      this.ID = id;
      this.Title = title;
      this.Year = year;
      this.Genre = genre;
      this.Situation = "Ativo";
    }

    public override string ToString()
    {
      string retorno = "";
      retorno += "Título: " + Title + Environment.NewLine;
      retorno += "Ano de Lançamento: " + Year + Environment.NewLine;
      retorno += "Gênero: " + Genre + Environment.NewLine;
      retorno += "Situação: " + Situation;
      return retorno;
    }

    public string returnTitle()
    {
      return Title;
    }

    public int returnID()
    {
      return ID;
    }

    public string returnSituation()
    {
      return Situation;
    }

    public void RemoveMovie()
    {
      Situation = "Removido";
    }
  }
}