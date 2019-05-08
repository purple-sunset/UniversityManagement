﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagement.Repositories.Poco;

namespace UniversityManagement.Repositories.Implements
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        ///     Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        ///     Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only
        ///     operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        TEntity GetById(int id);

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        ///     Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Add(TEntity entity);

        /// <summary>
        ///     Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        ///     Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Delete entity
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        ///     Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);

        /// <summary>
        ///     Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<TEntity> entities);
    }
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Ctor

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Fields

        private readonly DbContext _context;
        private DbSet<TEntity> _entities;

        #endregion

        #region Methods

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public TEntity GetById(int id)
        {
            return Entities.FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

        /// <summary>
        ///     Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        /// <summary>
        ///     Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                entity.CreatedAt = DateTime.Now;
                entity.IsDeleted = false;
                Entities.Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public void Add(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (TEntity entity in entities)
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.IsDeleted = false;
                    Entities.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                entity.UpdatedAt = DateTime.Now;
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public void Update(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (TEntity entity in entities)
                {
                    entity.UpdatedAt = DateTime.Now;
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Delete entity
        /// </summary>
        /// <param name="id">Entity</param>
        public void Delete(int id)
        {
            try
            {
                TEntity entity = GetById(id);
                if (entity == null)
                    throw new ArgumentNullException(nameof(id));

                entity.UpdatedAt = DateTime.Now;
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        ///     Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                entity.UpdatedAt = DateTime.Now;
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public void Delete(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (TEntity entity in entities)
                {
                    entity.UpdatedAt = DateTime.Now;
                    entity.IsDeleted = true;
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a table
        /// </summary>
        public IQueryable<TEntity> Table
        {
            get { return Entities.Where(x => !x.IsDeleted); }
        }

        /// <summary>
        ///     Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only
        ///     operations
        /// </summary>
        public IQueryable<TEntity> TableNoTracking
        {
            get { return Entities.AsNoTracking().Where(x => !x.IsDeleted); }
        }

        /// <summary>
        ///     Entities
        /// </summary>
        private DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());

        #endregion
    }
}
