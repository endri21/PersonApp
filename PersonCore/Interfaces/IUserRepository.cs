using PersonCore.Dto;
using PersonEntity.Entity;
using System.Threading.Tasks;

namespace PersonCore.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Shton nje persorues te ri 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<UserRegisterResponse> AddUserAsync(UserRegisterRequest user);
        /// <summary>
        /// Kerkon nese perdoruesi ekziston me username e dhene
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> UserExist(string userName);
        /// <summary>
        /// Merr userin qe ekziston me username dhe password dhe nese ekziston kemi login te sukseshem
        /// perndryshe nuk behet logini pasi kombinimi username dhe password nuk perputhen 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        /// <param name="encryped"></param>
        /// <returns></returns>
        Task<SE_Users> GetUserByUserNameAndPassword(string userName, string Password, bool encryped);


    }
}
