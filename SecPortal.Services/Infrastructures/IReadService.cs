using SecPortal.Entities.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SecPortal.Services.Infrastructures
{
    public interface IReadService<TEntity> where TEntity : IBaseEntities<Guid>
    {
        /// <summary>
        /// Get the first item that meets the criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get the items that meets the criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy">Order by needs to be exact with database column name</param>
        /// <param name="dir">Asc or Desc</param>
        /// <param name="includes"></param>
        /// <returns></returns>
        /// 

        IEnumerable<TEntity> GetAllSorted(Expression<Func<TEntity, bool>> predicate, string orderBy = "", int pageNo = 1, int recordsPerPage = 20, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAllSorted(string predicate, string orderBy = "", int pageNo = 1, int recordsPerPage = 20, params Expression<Func<TEntity, object>>[] includes);

        int GetAllSortedCount(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        int GetAllSortedCount(string predicate, params Expression<Func<TEntity, object>>[] includes);
        /// <summary>
        /// Get all items of type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, object>>[] includes);
    }

    public interface IReadServiceAsync<TEntity> where TEntity : IBaseEntities<Guid>
    {
        /// <summary>
        /// Get the first item that meets the criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get the items that meets the criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get all items of type T
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    }
}
