

using Api.Domain.Dtos;
using System.Security.Claims;



namespace Api.Domain.Interfaces.Services
{
    public interface IAuthorizationService
    {
        Task<object>Login(LoginDto loginDto);

        
    }
}