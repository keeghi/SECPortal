using AutoMapper;
using SecPortal.Commons.Constant;
using SecPortal.Commons.Enums;
using SecPortal.Commons.Exceptions;
using SecPortal.Commons.Helpers;
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
    public class CreateUserCommand : IRequest<GetUserResponse>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }

        public class CreateUserHandler : BaseHandler<CreateUserCommand, GetUserResponse>,
            IRequestHandler<CreateUserCommand, GetUserResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public CreateUserHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer,
                UserManager<ApplicationUser> userManager, ILoggerManager logger, IMapper mapper)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<GetUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var applicationUser = _mapper.Map<ApplicationUser>(request);
                        applicationUser.CreatedAt = DateHelper.Now;
                        applicationUser.ModifiedAt = DateHelper.Now;
                        applicationUser.EmailConfirmed = true;

                        var status = await _userManager.CreateAsync(applicationUser, request.Password);
                        if (status == IdentityResult.Success)
                        {
                            await _userManager.AddToRoleAsync(applicationUser, RolesEnum.Admin.ToString());
                            _context.SaveChanges(null);
                            trans.Commit();

                            return _mapper.Map<GetUserResponse>(applicationUser);
                        }
                        else
                        {
                            throw new CustomException(status.Errors.FirstOrDefault().Description);
                        }
                    }
                    catch (CustomException ex)
                    {
                        trans.Rollback();
                        throw new CustomException(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        _logger.LogException(nameof(CreateUserHandler), ex, request);
                        throw new CustomException(_localizer[LocalizationConstant.GENERIC_ERROR]);
                    }
                }
            }
        }
    }
}
