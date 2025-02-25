
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> GetByEmail(string email);
    }
}