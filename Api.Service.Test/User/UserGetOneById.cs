using Api.Domain.Interfaces.Services;
using Moq;
using Xunit.Sdk;

namespace Api.Service.Test.User;


public class UserGetOne : UserMock
{

    private IUserService _userService;
    private Mock<IUserService> _userServiceMock;


    [Fact(DisplayName = "Should be able get one user by id")]
    public async Task Should_Be_Able_Get_User_By_Id(){

        _userServiceMock = new Mock<IUserService>();
        _userServiceMock.Setup(x => x.GetOne(Id)).ReturnsAsync(UserDto);

        // depencie inject mock
        _userService = _userServiceMock.Object;

        var result = await _userService.GetOne(Id);

        Assert.NotNull(result);
        Assert.True(result.Id == Id);
        Assert.Equal(Name , result.Name);


    }
    
}