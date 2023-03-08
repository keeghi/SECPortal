using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecPortal.Commons.ViewModels.UserViewModels;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IUnitOfWork context, IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetUsers()
        {
            var result = _userService.GetUsers();
            var users = result.ToList();
            return BaseResponse.Factory.BuildSuccessResponse(users.Count,
                _userService.MapModelToViewModel<GetUserResponse>(users));
        }

        #region reference
        //[HttpPost("admin")]
        //[Authorize(Roles = "Admin,SuperAdmin")]
        //public async Task<ActionResult> CreateAdmin([FromBody] CreateUserCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }

        //            return CreatedAtAction("GetUsers", new { id = result.Id }, result);
        //        }

        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));
        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //}

        //[HttpPut("admin/{id}")]
        //[Authorize(Roles = "Admin,SuperAdmin")]
        //public async Task<ActionResult> PutAdmin([FromRoute] Guid id, [FromBody] PutUserCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid && id == command.Id)
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }

        //            return Ok(result);
        //        }

        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));
        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //}

        //[HttpPut("admin/customer/{id}")]
        //[Authorize(Roles = "Admin,SuperAdmin")]
        //public async Task<ActionResult> PutCustomerForAdmin([FromRoute] Guid id, [FromBody] PutCustomerCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid && id == command.Id)
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }

        //            return Ok(result);
        //        }

        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));
        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //}

        //[HttpPost("admin/change-password")]
        //[Authorize(Roles = "Admin,SuperAdmin")]
        //public async Task<ActionResult> ChangeAdminPassword([FromBody] ChangeAdminPasswordCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }

        //            return Ok(result);
        //        }

        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));
        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //}

        //[HttpPost("ChangePassword")]
        //[Authorize(Roles = "Customer")]
        //public async Task<ActionResult<BaseResponse>> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }
        //            return result;
        //        }
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));

        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //}

        //[HttpPut("customer/{id}")]
        //[Authorize(Roles = "Customer")]
        //public async Task<ActionResult> PutCustomer([FromRoute] Guid id, [FromBody] PutCustomerProfileCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid && id == Guid.Parse(((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value))
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }

        //            return Ok(result);
        //        }

        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));
        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.GENERIC_ERROR]));
        //    }
        //}

        //[HttpPut("customer/{id}/password")]
        //[Authorize(Roles = "Customer")]
        //public async Task<ActionResult> PutCustomerPassword([FromRoute] Guid id, [FromBody] PutCustomerOwnPasswordCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid && id == Guid.Parse(((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value))
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }

        //            return Ok(result);
        //        }

        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));
        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //}

        //[HttpPost("ChangeProfile")]
        //public async Task<ActionResult<BaseResponse>> ChangeUserProfile([FromBody] ChangeUserProfileCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }
        //            return result;
        //        }
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));

        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //}

        //[HttpDelete("{Id}")]
        //public async Task<ActionResult> DeleteUser([FromRoute] DeleteUserCommand command)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var result = await _mediator.Send(command);
        //            if (result == null)
        //            {
        //                return BadRequest();

        //            }

        //            return Ok(result);
        //        }

        //        return Ok(BaseResponse.Factory.BuildFailedResponse(_localizer[LocalizationConstant.BAD_REQUEST]));
        //    }
        //    catch (CustomException ex)
        //    {
        //        return Ok(BaseResponse.Factory.BuildFailedResponse(ex.Message));
        //    }
        //}

        //[HttpPost("forgot-password")]
        //[AllowAnonymous]
        //public async Task<BaseResponse> ForgotPassword([FromBody] ForgotPasswordCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return result;
        //}

        //[HttpPost("reset-password")]
        //[AllowAnonymous]
        //public async Task<BaseResponse> ResetPassword([FromBody] ResetPasswordCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return result;
        //}

        //[HttpPost("activate-account")]
        //[AllowAnonymous]
        //public async Task<IBaseResponse> ActivateAccount([FromBody] ActivateAccountCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return result;
        //}
        #endregion
    }
}

