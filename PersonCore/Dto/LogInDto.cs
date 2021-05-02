using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonCore.Models
{
    public class LogInRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public bool UserExist { get; set; } = false;
        public bool PasswordCorrect { get; set; } = false;
        public string ErrorMessage { get; set; }

    }
}
