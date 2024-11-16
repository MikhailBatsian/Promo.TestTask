using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Promo.TestTask.Api.Controllers;
using Promo.TestTask.Api.Requests;
using Promo.TestTask.Api.Validators;
using Promo.TestTask.Domain.Account.DTO;
using Promo.TestTask.Domain.Account.Services;
using Xunit;

namespace Promo.TestTask.Tests;

public class UserControllerTests
{
    [Fact]
    public async Task CreateUser_Returns_Error_When_Data_Is_Incorrect()
    {
        //Arrange
        var userService = new Mock<IUserService>().Object;
        var createUserValidator = new CreateUserValidator(userService);
        var accountController = new AccountController(userService, createUserValidator);

        //Act
        var sut = await accountController.CreateUser(new CreateUserRequest
        {
            Email = "string",
            Password = "password",
            IsAgreed = false
        });

        //Assert
       var badRequestResult = sut.Should().BeOfType<BadRequestObjectResult>();
       var errors = (List<string>)badRequestResult.Subject.Value!;
       errors.Should().Contain(CreateUserValidator.INVALID_EMAIL_MESSAGE);
       errors.Should().Contain(CreateUserValidator.MUST_AGREE_MESSAGE);
       errors.Should().Contain(CreateUserValidator.INVALID_PASSWORD_MESSAGE);
    }

    [Fact]
    public async Task CreateUser_Returns_Error_When_User_Already_Exists()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(new UserDto());
        var userService = userServiceMock.Object;
        
        var createUserValidator = new CreateUserValidator(userService);
        var accountController = new AccountController(userService, createUserValidator);

        //Act
        var sut = await accountController.CreateUser(new CreateUserRequest
        {
            Email = "test@test.cv",
            Password = "password1",
            IsAgreed = true
        });

        //Assert
        var badRequestResult = sut.Should().BeOfType<BadRequestObjectResult>();
        var errors = (List<string>)badRequestResult.Subject.Value!;
        errors.Should().Contain(CreateUserValidator.EMAIL_EXISTS_MESSAGE);
    }
}
