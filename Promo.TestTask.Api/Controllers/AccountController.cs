using Microsoft.AspNetCore.Mvc;
using Promo.TestTask.Api.Identity;
using Promo.TestTask.Api.Requests;
using Promo.TestTask.Api.Validators;
using Promo.TestTask.Domain.Account.DTO;
using Promo.TestTask.Domain.Account.Services;

namespace Promo.TestTask.Api.Controllers;
[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly CreateUserValidator _createUserValidator;

    public AccountController(
        IUserService userService, 
        CreateUserValidator createUserValidator)
    {
        _userService = userService;
        _createUserValidator = createUserValidator;
    }

    [HttpPost("create-user")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
    {
        var validationResult = await _createUserValidator.ValidateAsync(createUserRequest);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());

        var user = await _userService.Create(new UserDto
        {
            Email = createUserRequest.Email,
            PasswordHash = IdentityUtilities.ComputeHash(createUserRequest.Password),
            Country = createUserRequest.Country,
            Province = createUserRequest.Province,
            IsAgreed = createUserRequest.IsAgreed
        });

        return Ok(user);
    }
}
