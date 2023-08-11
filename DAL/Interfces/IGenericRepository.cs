using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task<TEntity> GetEagerByChoice(Expression<Func<TEntity, bool>> predicate, List<string> include);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        //void DeleteRange(TEntity entity);
        void Update(TEntity entity, TEntity oldEntity = null);
        Task<List<TEntity>> GetAllEmployees(List<string> include);
        void DeleteRange(IEnumerable<TEntity> Entities);
        void Delete(int id);
    }
}
