using Application.Common;
using Application.Common.Exceptions;
using Application.Users.Command.CreateUser;
using Castle.Components.DictionaryAdapter.Xml;
using Core.Entities;
using Core.Users.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace Presentation.AcceptTests;

public class UserControllerTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UserControllerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task ShouldCreateUserValidCommand_ReturnsOk()
    {
        // arrange
        var mockMediator = new Mock<IMediator>();
        var controller = new UserController(mockMediator.Object);
        var createUserCommand = new CreateUserCommand
        {
            Username = "havira",
            Password = "havira"
        };

        mockMediator.Setup(m => m.Send(createUserCommand, default))
            .ReturnsAsync(new CustomResult<User>(200, "success", "User has been created.", null));

        // act
        var result = await controller.CreateUser(createUserCommand);

        // assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal("User has been created.", ((CustomResult<User>)okResult.Value!)?.Message);
    }
    
    [Fact]
    public async Task ShouldCreateUserCorrectCommandSentToMediator()
    {
        // arrange
        var mockMediator = new Mock<IMediator>();
        var controller = new UserController(mockMediator.Object);
        var createUserCommand = new CreateUserCommand();

        // act
        await controller.CreateUser(createUserCommand);

        // assert
        mockMediator.Verify(m => m.Send(createUserCommand, default), Times.Once);
    }

    [Fact]
    public async Task ShouldCreateUserDuplicateUsername_ReturnsBadRequest()
    {
        // arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CustomException());

        var controller = new UserController(mockMediator.Object);
        var command = new CreateUserCommand { Username = "existingUsername", Password = "password" };

        // act
        var result = await controller.CreateUser(command);

        // assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<ExceptionDetails>(badRequestResult.Value);
        Assert.Equal(400, response.Code);
        Assert.Equal("BAD REQUEST", response.Status);
        Assert.Equal("Duplicated user...", response.Message);
    }
    
}