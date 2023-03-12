using SecPortal.Entities.Infrastructures;
using SecPortal.Webapp.Authorizations;
using SecPortal.Webapp.CQRS.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecPortal.Webapp.Controllers
{
    [Route("api/oauth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly IAuthorization _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthorizationController(IUnitOfWork context, IHttpContextAccessor httpContextAccessor, IAuthorization authService)
        {
            _context = context;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IBaseResponse> LoginAsync([FromBody] LoginRequest request)
        {
            var response = await _authService.Login(request.Username, request.Password);
            return response;
        }

        [Authorize]
        [HttpGet("validate")]
        public ActionResult Validate()
        {
            //var guid = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //var targetUser = _context.Users.Single(x => x.Id == guid);
            //return Ok(targetUser);

            throw new NotImplementedException();
        }

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
