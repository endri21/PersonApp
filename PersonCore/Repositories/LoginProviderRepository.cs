using PersonCore.Interfaces;
using PersonCore.Models;
using PersonEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonCore.Repositories
{
    public class LoginProviderRepository : ILoginProviderRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly PersonDbEntities dbEntities;

        public LoginProviderRepository(IUserRepository userRepository, PersonDbEntities entities)
        {
            this.dbEntities = entities;
            this._userRepository = userRepository;
        }

        public async Task<LoginResponseModel> LogInUserAsync(LogInRequestModel logIn)
        {
            


            var getUser = await _userRepository.GetUserByUserNameAndPassword(logIn.UserName, logIn.Password, false);
            
            if(getUser == null)
            {
                var user = await _userRepository.UserExist(logIn.UserName);

                if (user)
                {
                    return new LoginResponseModel()
                    {
                        UserExist = true,
                        PasswordCorrect = false,
                        ErrorMessage = "Fjalekalimi eshte i pasakte"

                    };
                }
                else
                {
                    return new LoginResponseModel()
                    {
                        UserExist = false,
                        PasswordCorrect = false,
                        ErrorMessage = "Perdoruesi nuk ekziston"

                    };
                }
            }
            return new LoginResponseModel
            {
                PasswordCorrect = true,
                UserExist = true,

            };


        }
    }
}
