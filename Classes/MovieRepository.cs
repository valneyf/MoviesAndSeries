using System.Collections.Generic;

namespace MoviesAndSeries.Classes
{
  public class MovieRepository : IEntityRepository<Movie>
  {
    private List<Movie> listOfMovies = new List<Movie>();

    public List<Movie> toList()
    {
      return listOfMovies;
    }

    public void toInsert(Movie entity)
    {
      listOfMovies.Add(entity);
    }

    public void toUpdate(int id, Movie entity)
    {
      listOfMovies[id] = entity;
    }

    public void toDelete(int id)
    {
      listOfMovies[id].RemoveMovie();
    }

    public Movie ReturnByID(int id)
    {
      return listOfMovies[id];
    }

    public int NextID()
    {
      return listOfMovies.Count;
    }
  }
}