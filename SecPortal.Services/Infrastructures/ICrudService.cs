using SecPortal.Entities.Infrastructures;
using System;

namespace SecPortal.Services.Infrastructures
{
    public interface ICrudService<TEntity> : IAutoMapperService<TEntity>,
                                             IReadServiceAsync<TEntity>,
                                             IReadService<TEntity>,
                                             IReadIdentityServiceAsync<TEntity, int>,
                                             IReadIdentityService<TEntity, int>,
                                             IWriteService<TEntity>,
                                             IWriteIdentityServiceAsync<int> where TEntity : class, IBaseEntities<int>
    {
    }
}
