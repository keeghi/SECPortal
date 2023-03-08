using SecPortal.Commons.Constant;
using SecPortal.Commons.Infrastructures;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Webapp.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class BaseDeleteHandler<TCommand, TService, TEntity> : BaseHandler<TCommand, TService, TEntity, BaseResponse>
        where TCommand : IRequest<BaseResponse>, IIdentifier
        where TEntity : class, IBaseEntities<int>
        where TService : class, ICrudService<TEntity>
    {
        public BaseDeleteHandler(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(baseService, context, httpContextAccessor, localizer, logger)
        {

        }

        public virtual async Task<BaseResponse> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var entity = await _baseService.GetAsync(x => x.Id == command.Id);
            if (entity == null)
            {
                return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.NOT_FOUND]);
            }

            entity.IsActive = false;
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                //await _baseService.DeleteAsync(entity);
                await _context.SaveChangesAsync(UserId);

                var checkRelation = _baseService.Get(x => x.Id == entity.Id);
                checkRelation.CheckRelation(_context);

                return BaseResponse.Factory.BuildSuccessResponse(_localizer[DefaultConstant.OK]);
            }
            catch (Exception ex)
            {
                _logger.LogException(command.GetType().Name.ToString(), ex, command);
                return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.DATA_USED]);
            }
        }
    }
}
