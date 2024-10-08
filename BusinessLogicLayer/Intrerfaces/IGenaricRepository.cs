﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Intrerfaces
{
    public interface IGenaricRepository<TEntity> where TEntity : class
    {
        int Create(TEntity entity);
        int Delete(TEntity entity);
        TEntity? Get(int id);
        IEnumerable<TEntity> GetAll();
        int Update(TEntity entity);
    }
}
