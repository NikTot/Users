using System.Collections.Generic;

namespace SAU.Repositories
{
    public interface IRepository<TEntity, in TPkey> where TEntity : class
    {
        TEntity Get(TPkey id);
        IEnumerable<TEntity> GetByName(string name);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
