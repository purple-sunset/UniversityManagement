using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagement.Entities.ViewModels;
using UniversityManagement.Repositories.Implements;
using UniversityManagement.Repositories.Poco;

namespace UniversityManagement.Services.Implements
{
    public interface IBaseService<TEntity, TViewModel> : IEntityBaseService<TEntity> where TEntity : BaseEntity where TViewModel : BaseViewModel
    {
        IQueryable<TViewModel> GetAll(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null, int? page = null, int? pageSize = null);
        TViewModel GetById(int id);
        Task<TViewModel> GetByIdAsync(int id);
        TViewModel GetModelBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
        bool Add(TViewModel viewModel);
        bool Add(IEnumerable<TViewModel> viewModels);
        Task<bool> AddAsync(TViewModel viewModel);
        Task<bool> AddAsync(IEnumerable<TViewModel> viewModels);
        bool Update(TViewModel viewModel);
        bool Update(IEnumerable<TViewModel> viewModels);
        Task<bool> UpdateAsync(TViewModel viewModel);
        Task<bool> UpdateAsync(IEnumerable<TViewModel> viewModels);
        bool Delete(int id);
        bool Delete(IEnumerable<int> ids);
        bool Delete(TViewModel viewModel);
        bool Delete(IEnumerable<TViewModel> viewModels);
        bool DeleteBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(IEnumerable<int> ids);
        Task<bool> DeleteAsync(TViewModel viewModel);
        Task<bool> DeleteAsync(IEnumerable<TViewModel> viewModels);
        Task<bool> DeleteByAsync(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
        bool HardDelete(int id);
        bool HardDelete(IEnumerable<int> ids);
        bool HardDelete(TViewModel viewModel);
        bool HardDelete(IEnumerable<TViewModel> viewModels);
        bool HardDeleteBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
        Task<bool> HardDeleteAsync(int id);
        Task<bool> HardDeleteAsync(IEnumerable<int> ids);
        Task<bool> HardDeleteAsync(TViewModel viewModel);
        Task<bool> HardDeleteAsync(IEnumerable<TViewModel> viewModels);
        Task<bool> HardDeleteByAsync(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
    }
    public class BaseService<TEntity, TViewModel> : EntityBaseService<TEntity>, IBaseService<TEntity, TViewModel>  where TEntity : BaseEntity where TViewModel : BaseViewModel
    {
        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository) : base(unitOfWork, repository)
        {

        }
        public IQueryable<TViewModel> GetAll(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null, int? page = null, int? pageSize = null)
        {
            try
            {
                var listModels = GetAllEntity(preCondition, page, pageSize).ProjectTo<TViewModel>();
                if(postCondition != null)
                {
                    listModels = listModels.Where(postCondition).AsQueryable();
                }
                return listModels;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TViewModel GetById(int id)
        {
            try
            {
                var entity = GetEntityById(id);
                return Mapper.Map<TViewModel>(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TViewModel> GetByIdAsync(int id)
        {
            try
            {
                var entity = await GetEntityByIdAsync(id);
                return Mapper.Map<TViewModel>(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TViewModel GetModelBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
        {
            try
            {
                IQueryable<TEntity> entities = GetAllEntity(preCondition);

                if(postCondition != null)
                {
                    IQueryable<TViewModel> listModel = entities.ProjectTo<TViewModel>();
                    return listModel.FirstOrDefault(postCondition);
                }
                else
                {
                    TEntity entity = entities.FirstOrDefault();
                    return Mapper.Map<TViewModel>(entity);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool Add(TViewModel viewModel)
        {
            try
            {
                var entity = Mapper.Map<TEntity>(viewModel);
                return AddEntity(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Add(IEnumerable<TViewModel> viewModels)
        {
            try
            {
                var entities = viewModels.AsQueryable().ProjectTo<TEntity>();
                return AddEntity(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddAsync(TViewModel viewModel)
        {
            try
            {
                var entity = Mapper.Map<TEntity>(viewModel);
                return await AddEntityAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddAsync(IEnumerable<TViewModel> viewModels)
        {
            try
            {
                var entities = viewModels.AsQueryable().ProjectTo<TEntity>();
                return await AddEntityAsync(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(TViewModel viewModel)
        {
            try
            {
                var entity = GetEntityById(viewModel.Id);
                Mapper.Map<TViewModel, TEntity>(viewModel, entity);
                return UpdateEntity(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(IEnumerable<TViewModel> viewModels)
        {
            try
            {
                var ids = viewModels.Select(m => m.Id);
                var entities = GetAllEntity(e=>ids.Contains(e.Id));
                foreach(var entity in entities)
                {
                    var viewModel = viewModels.FirstOrDefault(m => m.Id == entity.Id);
                    Mapper.Map<TViewModel, TEntity>(viewModel, entity);
                }
                
                return UpdateEntity(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAsync(TViewModel viewModel)
        {
            try
            {
                var entity = await GetEntityByIdAsync(viewModel.Id);
                Mapper.Map<TViewModel, TEntity>(viewModel, entity);
                return await UpdateEntityAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAsync(IEnumerable<TViewModel> viewModels)
        {
            try
            {
                var ids = viewModels.Select(m => m.Id);
                var entities = GetAllEntity(e => ids.Contains(e.Id));
                foreach (var entity in entities)
                {
                    var viewModel = viewModels.FirstOrDefault(m => m.Id == entity.Id);
                    Mapper.Map<TViewModel, TEntity>(viewModel, entity);
                }

                return await UpdateEntityAsync(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return DeleteEntity(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(IEnumerable<int> ids)
        {
            try
            {
                return DeleteEntity(ids);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(TViewModel viewModel)
        {
            try
            {
                var entity = GetEntityById(viewModel.Id);
                return DeleteEntity(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(IEnumerable<TViewModel> viewModels)
        {
            try
            {
                var ids = viewModels.Select(m => m.Id);
                var entities = GetAllEntity(e => ids.Contains(e.Id));
                
                return DeleteEntity(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
        {
            try
            {
                var entities = GetAllEntity(preCondition);

                if(postCondition != null)
                {
                    var viewModels = entities.ProjectTo<TViewModel>();
                    viewModels = viewModels.Where(postCondition).AsQueryable();
                    var ids = viewModels.Select(m => m.Id);
                    entities = GetAllEntity(e => ids.Contains(e.Id));
                }

                return DeleteEntity(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await DeleteEntityAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            try
            {
                return await DeleteEntityAsync(ids);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(TViewModel viewModel)
        {
            try
            {
                var entity = await GetEntityByIdAsync(viewModel.Id);
                return await DeleteEntityAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(IEnumerable<TViewModel> viewModels)
        {
            try
            {
                var ids = viewModels.Select(m => m.Id);
                var entities = GetAllEntity(e => ids.Contains(e.Id));

                return await DeleteEntityAsync(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteByAsync(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
        {
            try
            {
                var entities = GetAllEntity(preCondition);

                if (postCondition != null)
                {
                    var viewModels = entities.ProjectTo<TViewModel>();
                    viewModels = viewModels.Where(postCondition).AsQueryable();
                    var ids = viewModels.Select(m => m.Id);
                    entities = GetAllEntity(e => ids.Contains(e.Id));
                }

                return await DeleteEntityAsync(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool HardDelete(int id)
        {
            try
            {
                return HardDeleteEntity(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HardDelete(IEnumerable<int> ids)
        {
            try
            {
                return HardDeleteEntity(ids);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool HardDelete(TViewModel viewModel)
        {
            try
            {
                var entity = GetEntityById(viewModel.Id);
                return HardDeleteEntity(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HardDelete(IEnumerable<TViewModel> viewModels)
        {
            try
            {
                var ids = viewModels.Select(m => m.Id);
                var entities = GetAllEntity(e => ids.Contains(e.Id));

                return HardDeleteEntity(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HardDeleteBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
        {
            try
            {
                var entities = GetAllEntity(preCondition);

                if (postCondition != null)
                {
                    var viewModels = entities.ProjectTo<TViewModel>();
                    viewModels = viewModels.Where(postCondition).AsQueryable();
                    var ids = viewModels.Select(m => m.Id);
                    entities = GetAllEntity(e => ids.Contains(e.Id));
                }

                return HardDeleteEntity(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteAsync(int id)
        {
            try
            {
                return await HardDeleteEntityAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteAsync(IEnumerable<int> ids)
        {
            try
            {
                return await HardDeleteEntityAsync(ids);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> HardDeleteAsync(TViewModel viewModel)
        {
            try
            {
                var entity = await GetEntityByIdAsync(viewModel.Id);
                return await HardDeleteEntityAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteAsync(IEnumerable<TViewModel> viewModels)
        {
            try
            {
                var ids = viewModels.Select(m => m.Id);
                var entities = GetAllEntity(e => ids.Contains(e.Id));

                return await HardDeleteEntityAsync(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HardDeleteByAsync(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
        {
            try
            {
                var entities = GetAllEntity(preCondition);

                if (postCondition != null)
                {
                    var viewModels = entities.ProjectTo<TViewModel>();
                    viewModels = viewModels.Where(postCondition).AsQueryable();
                    var ids = viewModels.Select(m => m.Id);
                    entities = GetAllEntity(e => ids.Contains(e.Id));
                }

                return await HardDeleteEntityAsync(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
