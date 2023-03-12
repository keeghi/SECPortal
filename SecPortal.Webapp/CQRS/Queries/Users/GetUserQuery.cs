using SecPortal.Commons.Infrastructures;
using SecPortal.Commons.ViewModels.UserViewModels;
using SecPortal.Entities.Data;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Services.Services.UserServices;
using SecPortal.Webapp.CQRS.Infrastructures;
using SecPortal.Webapp.Resources;
using MediatR;
using Microsoft.Extensions.Localization;
using System;

namespace SecPortal.Webapp.CQRS.Queries.Users
{
    public class GetUserQuery : IRequest<BaseResponse>, IIdentifier
    {
        public Guid Id { get; set; }

        public class GetUserHandler : BaseGetSingleQuery<GetUserQuery, IUserService, ApplicationUser, GetUserResponse>,
            IRequestHandler<GetUserQuery, BaseResponse>
        {
            public GetUserHandler(IUserService baseService, IDataContext context, IStringLocalizer<Resource> localizer, ILoggerManager logger)
                : base(baseService, context, localizer, logger)
            {

            }
        }
    }
}
