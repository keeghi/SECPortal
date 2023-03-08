using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using Microsoft.Extensions.Localization;
using SecPortal.Commons.Constant;
using SecPortal.Webapp.Resources;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class BaseGetShownHandler<TCommand, TService, TEntity, TResponse> : BaseHandler<TCommand, TService, TEntity, BaseResponse>
        where TCommand : IRequest<BaseResponse>
        where TEntity : class, IBaseEntities<int>, IShownEntities
        where TService : class, ICrudService<TEntity>
    {
        public BaseGetShownHandler(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(baseService, context, httpContextAccessor, localizer, logger)
        {

        }

        public virtual async Task<BaseResponse> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = new List<TResponse>();
                var entities = await _baseService.GetAllAsync(x => x.IsShown && x.IsActive);

                foreach (var entity in entities)
                {
                    entity.CheckRelation(_context);
                    result.Add(_baseService.MapModelToViewModel<TResponse>(entity));
                }

                var response = BaseResponse.Factory.BuildSuccessResponse<IEnumerable<TResponse>>(entities.Count(), result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(command.GetType().Name, ex, command);
                return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
            }
        }
    }
}
