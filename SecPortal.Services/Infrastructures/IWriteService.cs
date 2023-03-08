using SecPortal.Entities.Infrastructures;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SecPortal.Services.Infrastructures
{
    public interface IWriteService<TEntity> where TEntity : IBaseEntities<int>
    {
        /// <summary>
        /// Add's an item to the repository
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        TEntity Add(TEntity item);

        /// <summary>
        /// Removes the item from the list
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity item);

        /// <summary>
        /// Delete all items that match a certain condition
        /// </summary>
        /// <param name="predicate"></param>
        Task DeleteAllAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
