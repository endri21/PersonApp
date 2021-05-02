using PersonEntity.Entity;


namespace PersonCore.Dto
{
    public class UserRegisterRequest
    {
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
    }

    public class UserRegisterResponse
    {
        public bool AddedPersonalInfo { get; set; } = false;
        public bool AddedUser { get; set; } = false;
        public bool UserExist { get; set; } = false;
        public string ErrorMessage { get; set; } = "";
    }
}
