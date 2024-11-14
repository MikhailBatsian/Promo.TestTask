using Microsoft.AspNetCore.Mvc;
using Promo.TestTask.Api.Identity;
using Promo.TestTask.Api.Requests;
using Promo.TestTask.Domain.Account.DTO;
using Promo.TestTask.Domain.Account.Services;

namespace Promo.TestTask.Api.Controllers;
[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{

    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create-user")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userService.Get(createUserRequest.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError(nameof(createUserRequest.Email), "User with same email already exist");
            return BadRequest(ModelState);
        }

        var user = await _userService.Create(new UserDto
        {
            Email = createUserRequest.Email,
            PasswordHash = IdentityUtilities.ComputeHash(createUserRequest.Password),
            ProvinceId = createUserRequest.ProvinceId,
            IsAgreed = createUserRequest.IsAgreed
        });

        return Ok(user);
    }
}
