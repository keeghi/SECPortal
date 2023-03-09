using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecPortal.Commons.ViewModels.RoleViewModels;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Services.UserServices;
using SecPortal.Webapp.CQRS.Infrastructures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecPortal.Webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IMapper mapper, IUnitOfWork context, IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetRoles()
        {
            var result = _roleService.GetRoles();
            var roles = result.ToList();
            return BaseResponse.Factory.BuildSuccessResponse(roles.Count,
                _roleService.MapModelToViewModel<GetRoleResponse>(roles));
        }
    }
}
