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
    public class PutUserCommand : IRequest<BaseResponse>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class PutUserHandler : BaseHandler<PutUserCommand, BaseResponse>,
            IRequestHandler<PutUserCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public PutUserHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer,
                UserManager<ApplicationUser> userManager, ILoggerManager logger, IMapper mapper)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<BaseResponse> Handle(PutUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _context.Users.Single(x => x.Id == request.Id);
                    var applicationUser = _mapper.Map(request, entity);
                    await _context.SaveChangesAsync(UserId);

                    return BaseResponse.Factory.BuildSuccessResponse(1, _mapper.Map<GetUserResponse>(applicationUser));
                }
                catch (Exception ex)
                {
                    _logger.LogException(nameof(PutUserHandler), ex, request);
                    throw new CustomException(_localizer[LocalizationConstant.GENERIC_ERROR]);
                }
            }
        }
    }
}
