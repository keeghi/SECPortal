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
    public class ChangeUserProfileCommand : IRequest<BaseResponse>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public class ChangeUserProfileHandler : BaseHandler<ChangeUserProfileCommand, BaseResponse>,
            IRequestHandler<ChangeUserProfileCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public ChangeUserProfileHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer,
                UserManager<ApplicationUser> userManager, ILoggerManager logger, IMapper mapper)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<BaseResponse> Handle(ChangeUserProfileCommand request, CancellationToken cancellationToken)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        string errorMessage = string.Empty;
                        ApplicationUser applicationUser = await _userManager.FindByNameAsync(request.Username);

                        if (applicationUser != null)
                        {
                            applicationUser.Name = request.Name;
                            applicationUser.PhoneNumber = request.PhoneNumber;

                            var status = await _userManager.UpdateAsync(applicationUser);
                            if (status == IdentityResult.Success)
                            {
                                _context.SaveChanges(null);
                                trans.Commit();

                                return BaseResponse.Factory.BuildSuccessResponse(_localizer[errorMessage]);
                            }
                            else
                            {
                                errorMessage = "change user profile failed";
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
                        _logger.LogException(nameof(ChangeUserProfileHandler), ex, request);
                        throw new CustomException(_localizer[LocalizationConstant.GENERIC_ERROR]);
                    }
                }
            }
        }
    }
}
