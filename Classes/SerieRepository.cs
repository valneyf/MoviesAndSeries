using System.Collections.Generic;

namespace MoviesAndSeries.Classes
{
  public class SerieRepository : IEntityRepository<Serie>
  {
    private List<Serie> listOfSeries = new List<Serie>();

    public List<Serie> toList()
    {
      return listOfSeries;
    }

    public void toInsert(Serie entity)
    {
      listOfSeries.Add(entity);
    }
    
    public void toUpdate(int id, Serie entity)
    {
      listOfSeries[id] = entity;
    }

    public void toDelete(int id)
    {
      listOfSeries[id].RemoveMedia();
    }    
    
    public Serie ReturnByID(int id)
    {
      return listOfSeries[id];
    }

    public int NextID()
    {
      return listOfSeries.Count;
    }
  }
}