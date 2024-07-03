
using Api.Domain.Dtos;



namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
    Task<UserDto> GetOne(Guid id);
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDto> Update(UserDto user);
    Task<UserDto> Create(UserDto user);
    Task<bool> Delete(Guid id);
        
    }
}