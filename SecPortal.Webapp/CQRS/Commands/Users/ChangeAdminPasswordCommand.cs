using AutoMapper;
using SecPortal.Commons.Constant;
using SecPortal.Commons.Exceptions;
using SecPortal.Entities.Data;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Webapp.CQRS.Infrastructures;
using SecPortal.Webapp.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecPortal.Webapp.CQRS.Commands.Users
{
    public class ChangeAdminPasswordCommand : IRequest<BaseResponse>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Password { get; set; }

        public class ChangeAdminPasswordHandler : BaseHandler<ChangeAdminPasswordCommand, BaseResponse>,
           IRequestHandler<ChangeAdminPasswordCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public ChangeAdminPasswordHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer,
                UserManager<ApplicationUser> userManager, ILoggerManager logger, IMapper mapper)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<BaseResponse> Handle(ChangeAdminPasswordCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    string errorMessage = string.Empty;
                    var entity = _context.Users.Single(x => x.Id == request.Id);
                    ApplicationUser applicationUser = await _userManager.FindByEmailAsync(entity.Email);

                    if (applicationUser != null)
                    {
                        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
                        var result = await _userManager.ResetPasswordAsync(applicationUser, resetToken, request.Password);
                        if (result.Succeeded)
                        {
                            return BaseResponse.Factory.BuildSuccessResponse(_localizer[errorMessage]);
                        }
                        else
                        {
                            errorMessage = "incorrect old password";
                        }
                    }
                    else
                    {
                        errorMessage = "user not found";
                    }

                    return BaseResponse.Factory.BuildFailedResponse(_localizer[errorMessage]);

                }
                catch (Exception ex)
                {
                    _logger.LogException(nameof(ChangeAdminPasswordHandler), ex, request);
                    throw new CustomException(_localizer[LocalizationConstant.GENERIC_ERROR]);
                }
            }
        }
    }
}
