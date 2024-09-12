using Demo.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repository
{
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : class
    {
        private DbContext _dbContext;
        protected DbSet<TEntity> _entities;

        public GenaricRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }

        public int Create(TEntity entity)
        {
            _entities.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _entities.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public TEntity? Get(int id) => _entities.Find(id);
        public IEnumerable<TEntity> GetAll() => _entities.ToList();
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
