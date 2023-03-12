using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Webapp.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using System.Threading;
using Bogus;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class BaseFakerHandler<TCommand, TService, TEntity, TResponse> : BaseHandler<TCommand, TService, TEntity, TResponse>
         where TCommand : IRequest<TResponse>
          where TEntity : class, IBaseEntities<int>
          where TService : class, ICrudService<TEntity>
        where TResponse : BaseResponse
    {

        protected int _itemCount = 100;
        public BaseFakerHandler(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(baseService, context, httpContextAccessor, localizer, logger)
        {

        }

        public virtual async Task<BaseResponse> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var faker = new Faker();
                for (int i = 0; i < _itemCount; i++)
                {
                    var entity = GenerateEntity(faker);
                    _baseService.Add(entity);
                }

                _context.SaveChanges(UserId);

                return BaseResponse.Factory.BuildSuccessResponse();
            }
            catch (Exception ex)
            {
                Type typeParameterType = typeof(TCommand);
                _logger.LogException(typeParameterType.Name, ex, command);
                throw;
            }
        }

        public virtual TEntity GenerateEntity(Faker faker)
        {
            throw new NotImplementedException();
        }
    }
}
