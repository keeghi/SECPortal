using AutoMapper;
using SecPortal.Commons.Constant;
using SecPortal.Commons.Enums;
using SecPortal.Commons.ViewModels.UserViewModels;
using SecPortal.Entities.Infrastructures;
using SecPortal.Webapp.CQRS.Infrastructures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace SecPortal.Webapp.CQRS.Queries.Users
{
    public class GetAllUserQuery : BaseQueries, IRequest<BaseResponse>
    {
        public RolesEnum? Roles { get; set; }
        public ActivityEnum IsActive { get; set; }

        public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, BaseResponse>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;
            public GetAllUserHandler(IMapper mapper, IDataContext context)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BaseResponse> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {
                var users = _context.Users.ToList();
                var query = _context.Users.AsQueryable();

                if (request.Roles != null)
                {
                    //TODO: handle this 
                    switch (request.Roles)
                    {
                        case RolesEnum.Admin:
                            query = query.Where(x => x.UserRoles.Any(x => x.RoleId == Guid.Parse(RolesConstant.AdminId)));
                            break;
                        case RolesEnum.SuperAdmin:
                            query = query.Where(x => x.UserRoles.Any(x => x.RoleId == Guid.Parse(RolesConstant.SuperAdminId)));
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }

                if (request.IsActive != ActivityEnum.All)
                {
                    switch (request.IsActive)
                    {
                        case ActivityEnum.Active:
                            query = query.Where(x => x.IsActive);
                            break;
                        case ActivityEnum.Inactive:
                            query = query.Where(x => !x.IsActive);
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(request.SortBy))
                {
                    query = query.OrderBy(request.SortBy);
                }

                int pageNo = request.PageNo < 1 ? 1 : request.PageNo;
                var result = query.Skip((pageNo - 1) * request.RecordsPerPage).Take(request.RecordsPerPage).ToList();

                var holder = new List<GetUserResponse>();
                foreach (var item in result)
                {
                    item.CheckRelation(_context);
                    holder.Add(_mapper.Map<GetUserResponse>(item));
                }

                return BaseResponse.Factory.BuildSuccessResponse(query.ToList().Count(), holder);
            }
        }
    }
}
