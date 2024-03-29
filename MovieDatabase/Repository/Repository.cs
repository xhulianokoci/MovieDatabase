﻿using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data;
using MovieDatabase.Repository.IRepository;
using System.Linq;
using System.Linq.Expressions;

namespace MovieDatabase.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RepositoryDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(RepositoryDbContext db)
        {
            _db = db;
            _db.Products.Include(u => u.Category);
            this.dbSet = _db.Set<T>();
        }

        //Add an entity to database
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //Get all records of an enitity from database
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties)
                {
                    query = query.Include(includeProperties);
                }
            }
            return query.ToList();
        }

        //Find object by id
        public T Find(int? id)
        {
            T entity = dbSet.Find(id);
            return entity;
        }

        //Get first value or default of an entity from database
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(includeProperties);
                }
            }
            return query.FirstOrDefault();
        }

        //Remove an entity from database
        public void Remove(T entity)
        {
           dbSet.Remove(entity);
        }

        //Remove a range of entitnies from database
        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        //Add Asynchronys for an entity 
        public async void AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Clear()
        {
            _db.ChangeTracker.Clear();
        }
    }
}
