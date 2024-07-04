using Api.Aplication.Controllers;
using Api.Domain.Dto;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.User;

public class UserCreateTest
{
    [Fact(DisplayName = "Should_Be_Able_Create_A_User")]
    public async void Should_Be_Able_Create_A_User()
    {

        var userMock = new UserMock();
        var  serviceMock = new Mock<IUserService>();

        serviceMock.Setup(x => x.Create(UserMock.UserDto)).ReturnsAsync(UserMock.UserDto);

        var _controller = new UsersController(serviceMock.Object);

        Mock<IUrlHelper> url = new Mock<IUrlHelper>();
        url.Setup(x => x.Link(It.IsAny<string>(),It.IsAny<object>())).Returns("http://test.com");

        _controller.Url = url.Object;

        var result = await _controller.Create(UserMock.UserDto);
        var value = ((OkObjectResult)result).Value as UserDto;

        Assert.True(result is OkObjectResult);
        Assert.NotNull(result);
        Assert.True(UserMock.UserDto.Id == value.Id);   
        Assert.Equal(UserMock.UserDto.Name , value.Name);
    
    }
}