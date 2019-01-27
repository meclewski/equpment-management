using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public interface IDbRepository : IDisposable
    {
        IQueryable<TResult> GetAll<TResult>() where TResult : class;
        
        void Save<TEntity>(TEntity entity) where TEntity : class;
       
        TEntity Delete<TEntity>(int id) where TEntity : class;
    }
}
