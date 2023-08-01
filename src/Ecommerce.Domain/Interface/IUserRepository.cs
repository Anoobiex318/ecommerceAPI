using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface
{

    public interface IUserRepository
    {

        Task<UserEntity> AddUser(UserEntity userName);

        Task<UserEntity> GetUserById(Guid userId);

        Task<UserEntity> GetUserByUsername(string username);

    }
}
