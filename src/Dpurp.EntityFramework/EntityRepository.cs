﻿using Dpurp.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Dpurp.EntityFramework
{
    public class EntityRepository<TItem> : IRepository<TItem> where TItem : class
    {
        private readonly EntityDataContext _dataContext;
        private DbSet<TItem> _entities;

        public EntityRepository(EntityDataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException("dataContext");
            _dataContext.RegisterEntityType(typeof(TItem));
        }

        protected virtual DbSet<TItem> DbEntities
        {
            get
            {
                return _entities ?? (_entities = _dataContext.Set<TItem>());
            }
        }

        public IQueryable<TItem> Entities { get { return DbEntities; } }

        // TODO: remove
        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public IEnumerable<TItem> Find(Func<TItem, bool> predicate)
        {
            return DbEntities.Where(predicate);
        }

        public TItem Get(int id)
        {
            return DbEntities.Find(id);
        }

        public IEnumerable<TItem> GetAll()
        {
            return DbEntities.AsEnumerable();
        }

        public void Add(TItem item)
        {
            DbEntities.Add(item);
            SaveChanges(); // TODO: remove
        }

        public void AddRange(IEnumerable<TItem> items)
        {
            DbEntities.AddRange(items);
            SaveChanges(); // TODO: remove
        }

        public void Remove(TItem item)
        {
            DbEntities.Remove(item);
            SaveChanges(); // TODO: remove
        }

        public void RemoveRange(IEnumerable<TItem> items)
        {
            DbEntities.RemoveRange(items);
            SaveChanges(); // TODO: remove
        }

        public void Update(TItem item)
        {
            SaveChanges(); // TODO: remove
        }
    }
}
