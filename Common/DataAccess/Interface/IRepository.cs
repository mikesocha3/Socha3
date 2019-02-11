using System;
using System.Collections.Generic;
using System.Linq;

namespace Socha3.Common.DataAccess.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> ObjectSet { get; }
        IQueryable<T> Get(Guid id);
        IQueryable<T> Get();        
        IEnumerable<T> List();
        int Add(T entity);
        bool Update(T entity);        
        bool Remove(int id);
        void Remove(T entity);
        DateTime GetCurrentServerTime();
        T FirstOrDefault(Func<T, bool> where);
        void SaveChanges();
    }
}
