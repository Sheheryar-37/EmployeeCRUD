using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
//using DAL.Interfces;

namespace DAL.Implementation
{
    public class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class
    {
        internal CRUDEntities context;
        internal DbSet<TEntity> DbSet;
        private bool disposed = false;

        public GenericRepository(CRUDEntities context)
        {
            this.context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual void Update(TEntity entityToUpdate, TEntity oldEntity = null)
        {
            //DbSet.Attach(entityToUpdate);
            if(oldEntity != null)
            {
                context.Entry(oldEntity).State = EntityState.Detached;
                this.DbSet.Attach(entityToUpdate);
                
            }
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        Task<TEntity> IGenericRepository<TEntity>.Get(int id)
        {
            return GetByIdAsync(id);
            
        }

        public async Task<TEntity> GetEagerByChoice(Expression<Func<TEntity, bool>> predicate, List<string> include)
        {
            //_ = DbSet.AsQueryable();

            IQueryable<TEntity> query = null;
            if (predicate == null)
                query = DbSet;
            else
            {
                query = DbSet.Where(predicate).AsQueryable();
            }


            if (include != null && include.Count > 0)
            {
                for (var i = 0; i < include.Count(); i++)
                {
                    query = query.Include(include[i]);
                }
            }
           
            //query.FirstOrDefault();
            return await query.FirstOrDefaultAsync();

            

        }

        public async virtual Task<TEntity> GetByIdAsync(object id)
        {
            return  await this.DbSet.FindAsync(id);
        }
        //Task<TEntity> IGenericRepository<TEntity>.GetAllEmployees(Expression<Func<TEntity, bool>> predicate)
        async Task<List<TEntity>> IGenericRepository<TEntity>.GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            if(predicate == null)
                return await this.DbSet.ToListAsync();
            else
                return await this.DbSet.Where(predicate).ToListAsync();
        }
        async Task<List<TEntity>> IGenericRepository<TEntity>.GetAllEmployees(List<string> include)
        {
            IQueryable<TEntity> query = null;
            query = DbSet;
            if (include != null && include.Count > 0)
            {
                for (var i = 0; i < include.Count(); i++)
                {
                    query = query.Include(include[i]);
                }
            }

            return await query.ToListAsync();
            //return await this.DbSet.ToListAsync();
          
        }

        void IGenericRepository<TEntity>.Add(TEntity entity)
        {
           DbSet.Add(entity);
        }

        void IGenericRepository<TEntity>.Delete(TEntity entity)
        {
            //TEntity entityToDelete = GetById(id);

            DbSet.Remove(entity);
        }
        void IGenericRepository<TEntity>.Delete(int id)
        {
            TEntity entityToDelete = GetById(id);
            if(entityToDelete!= null)
                DbSet.Remove(entityToDelete);
        }
        void IGenericRepository<TEntity>.DeleteRange(IEnumerable<TEntity> entity)
        {
            DbSet.RemoveRange(entity);
        }
        public virtual TEntity GetById(object id)
        {

            return (TEntity)this.DbSet.Find(id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }
            }
            this.disposed = true;
        }

        
        

        public CRUDEntities Context
        {
            set
            {
                this.context = value; //new isomEntities();
                this.DbSet = context.Set<TEntity>();

            }
            get
            {
                return this.context;

            }
        }
    }
}
