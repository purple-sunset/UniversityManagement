using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagement.Repositories.Implements;
using UniversityManagement.Repositories.Poco;

namespace UniversityManagement.Services.Implements
{
    public interface IEntityBaseService<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAllEntity();
        TEntity GetEntityById(int id);
        Task<TEntity> GetEntityByIdAsync(int id);
        bool AddEntity(TEntity entity);
        Task<bool> AddEntityAsync(TEntity entity);
        bool UpdateEntity(TEntity entity);
        Task<bool> UpdateEntityAsync(TEntity entity);
        bool DeleteEntity(int id);
        Task<bool> DeleteEntityAsync(int id);
        bool DeleteEntity(TEntity entity);
        Task<bool> DeleteEntityAsync(TEntity entity);

    }
    public abstract class EntityBaseService<TEntity> : IEntityBaseService<TEntity> where TEntity : BaseEntity
    {
        protected EntityBaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        protected IUnitOfWork UnitOfWork { get; }
        protected IBaseRepository<TEntity> Repository { get; }

        public IQueryable<TEntity> GetAllEntity()
        {
            try
            {
                return Repository.TableNoTracking;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TEntity GetEntityById(int id)
        {
            try
            {
                return Repository.GetById(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            try
            {
                return await Repository.GetByIdAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddEntity(TEntity entity)
        {
            try
            {
                Repository.Add(entity);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddEntityAsync(TEntity entity)
        {
            try
            {
                Repository.Add(entity);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateEntity(TEntity entity)
        {
            try
            {
                Repository.Update(entity);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateEntityAsync(TEntity entity)
        {
            try
            {
                Repository.Update(entity);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteEntity(int id)
        {
            try
            {
                Repository.Delete(id);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteEntityAsync(int id)
        {
            try
            {
                Repository.Delete(id);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteEntity(TEntity entity)
        {
            try
            {
                Repository.Delete(entity);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteEntityAsync(TEntity entity)
        {
            try
            {
                Repository.Delete(entity);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
