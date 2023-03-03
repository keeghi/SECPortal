using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Webapp.Resources;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class BaseCreateHandler<TCommand, TService, TEntity, TResponse> : BaseHandler<TCommand, TService, TEntity, TResponse>
        where TCommand : IRequest<TResponse>
        where TEntity : class, IBaseEntities<Guid>
        where TService : class, ICrudService<TEntity>
    {
        public BaseCreateHandler(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(baseService, context, httpContextAccessor, localizer, logger)
        {

        }

        public virtual async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _baseService.MapViewModelToModel(command);
                _baseService.Add(entity);
                await _context.SaveChangesAsync(UserId);

                var checkRelation = _baseService.Get(x => x.Id == entity.Id);
                checkRelation.CheckRelation(_context);

                return _baseService.MapModelToViewModel<TResponse>(entity);
            }
            catch (Exception ex)
            {
                Type typeParameterType = typeof(TCommand);
                _logger.LogException(typeParameterType.Name, ex, command);
                throw;
            }
        }
    }
}
