using SecPortal.Commons.Constant;
using SecPortal.Entities.Data;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Webapp.Authorizations;
using SecPortal.Webapp.CQRS.Infrastructures;
using SecPortal.Webapp.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecPortal.Webapp.CQRS.Commands.Users
{
    public class ActivateAccountCommand : IRequest<IBaseResponse>
    {
        public string Token { get; set; }
        public Guid Id { get; set; }

        public class ActivateAccountHandler : BaseHandler<ActivateAccountCommand, IBaseResponse>, IRequestHandler<ActivateAccountCommand, IBaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IAuthorization _authService;

            public ActivateAccountHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer, ILoggerManager logger, IAuthorization authService, UserManager<ApplicationUser> userManager)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _authService = authService;
                _userManager = userManager;
            }

            public async Task<IBaseResponse> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == request.Id);
                    if (user == null)
                    {
                        return BaseResponse.Factory.BuildFailedResponse("Failed to activate account, please contact support");
                    }

                    var response = await _userManager.ConfirmEmailAsync(user, request.Token);
                    if (response.Succeeded)
                    {
                        return BaseResponse.Factory.BuildSuccessResponse();
                    }
                    else
                    {
                        return BaseResponse.Factory.BuildFailedResponse("Failed to activate account, please contact support");
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogException(nameof(ActivateAccountCommand), ex, request);
                    return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
                }
            }
        }
    }
}
