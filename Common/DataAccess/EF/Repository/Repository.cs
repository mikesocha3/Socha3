using Socha3.Common.DataAccess.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Socha3.Common.DataAccess.EF.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbContext Context { get; set; }


        public IQueryable<T> ObjectSet
        {
            get
            {
                return Context.Set<T>().AsQueryable();
            }
        }
        public void Attach(T entity)
        {
            Context.Set<T>().Attach(entity);
        }
        public DateTime GetCurrentServerTime()
        {
            return DateTime.Now;
        }
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        public T FirstOrDefault(Func<T, bool> where)
        {
            return ObjectSet.FirstOrDefault(where);
        }
        public IQueryable<T> Get(Guid id)
        {
            throw new NotImplementedException();
        }
        public IQueryable<T> Get()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> List()
        {
            throw new NotImplementedException();
        }
        public int Add(T entity)
        {
            Context.Set<T>().Add(entity);
            return 0;
        }
        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}