using PersonCore.Interfaces;
using PersonCore.Dto;
using PersonEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PersonCore.Helpers.Security;

namespace PersonCore.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly PersonDbEntities dbEntities;

        public UserRepository(PersonDbEntities entities)
        {
            this.dbEntities = entities;
        }
        public async Task<UserRegisterResponse> AddUserAsync(UserRegisterRequest user)
        {

            var userExist = await UserExist(user.Username);
            if (userExist)
            {
                return new UserRegisterResponse()
                {

                    UserExist = true,
                    ErrorMessage = "Ky perdorues eshte i regjistruar me pare!"

                };
            }

            using (var transaction = dbEntities.Database.BeginTransaction())
            {
                try
                {
                    var addPersonalInfo = new SE_UserPersonalInfo
                    {
                        Name = user.Name,
                        Address = user.Address,
                        Email = user.Email,
                        Registration_date = DateTime.Now,


                    };
                    dbEntities.SE_UserPersonalInfo.Add(addPersonalInfo);
                    try
                    {
                        await dbEntities.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        return new UserRegisterResponse()
                        {
                            AddedPersonalInfo = false,
                            AddedUser = false,
                            ErrorMessage = "Nuk u ruajten te dhenat e perdoruesit "
                        };
                    }

                    var addUser = new SE_Users()
                    {
                        IDSE_UserPersonalInfo = addPersonalInfo.IDUserPersonalInfo,
                        Username = user.Username,
                        Password = Crypto.Hash(user.Password),
                        Invalidated = 20
                    };

                    dbEntities.SE_Users.Add(addUser);
                    try
                    {
                        await dbEntities.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return new UserRegisterResponse()
                        {
                            AddedPersonalInfo = true,
                            AddedUser = false,
                            UserExist = false,
                            ErrorMessage = "Deshtoi ruajtja e perdoruesit"
                        };
                    }
                    transaction.Commit();
                    return new UserRegisterResponse
                    {
                        AddedUser = true,
                        AddedPersonalInfo = true,
                        UserExist = false,
                        ErrorMessage = "Perdoruesi u shtua me sukses"
                    };
                }
                catch
                {
                    transaction.Rollback();
                    return new UserRegisterResponse()
                    {
                        AddedPersonalInfo = false,
                        AddedUser = false,
                        UserExist = false,
                        ErrorMessage = "Ka ndodhur nje gabim! Ju lutem provoni me vone"
                    };
                }
            }

        }

        public async Task<SE_Users> GetUserByUserNameAndPassword(string userName, string Password, bool encryped = false)
        {

            string EncrypedPass = "";
            if (!encryped)
            {
                //do fshihet 
                EncrypedPass = Crypto.Hash(Password);
            }
                return await (dbEntities.SE_Users.FirstOrDefaultAsync(a => a.Invalidated == 20 && a.Username.ToLower() == userName && a.Password == EncrypedPass));

        }

        public async Task<bool> UserExist(string userName)
        {
            try
            {
                return (await dbEntities.SE_Users.AnyAsync(a => a.Username.ToLower() == userName.ToLower()));

            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
