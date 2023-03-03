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
    public class PutCustomerCommand : IRequest<BaseResponse>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }
        public string Notes { get; set; }

        public class PutCustomerHandler : BaseHandler<PutCustomerCommand, BaseResponse>,
            IRequestHandler<PutCustomerCommand, BaseResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;

            public PutCustomerHandler(IDataContext context, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Resource> localizer,
                UserManager<ApplicationUser> userManager, ILoggerManager logger, IMapper mapper)
                : base(context, httpContextAccessor, localizer, logger)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<BaseResponse> Handle(PutCustomerCommand request, CancellationToken cancellationToken)
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
                    _logger.LogException(nameof(PutCustomerHandler), ex, request);
                    throw new CustomException(_localizer[LocalizationConstant.GENERIC_ERROR]);
                }
            }
        }
    }
}
