using SecPortal.Commons.Infrastructures;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using SecPortal.Webapp.Resources;
using Microsoft.Extensions.Localization;
using SecPortal.Commons.Constant;
using SecPortal.Services.Managers.LoggerManager;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class BaseGetSingleQuery<TQuery, TService, TEntity, TMapTo>
        where TQuery : IRequest<BaseResponse>, IIdentifier
        where TEntity : class, IBaseEntities<int>
        where TService : class, ICrudService<TEntity>
    {
        protected readonly TService _baseService;
        protected readonly IDataContext _context;
        protected readonly ILoggerManager _logger;
        protected readonly IStringLocalizer<Resource> _localizer;

        public BaseGetSingleQuery(TService baseService, IDataContext context, IStringLocalizer<Resource> localizer, ILoggerManager logger)
        {
            _baseService = baseService;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _localizer = localizer;
            _logger = logger;
        }

        public virtual async Task<BaseResponse> Handle(TQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _baseService.GetAsync(x => x.Id == request.Id && x.IsActive);
                if (entity == null)
                {
                    return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.NOT_FOUND]);
                }
                else
                {
                    entity.CheckRelation(_context);
                    var map = _baseService.MapModelToViewModel<TMapTo>(entity);
                    return BaseResponse.Factory.BuildSuccessResponse(1, map);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(request.GetType().Name, ex, request);
                return BaseResponse.Factory.BuildFailedResponse(ex.Message);
            }
        }
    }
}
