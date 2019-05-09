using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagement.Entities.ViewModels;
using UniversityManagement.Repositories.Implements;
using UniversityManagement.Repositories.Poco;

namespace UniversityManagement.Services.Implements
{
    public interface IBaseService<TEntity, TViewModel> : IEntityBaseService<TEntity> where TEntity : BaseEntity where TViewModel : BaseViewModel
    {
        IQueryable<TViewModel> GetAll();
        TViewModel GetById(int id);
        Task<TViewModel> GetByIdAsync(int id);
        bool Add(TViewModel viewModel);
        Task<bool> AddAsync(TViewModel viewModel);
        bool Update(TViewModel viewModel);
        Task<bool> UpdateAsync(TViewModel viewModel);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
        bool Delete(TViewModel viewModel);
        Task<bool> DeleteAsync(TViewModel viewModel);
    }
    public class BaseService<TEntity, TViewModel> : EntityBaseService<TEntity>, IBaseService<TEntity, TViewModel>  where TEntity : BaseEntity where TViewModel : BaseViewModel
    {
        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository) : base(unitOfWork, repository)
        {

        }
        public IQueryable<TViewModel> GetAll()
        {
            try
            {
                return GetAllEntity().ProjectTo<TViewModel>();
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
    }
}
