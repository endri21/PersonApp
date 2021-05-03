using System;
namespace PersonCore.Dto
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string BirthPlace { get; set; }
        public string PhoneNumber { get; set; }
        public byte? Employed { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
    }

    public class PersonResponseDto
    {
        public bool Added { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class PersonUpdateDto
    {
        public bool Finded { get; set; }
        public bool Updated { get; set; }
        public string ErrorMessage { get; set; }

    }

}
