using SecPortal.Entities.Infrastructures;
using System;

namespace SecPortal.Services.Infrastructures
{
    public interface ICrudService<TEntity> : IAutoMapperService<TEntity>,
                                             IReadServiceAsync<TEntity>,
                                             IReadService<TEntity>,
                                             IReadIdentityServiceAsync<TEntity, Guid>,
                                             IReadIdentityService<TEntity, Guid>,
                                             IWriteService<TEntity>,
                                             IWriteIdentityServiceAsync<Guid> where TEntity : class, IBaseEntities<Guid>
    {
    }
}
