using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagement.Repositories.Implements;
using UniversityManagement.Repositories.Poco;

namespace UniversityManagement.Services.Implements
{
    public interface IBaseService<TEntity, TViewModel> where TEntity : BaseEntity
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
    public class BaseService<TEntity, TViewModel> : IBaseService<TEntity, TViewModel> where TEntity : BaseEntity
    {
        protected BaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        protected IUnitOfWork UnitOfWork { get; }
        protected IBaseRepository<TEntity> Repository { get; }

        public IQueryable<TViewModel> GetAll()
        {
            try
            {
                return Repository.TableNoTracking.ProjectTo<TViewModel>();
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
                var entity = Repository.GetById(id);
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
                var entity = await Repository.GetByIdAsync(id);
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
                Repository.Add(entity);
                int result = UnitOfWork.Commit();
                return result > 0;
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
                Repository.Add(entity);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
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
                var entity = Mapper.Map<TEntity>(viewModel);
                Repository.Update(entity);
                int result = UnitOfWork.Commit();
                return result > 0;
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
                var entity = Mapper.Map<TEntity>(viewModel);
                Repository.Update(entity);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
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
                Repository.Delete(id);
                int result = UnitOfWork.Commit();
                return result > 0;
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
                Repository.Delete(id);
                int result = await UnitOfWork.CommitAsync();
                return result > 0;
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
                var entity = Mapper.Map<TEntity>(viewModel);
                Repository.Delete(entity);
                int result = UnitOfWork.Commit();
                return result > 0;
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
                var entity = Mapper.Map<TEntity>(viewModel);
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
