using SecPortal.Commons.Infrastructures;
using SecPortal.Entities.Data;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Services.Services.UserServices;
using SecPortal.Webapp.CQRS.Infrastructures;
using SecPortal.Webapp.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecPortal.Webapp.CQRS.Users.Commands
{
    public class DeleteUserCommand : IRequest<BaseResponse>, IIdentifier
    {
        [Required]
        public Guid Id { get; set; }

        public class DeleteUserHandler : BaseDeleteHandler<DeleteUserCommand, IUserService, ApplicationUser>,
            IRequestHandler<DeleteUserCommand, BaseResponse>
        {
            public DeleteUserHandler(IUserService baseService, IDataContext context, IHttpContextAccessor httpContextAccessor,
                IStringLocalizer<Resource> localizer, ILoggerManager logger) : base(baseService, context, httpContextAccessor, localizer, logger)
            {

            }
        }
    }
}
