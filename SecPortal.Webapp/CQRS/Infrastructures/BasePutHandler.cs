using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class BasePutHandler<TCommand, TService, TEntity, TResponse> : BaseHandler<TCommand, TService, TEntity, BaseResponse>
       where TCommand : IRequest<BaseResponse>, IIdentifier
       where TEntity : class, IBaseEntities<int>
       where TService : class, ICrudService<TEntity>
    {
        public BasePutHandler(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(baseService, context, httpContextAccessor, localizer, logger)
        {

        }

        public virtual async Task<BaseResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await _baseService.GetAsync(x => x.Id == request.Id);
            if (entity == null)
            {
                return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.NOT_FOUND]);
            }

            _context.Entry(entity).State = EntityState.Modified;
            _baseService.AssignValue(entity, request);

            try
            {
                await _context.SaveChangesAsync(UserId);

                var checkRelation = _baseService.Get(x => x.Id == entity.Id);

                return BaseResponse.Factory.BuildSuccessResponse(1, _baseService.MapModelToViewModel<TResponse>(entity));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogException(request.GetType().Name, ex, request);
                return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
            }
        }
    }
}
