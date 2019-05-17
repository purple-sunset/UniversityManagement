using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagement.Entities.ViewModels;
using UniversityManagement.Repositories.Implements;
using UniversityManagement.Repositories.Poco;
using UniversityManagement.Utilities;

namespace UniversityManagement.Services.Implements
{
    public interface IBaseService<TEntity, TViewModel> : IEntityBaseService<TEntity> where TEntity : BaseEntity where TViewModel : BaseViewModel
    {
        IQueryable<TViewModel> GetAllModel(Func<TEntity, bool> preCondition = null, List<SortParameter<TEntity>> preSorting = null, Func<TViewModel, bool> postCondition = null, List<SortParameter<TViewModel>> postSorting = null, PagingParameter paging = null);
        TViewModel GetModelById(int id);
        Task<TViewModel> GetModelByIdAsync(int id);
        TViewModel GetModelBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
        bool AddModel(TViewModel viewModel);
        bool AddModel(IEnumerable<TViewModel> viewModels);
        Task<bool> AddModelAsync(TViewModel viewModel);
        Task<bool> AddModelAsync(IEnumerable<TViewModel> viewModels);
        bool UpdateModel(TViewModel viewModel);
        bool UpdateModel(IEnumerable<TViewModel> viewModels);
        Task<bool> UpdateModelAsync(TViewModel viewModel);
        Task<bool> UpdateModelAsync(IEnumerable<TViewModel> viewModels);
        bool DeleteModel(int id);
        bool DeleteModel(IEnumerable<int> ids);
        bool DeleteModel(TViewModel viewModel);
        bool DeleteModel(IEnumerable<TViewModel> viewModels);
        bool DeleteModelBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
        Task<bool> DeleteModelAsync(int id);
        Task<bool> DeleteModelAsync(IEnumerable<int> ids);
        Task<bool> DeleteModelAsync(TViewModel viewModel);
        Task<bool> DeleteModelAsync(IEnumerable<TViewModel> viewModels);
        Task<bool> DeleteModelByAsync(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
        bool HardDeleteModel(int id);
        bool HardDeleteModel(IEnumerable<int> ids);
        bool HardDeleteModel(TViewModel viewModel);
        bool HardDeleteModel(IEnumerable<TViewModel> viewModels);
        bool HardDeleteByModel(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
        Task<bool> HardDeleteModelAsync(int id);
        Task<bool> HardDeleteModelAsync(IEnumerable<int> ids);
        Task<bool> HardDeleteModelAsync(TViewModel viewModel);
        Task<bool> HardDeleteModelAsync(IEnumerable<TViewModel> viewModels);
        Task<bool> HardDeleteModelByAsync(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null);
    }
    public class BaseService<TEntity, TViewModel> : EntityBaseService<TEntity>, IBaseService<TEntity, TViewModel>  where TEntity : BaseEntity where TViewModel : BaseViewModel
    {
        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository) : base(unitOfWork, repository)
        {

        }
        public IQueryable<TViewModel> GetAllModel(Func<TEntity, bool> preCondition = null, List<SortParameter<TEntity>> preSorting = null, Func<TViewModel, bool> postCondition = null, List<SortParameter<TViewModel>> postSorting = null, PagingParameter paging = null)
        {
            try
            {
                var listModels = GetAllEntity(preCondition, preSorting, paging).ProjectTo<TViewModel>();
                if(postCondition != null)
                {
                    listModels = listModels.Where(postCondition).AsQueryable();
                }
                if(postSorting != null)
                {
                    listModels = listModels.OrderBy(postSorting);
                }
                return listModels;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TViewModel GetModelById(int id)
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

        public async Task<TViewModel> GetModelByIdAsync(int id)
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
                IQueryable<TEntity> entities = GetAllEntity(preCondition).OrderByDescending(x=>x.Id);

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


        public bool AddModel(TViewModel viewModel)
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

        public bool AddModel(IEnumerable<TViewModel> viewModels)
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

        public async Task<bool> AddModelAsync(TViewModel viewModel)
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

        public async Task<bool> AddModelAsync(IEnumerable<TViewModel> viewModels)
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

        public bool UpdateModel(TViewModel viewModel)
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

        public bool UpdateModel(IEnumerable<TViewModel> viewModels)
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

        public async Task<bool> UpdateModelAsync(TViewModel viewModel)
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

        public async Task<bool> UpdateModelAsync(IEnumerable<TViewModel> viewModels)
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

        public bool DeleteModel(int id)
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

        public bool DeleteModel(IEnumerable<int> ids)
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

        public bool DeleteModel(TViewModel viewModel)
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

        public bool DeleteModel(IEnumerable<TViewModel> viewModels)
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

        public bool DeleteModelBy(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
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

        public async Task<bool> DeleteModelAsync(int id)
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

        public async Task<bool> DeleteModelAsync(IEnumerable<int> ids)
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

        public async Task<bool> DeleteModelAsync(TViewModel viewModel)
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

        public async Task<bool> DeleteModelAsync(IEnumerable<TViewModel> viewModels)
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

        public async Task<bool> DeleteModelByAsync(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
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


        public bool HardDeleteModel(int id)
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

        public bool HardDeleteModel(IEnumerable<int> ids)
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


        public bool HardDeleteModel(TViewModel viewModel)
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

        public bool HardDeleteModel(IEnumerable<TViewModel> viewModels)
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

        public bool HardDeleteByModel(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
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

        public async Task<bool> HardDeleteModelAsync(int id)
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

        public async Task<bool> HardDeleteModelAsync(IEnumerable<int> ids)
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


        public async Task<bool> HardDeleteModelAsync(TViewModel viewModel)
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

        public async Task<bool> HardDeleteModelAsync(IEnumerable<TViewModel> viewModels)
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

        public async Task<bool> HardDeleteModelByAsync(Func<TEntity, bool> preCondition = null, Func<TViewModel, bool> postCondition = null)
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
