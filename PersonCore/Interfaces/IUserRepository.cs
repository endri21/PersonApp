using PersonCore.Dto;
using PersonEntity.Entity;
using System.Threading.Tasks;

namespace PersonCore.Interfaces
{
   public interface IUserRepository
    {
        Task<UserRegisterResponse> AddUserAsync(UserRegisterRequest user);

        Task<bool> UserExist(string userName);
        Task<SE_Users> GetUserByUserNameAndPassword(string userName, string Password, bool encryped);
        

    }
}
