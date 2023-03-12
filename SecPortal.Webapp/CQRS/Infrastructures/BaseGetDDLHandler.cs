using SecPortal.Commons.ViewModels;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SecPortal.Services.Managers.LoggerManager;
using Microsoft.Extensions.Localization;
using SecPortal.Webapp.Resources;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class BaseGetDDLHandler<TCommand, TService, TEntity> : BaseHandler<TCommand, TService, TEntity, BaseResponse>, IRequestHandler<TCommand, BaseResponse>
        where TCommand : IRequest<BaseResponse>
        where TEntity : class, IBaseEntities<int>, IKeyValueCandidates
        where TService : class, ICrudService<TEntity>
    {
        public BaseGetDDLHandler(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(baseService, context, httpContextAccessor, localizer, logger)
        {

        }

        public async Task<BaseResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entities = (await _baseService.GetAllAsync(x => x.IsActive)).ToList().Select(x => new KeyValuePair<int>(x.Id, x.Name)).ToList();
                return BaseResponse.Factory.BuildSuccessResponse(entities.Count, entities);
            }
            catch (Exception ex)
            {
                _logger.LogException(request.GetType().Name, ex, request);
                throw;
            }
        }
    }
}
