using SecPortal.Commons.Constant;
using SecPortal.Entities.Data;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
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
    public class ResetPasswordCommand : IRequest<BaseResponse>
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IDataContext _context;
            private readonly ILoggerManager _logger;
            private readonly IStringLocalizer<Resource> _localizer;

            public ResetPasswordHandler(UserManager<ApplicationUser> userManager, IDataContext context, ILoggerManager logger, IStringLocalizer<Resource> localizer)
            {
                _userManager = userManager;
                _context = context;
                _logger = logger;
                _localizer = localizer;
            }

            public async Task<BaseResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(x => x.Email == request.Email);
                    if (user == null)
                    {
                        return BaseResponse.Factory.BuildSuccessResponse();
                    }

                    var response = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
                    if (response.Succeeded)
                    {
                        // the user wants to log in and change his/her preferences, we need to send email and notification
                        _context.SaveChangesWithoutTracking();
                        return BaseResponse.Factory.BuildSuccessResponse();
                    }

                    return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
                }
                catch (System.Exception ex)
                {
                    _logger.LogException(nameof(ResetPasswordCommand), ex, request);
                    return BaseResponse.Factory.BuildFailedResponse(_localizer[DefaultConstant.GENERIC_ERROR]);
                }
            }
        }
    }
}
