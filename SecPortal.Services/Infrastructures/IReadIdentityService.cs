using SecPortal.Entities.Infrastructures;
using System;
using System.Threading.Tasks;

namespace SecPortal.Services.Infrastructures
{
    public interface IReadIdentityService<TEntity, in TIdentity>
       where TEntity : IBaseEntities<Guid>
    {
        /// <summary>
        /// Get the item with this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(TIdentity id);
    }

    public interface IReadIdentityServiceAsync<TEntity, in TIdentity>
        where TEntity : IBaseEntities<Guid>
    {
        /// <summary>
        /// Get the item with this id using asynchronous processing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TIdentity id);
    }
}
