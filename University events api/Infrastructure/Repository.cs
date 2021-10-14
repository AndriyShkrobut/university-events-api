using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly UniversityEventsContext _context;
        protected DbSet<TEntity> _entities;

        public Repository(UniversityEventsContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> GetAll()
        {
            return _entities.AsQueryable();
        }
        public virtual async Task<TEntity> FindByIdAsync(params object[] keys)
        {
            return await _entities.FindAsync(keys);
        }
        public virtual async Task<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate)
        {

            return await _entities.FirstOrDefaultAsync(predicate);
        }
        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }
        public virtual void AddRange(IEnumerable<TEntity> entity)
        {
            _entities.AddRange(entity);
        }
        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }
        public virtual void RemoveRange(IEnumerable<TEntity> entity)
        {
            _entities.RemoveRange(entity);
        }
        public virtual void Update(TEntity entity)
        {
            _entities.Update(entity);
        }
        public virtual async Task Update(TEntity entity, IEnumerable<string> fieldMasks)
        {
            var entry = _context.Entry(entity);
            var collectionList = new List<CollectionEntry>();
            var oldEntity = await _entities.FindAsync(entry.Property("Id").CurrentValue);
            var oldEntry = _context.Entry(oldEntity);

            var collectionFieldMasks = fieldMasks.Where(name => entry.Collections.Any(a => a.Metadata.Name == name));
            var propertyFieldMasks = fieldMasks.Where(name => entry.Properties.Any(a => a.Metadata.Name == name));

            foreach (var name in collectionFieldMasks)
            {
                var oldCollection = _context.Entry(oldEntity).Collection(name);
                await oldCollection.LoadAsync();
                collectionList.Add(oldCollection);
            }

            if (oldEntry != null)
            {
                oldEntry.State = EntityState.Detached;
            }

            foreach (var collection in collectionList)
            {
                foreach (var item in collection.CurrentValue)
                {
                    var newEntry = _context.Entry(item);
                    newEntry.State = EntityState.Deleted;
                }
            }

            foreach (var name in collectionFieldMasks)
            {
                var newCollection = entry.Collections.Single(a => a.Metadata.Name == name);
                foreach (var item in newCollection.CurrentValue)
                {
                    var newEntry = _context.Entry(item);
                    newEntry.State = EntityState.Added;
                }
            }

            foreach (var name in propertyFieldMasks)
            {
                entry.Property(name).IsModified = true;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _entities = null;
                }
                _context?.Dispose();
                _disposedValue = true;
            }
        }
        ~Repository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


    }
}
