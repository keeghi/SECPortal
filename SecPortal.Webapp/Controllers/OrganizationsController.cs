using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecPortal.Commons.ViewModels.OrganizationViewModels;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Services.UserServices;
using SecPortal.Webapp.CQRS.Infrastructures;
using System.Linq;
using System.Threading.Tasks;

namespace SecPortal.Webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationService _organizationService;

        public OrganizationsController(IMapper mapper, IDataContext context)
        {
            _organizationService = new OrganizationService(mapper, context);
        }

        // GET: api/Organizations
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetOrganizations()
        {
            var result = _organizationService.GetOrganizations();
            var organizations = result.ToList();
            return BaseResponse.Factory.BuildSuccessResponse(organizations.Count, _organizationService.MapModelToViewModel<GetOrganizationResponse>(organizations));
        }
    }
}
