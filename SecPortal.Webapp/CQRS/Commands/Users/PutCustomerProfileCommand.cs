using AutoMapper;
using SecPortal.Commons.Constant;
using SecPortal.Commons.Exceptions;
using SecPortal.Commons.ViewModels.UserViewModels;
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
    public class PutCustomerProfileCommand : IRequest<BaseResponse>
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }

        public class PutCustomerProfileHandler : BaseHandler<PutCustomerProfileCommand, BaseResponse>,
            IRequestHandler<PutCustomerProfileCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public PutCustomerProfileHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer,
                UserManager<ApplicationUser> userManager, ILoggerManager logger, IMapper mapper)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<BaseResponse> Handle(PutCustomerProfileCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _context.Users.Single(x => x.Id == User.Id);
                    var applicationUser = _mapper.Map(request, entity);
                    await _context.SaveChangesAsync(UserId);

                    return BaseResponse.Factory.BuildSuccessResponse(1, _mapper.Map<GetUserResponse>(applicationUser));
                }
                catch (Exception ex)
                {
                    _logger.LogException(nameof(PutCustomerProfileHandler), ex, request);
                    throw new CustomException(_localizer[LocalizationConstant.GENERIC_ERROR]);
                }
            }
        }
    }


    public class PutCustomerOwnPasswordCommand : IRequest<BaseResponse>
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

        public class PutCustomerOwnPasswordHandler : BaseHandler<PutCustomerOwnPasswordCommand, BaseResponse>,
            IRequestHandler<PutCustomerOwnPasswordCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public PutCustomerOwnPasswordHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer,
                UserManager<ApplicationUser> userManager, ILoggerManager logger, IMapper mapper)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<BaseResponse> Handle(PutCustomerOwnPasswordCommand request, CancellationToken cancellationToken)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        string errorMessage = string.Empty;
                        ApplicationUser applicationUser = await _userManager.FindByIdAsync(UserId.ToString());

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
                                    errorMessage = "Change password failed";
                                }
                            }
                            else
                            {
                                errorMessage = "Incorrect old password";
                            }
                        }
                        else
                        {
                            errorMessage = "User not found";
                        }

                        return BaseResponse.Factory.BuildFailedResponse(_localizer[errorMessage]);

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        _logger.LogException(nameof(PutCustomerOwnPasswordCommand), ex, request);
                        throw new CustomException(_localizer[LocalizationConstant.GENERIC_ERROR]);
                    }
                }
            }
        }
    }
}
