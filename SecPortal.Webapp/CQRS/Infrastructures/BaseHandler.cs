using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Linq.Dynamic.Core;
using System.Linq;
using SecPortal.Entities.Entities;
using SecPortal.Commons.Exceptions;
using Microsoft.Extensions.Localization;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Webapp.Resources;
using SecPortal.Entities.Data;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    // WITHOUT ENTITY SERVICE
    public class BaseHandler<TCommand, TResponse>
        where TCommand : IRequest<TResponse>
    {
        protected readonly IDataContext _context;
        protected readonly IHttpContextAccessor _httpContext;
        protected readonly IStringLocalizer<Resource> _localizer;
        protected readonly ILoggerManager _logger;

        protected int? UserId
        {
            get
            {
                if (_httpContext.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    return int.Parse(_httpContext.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                }
                return null;
            }
        }
        protected User User
        {
            get
            {
                //var user = _context.Users.Single(x => x.Id == UserId);
                //if (user != null)
                //{
                //    return user;
                //}

                throw new CustomException("This user doesn't exists", 404);
            }
        }

        public BaseHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContext = httpContextAccessor;
            _localizer = localizer;
            _logger = logger;
        }
    }

    // WITH ENTITY SERVICE
    public class BaseHandler<TCommand, TService, TEntity, TResponse> : BaseHandler<TCommand, TResponse>

          where TCommand : IRequest<TResponse>
          where TEntity : class, IBaseEntities<int>
          where TService : class, ICrudService<TEntity>
    {
        protected readonly TService _baseService;

        public BaseHandler(TService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger)
            : base(context, httpContextAccessor, localizer, logger)
        {
            _baseService = baseService;
        }
    }
}
