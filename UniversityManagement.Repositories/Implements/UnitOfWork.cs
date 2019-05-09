using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagement.Repositories.Implements
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Commits the synchronous.
        /// </summary>
        /// <returns>The commit result</returns>
        int Commit();

        /// <summary>
        ///     Commits the asynchronous.
        /// </summary>
        /// <returns>The commit result</returns>
        Task<int> CommitAsync();
    }
    public class UnitOfWork:IUnitOfWork
    {
        /// <summary>
        ///     The database context
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see>
        ///         <cref>UnitOfWork{TContext}</cref>
        ///     </see>
        ///     class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Commits the synchronous.
        /// </summary>
        /// <returns>
        ///     The commit result
        /// </returns>
        public int Commit()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        ///     Commits the asynchronous.
        /// </summary>
        /// <returns>
        ///     The commit result
        /// </returns>
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
