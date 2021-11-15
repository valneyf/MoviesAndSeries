using System.Collections.Generic;

namespace MoviesAndSeries.Classes
{
  public interface IEntityRepository<T>
  {
    List<T> toList();
    T ReturnByID(int id);
    void toInsert(T entity);
    void toDelete(int id);
    void toUpdate(int id, T entity);
    int NextID();
  }
}