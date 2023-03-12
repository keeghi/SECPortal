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
using System.Threading;
using System.Threading.Tasks;

namespace SecPortal.Webapp.CQRS.Commands.Users
{
    public class ChangeUserPasswordCommand : IRequest<BaseResponse>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

        public class ChangeUserPasswordHandler : BaseHandler<ChangeUserPasswordCommand, BaseResponse>,
            IRequestHandler<ChangeUserPasswordCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public ChangeUserPasswordHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer,
                UserManager<ApplicationUser> userManager, ILoggerManager logger, IMapper mapper)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<BaseResponse> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        string errorMessage = string.Empty;
                        ApplicationUser applicationUser = await _userManager.FindByNameAsync(request.Username);

                        if (applicationUser != null)
                        {
                            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(applicationUser, request.OldPassword);
                            if (isPasswordCorrect)
                            {
                                var status = await _userManager.ChangePasswordAsync(applicationUser, request.OldPassword, request.NewPassword);
                                if (status == IdentityResult.Success)
                                {
                                    _context.SaveChangesWithoutTracking();
                                    trans.Commit();

                                    return BaseResponse.Factory.BuildSuccessResponse(_localizer[errorMessage]);
                                }
                                else
                                {
                                    errorMessage = "change password failed";
                                }
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
                        trans.Rollback();
                        _logger.LogException(nameof(ChangeUserPasswordHandler), ex, request);
                        throw new CustomException(_localizer[LocalizationConstant.GENERIC_ERROR]);
                    }
                }
            }
        }
    }
}
