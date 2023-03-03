using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SecPortal.Commons.Constant;
using SecPortal.Commons.Infrastructures;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Webapp.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class BaseIsShownHandler<TCommand, TService, TEntity, TResponse> : BaseHandler<TCommand, TService, TEntity, BaseResponse>
       where TCommand : IRequest<BaseResponse>, IIdentifier
       where TEntity : class, IBaseEntities<Guid>, IShownEntities
       where TService : class, ICrudService<TEntity>
    {
        public BaseIsShownHandler(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(baseService, context, httpContextAccessor, localizer, logger)
        {

        }

        public async Task<BaseResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _baseService.GetAsync(x => x.Id == request.Id && x.IsActive);
                if (entity == null)
                {
                    return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.NOT_FOUND]);
                }

                entity.IsShown = !entity.IsShown;
                await _context.SaveChangesAsync(UserId);

                return BaseResponse.Factory.BuildSuccessResponse(_localizer[DefaultConstant.OK]);
            }
            catch (Exception ex)
            {
                _logger.LogException(request.GetType().Name, ex, request);
                return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
            }
        }
    }
}
