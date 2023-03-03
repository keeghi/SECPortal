using SecPortal.Commons.Constant;
using SecPortal.Commons.Enums;
using SecPortal.Entities.Data;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Services.Services.MailingServices;
using SecPortal.Webapp.CQRS.Infrastructures;
using SecPortal.Webapp.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecPortal.Webapp.CQRS.Commands.Users
{
    public class ForgotPasswordCommand : IRequest<BaseResponse>
    {
        public string Email { get; set; }

        public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IDataContext _context;
            private readonly IMailingService _mailingService;
            private readonly ILoggerManager _logger;
            private readonly IStringLocalizer<Resource> _localizer;

            public ForgotPasswordHandler(IDataContext context, IMailingService mailingService, ILoggerManager logger, IStringLocalizer<Resource> localizer, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _mailingService = mailingService;
                _logger = logger;
                _localizer = localizer;
                _userManager = userManager;
            }

            public async Task<BaseResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await Task.FromResult(0);

                    var user = _context.Users.FirstOrDefault(x => x.Email == request.Email);
                    if (user == null)
                    {
                        return BaseResponse.Factory.BuildSuccessResponse();
                    }

                    if (await _userManager.IsInRoleAsync(user, RolesEnum.SuperAdmin.ToString()) || await _userManager.IsInRoleAsync(user, RolesEnum.Admin.ToString()))
                    {
                        return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
                    }

                    await _mailingService.SendForgotPasswordEmail(user.Id);
                    return BaseResponse.Factory.BuildSuccessResponse();
                }
                catch (System.Exception ex)
                {
                    _logger.LogException(request.GetType().Name, ex, request);
                    return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
                }
            }
        }
    }
}
