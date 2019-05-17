using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagement.Repositories.Implements;
using UniversityManagement.Repositories.Poco;
using UniversityManagement.Utilities;

namespace UniversityManagement.Services.Implements
{
    public interface IEntityBaseService<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAllEntity(Func<TEntity, bool> preCondition = null, List<SortParameter<TEntity>> sorting = null, PagingParameter paging = null);
        TEntity GetEntityById(int id);
        Task<TEntity> GetEntityByIdAsync(int id);
        TEntity GetEntityBy(Func<TEntity, bool> preCondition = null);
        bool AddEntity(TEntity entity);
        bool AddEntity(IEnumerable<TEntity> entities);
        Task<bool> AddEntityAsync(TEntity entity);
        Task<bool> AddEntityAsync(IEnumerable<TEntity> entities);
        bool UpdateEntity(TEntity entity);
        bool UpdateEntity(IEnumerable<TEntity> entities);
        Task<bool> UpdateEntityAsync(TEntity entity);
        Task<bool> UpdateEntityAsync(IEnumerable<TEntity> entities);
        bool DeleteEntity(int id);
        bool DeleteEntity(IEnumerable<int> ids);
        bool DeleteEntity(TEntity entity);
        bool DeleteEntity(IEnumerable<TEntity> entities);
        bool DeleteEntityBy(Func<TEntity, bool> preCondition = null);
        Task<bool> DeleteEntityAsync(int id);
        Task<bool> DeleteEntityAsync(IEnumerable<int> ids);
        Task<bool> DeleteEntityAsync(TEntity entity);
        Task<bool> DeleteEntityAsync(IEnumerable<TEntity> entities);
        Task<bool> DeleteEntityByAsync(Func<TEntity, bool> preCondition = null);
        bool HardDeleteEntity(int id);
        bool HardDeleteEntity(IEnumerable<int> ids);
        bool HardDeleteEntity(TEntity entity);
        bool HardDeleteEntity(IEnumerable<TEntity> entities);
        bool HardDeleteEntityBy(Func<TEntity, bool> preCondition = null);
        Task<bool> HardDeleteEntityAsync(int id);
        Task<bool> HardDeleteEntityAsync(IEnumerable<int> ids);
        Task<bool> HardDeleteEntityAsync(TEntity entity);
        Task<bool> HardDeleteEntityAsync(IEnumerable<TEntity> entities);
        Task<bool> HardDeleteEntityByAsync(Func<TEntity, bool> preCondition = null);

    }
    public class EntityBaseService<TEntity> : IEntityBaseService<TEntity> where TEntity : BaseEntity
    {
        protected EntityBaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        protected IUnitOfWork UnitOfWork { get; }
        protected IBaseRepository<TEntity> Repository { get; }

        public IQueryable<TEntity> GetAllEntity(Func<TEntity, bool> preCondition = null, List<SortParameter<TEntity>> sorting = null, PagingParameter paging = null)
        {
            try
            {
                var listEntity = Repository.TableNoTracking;
                if(preCondition != null)
                {
                    listEntity = listEntity.Where(preCondition).AsQueryable();
                }
                if(sorting != null)
                {
                    listEntity = listEntity.OrderBy(sorting);
                }
                if(paging != null)
                {
                    listEntity = listEntity.Skip(paging.PageSize * (paging.Page - 1)).Take(paging.PageSize);
                }
                return listEntity;
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

        public TEntity GetEntityBy(Func<TEntity, bool> preCondition = null)
        {
            try
            {
                var listEntity = Repository.Table;
                if (preCondition != null)
                {
                    listEntity = listEntity.Where(preCondition).AsQueryable();
                }
                return listEntity.OrderByDescending(e=>e.Id).FirstOrDefault();
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

        public bool AddEntity(IEnumerable<TEntity> entities)
        {
            try
            {
                Repository.Add(entities);
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

        public async Task<bool> AddEntityAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                Repository.Add(entities);
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

        public bool UpdateEntity(IEnumerable<TEntity> entities)
        {
            try
            {
                Repository.Update(entities);
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

        public async Task<bool> UpdateEntityAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                Repository.Update(entities);
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

        public bool DeleteEntity(IEnumerable<int> ids)
        {
            try
            {
                Repository.Delete(ids);
                int result = UnitOfWork.Commit();
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

        public bool DeleteEntity(IEnumerable<TEntity> entities)
        {
            try
            {
                Repository.Delete(entities);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteEntityBy(Func<TEntity, bool> preCondition = null)
        {
            try
            {
                IQueryable<TEntity> entities = GetAllEntity(preCondition);
                Repository.Delete(entities);
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

        public async Task<bool> DeleteEntityAsync(IEnumerable<int> ids)
        {
            try
            {
                Repository.Delete(ids);
                int result = await UnitOfWork.CommitAsync();
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

        public async Task<bool> DeleteEntityAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                Repository.Delete(entities);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteEntityByAsync(Func<TEntity, bool> preCondition = null)
        {
            try
            {
                IQueryable<TEntity> entities = GetAllEntity(preCondition);
                Repository.Delete(entities);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HardDeleteEntity(int id)
        {
            try
            {
                Repository.HardDelete(id);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HardDeleteEntity(IEnumerable<int> ids)
        {
            try
            {
                Repository.HardDelete(ids);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HardDeleteEntity(TEntity entity)
        {
            try
            {
                Repository.HardDelete(entity);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HardDeleteEntity(IEnumerable<TEntity> entities)
        {
            try
            {
                Repository.HardDelete(entities);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HardDeleteEntityBy(Func<TEntity, bool> preCondition = null)
        {
            try
            {
                IQueryable<TEntity> entities = GetAllEntity(preCondition);
                Repository.HardDelete(entities);
                int result = UnitOfWork.Commit();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteEntityAsync(int id)
        {
            try
            {
                Repository.HardDelete(id);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteEntityAsync(IEnumerable<int> ids)
        {
            try
            {
                Repository.HardDelete(ids);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteEntityAsync(TEntity entity)
        {
            try
            {
                Repository.HardDelete(entity);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteEntityAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                Repository.HardDelete(entities);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteEntityByAsync(Func<TEntity, bool> preCondition = null)
        {
            try
            {
                IQueryable<TEntity> entities = GetAllEntity(preCondition);
                Repository.HardDelete(entities);
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
