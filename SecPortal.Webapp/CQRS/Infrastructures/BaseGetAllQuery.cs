using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecPortal.Services.Managers.LoggerManager;
using Microsoft.Extensions.Localization;
using SecPortal.Commons.Constant;
using SecPortal.Webapp.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class BaseGetAllQuery<TCommand, TService, TEntity, TResponse> : BaseHandler<TCommand, TService, TEntity, BaseResponse>
        where TCommand : IRequest<BaseResponse>, IBaseQueries, IFilterQuery
        where TEntity : class, IBaseEntities<int>
        where TService : class, ICrudService<TEntity>
    {
        public BaseGetAllQuery(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(baseService, context, httpContextAccessor, localizer, logger)
        {

        }

        protected virtual void HandleBeforeExecute(TCommand request)
        {

        }

        public async virtual Task<BaseResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await Task.FromResult(0);

                HandleBeforeExecute(request);

                var result = new List<TResponse>();
                int pageNo = request.PageNo;
                if (request.PageNo < 1)
                {
                    pageNo = 1;
                }

                int recordsPerPage = request.RecordsPerPage;
                if (request.RecordsPerPage < 1)
                {
                    recordsPerPage = 20;
                }

                var queryHolder = request.ConstructQuery();
                IEnumerable<TEntity> entities;
                int count;
                if (string.IsNullOrWhiteSpace(queryHolder))
                {
                    entities = _baseService.GetAllSorted(x => true, request.SortBy, pageNo, recordsPerPage);
                    count = _baseService.GetAllSortedCount(x => true);
                }
                else
                {
                    entities = _baseService.GetAllSorted(queryHolder, request.SortBy, pageNo, recordsPerPage);
                    count = _baseService.GetAllSortedCount(queryHolder);
                }

                foreach (var entity in entities)
                {
                    result.Add(_baseService.MapModelToViewModel<TResponse>(entity));
                }

                var response = BaseResponse.Factory.BuildSuccessResponse<IEnumerable<TResponse>>(count, result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(request.GetType().Name, ex, request);
                return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
            }
        }
    }

    public class BaseGetSystemQuery<TCommand, TEntity, TResponse>
        : BaseHandler<TCommand, BaseResponse>
        where TCommand : IRequest<BaseResponse>
        where TEntity : class, ISecPortalSystemEntity
    {
        DbSet<TEntity> _repository;

        public BaseGetSystemQuery(IDataContext context, DbSet<TEntity> repository, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(context, httpContextAccessor, localizer, logger)
        {
            _repository = repository;
        }

        public async virtual Task<BaseResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await Task.FromResult(0);

                var entities = await _repository.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToListAsync();
                var response = BaseResponse.Factory.BuildSuccessResponse<IEnumerable<KeyValuePair<int, string>>>(entities.Count(), entities);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(request.GetType().Name, ex, request);
                return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
            }
        }
    }
}
