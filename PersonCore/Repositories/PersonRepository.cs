using PersonCore.Dto;
using PersonCore.Interfaces;
using PersonEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonCore.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbEntities dbEntities;
        public PersonRepository(PersonDbEntities dbEntities)
        {
            this.dbEntities = dbEntities;
        }

        public async Task<PersonResponseDto> AddPersonAsync(PersonDto personDto)
        {
            using (var transaction = dbEntities.Database.BeginTransaction())
            {
                try
                {

                    var person = new SE_Persons
                    {
                        FirstName = personDto.Name,
                        LastName = personDto.LastName,
                        BirthDay = personDto.Birthday,
                        BirthPlace = personDto.BirthPlace,
                        CivilStatus = personDto.CivilStatus,
                        PhoneNumber = personDto.PhoneNumber,
                        Employed = personDto.Employed,
                        Gender = personDto.Gender,
                        CreatedAt = DateTime.Now

                    };
                    dbEntities.SE_Persons.Add(person);
                    await dbEntities.SaveChangesAsync();
                    transaction.Commit();
                    return new PersonResponseDto
                    {
                        Added = true,
                        ErrorMessage = "Personni u shtua me sukses"
                    };

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new PersonResponseDto
                    {
                        Added = false,
                        ErrorMessage = "Personi nuk u shtua."
                    };
                }
            }
        }


        public async Task<List<PersonDto>> GetAllPersonsAsync()
        {
            return await dbEntities.SE_Persons
                .Select(s => new PersonDto
                {
                    Id = (int)s.IDSE_Person,
                    Name = s.FirstName,
                    LastName = s.LastName,
                    CivilStatus = s.CivilStatus,
                    BirthPlace = s.BirthPlace,
                    Employed = s.Employed
                })
                .ToListAsync();

        }

        public async Task<SE_Persons> GetPersonById(int id)
        {
            return await dbEntities.SE_Persons.FirstOrDefaultAsync(a => a.IDSE_Person == id);
        }

        public async Task<List<PersonDto>> GetPersonByTextAsync(string search)
        {
            return await dbEntities.SE_Persons
                .Where(a => (a.FirstName.ToLower().Contains(search.ToLower())
                )
                || (a.LastName.ToLower().Contains(search.ToLower())

                ))
               .Select(s => new PersonDto
               {
                   Name = s.FirstName,
                   LastName = s.LastName,
                   Employed = s.Employed
               })
               .ToListAsync();
        }

        public async Task<PersonUpdateDto> UpdatePersonAsync(PersonDto Dto)
        {
            using (var transaction = dbEntities.Database.BeginTransaction())
            {
                try
                {
                    var person = await GetPersonById(Dto.Id);
                    if (person == null)
                    {
                        return new PersonUpdateDto
                        {
                            Finded = false,
                            Updated = false,
                            ErrorMessage = "Te dhenat e ketij personi nuk gjenden"
                        };
                    }
                    person.FirstName = Dto.Name;
                    person.LastName = Dto.LastName;
                    person.BirthDay = Dto.Birthday;
                    person.BirthPlace = Dto.BirthPlace;
                    person.Gender = Dto.Gender;
                    person.Employed = Dto.Employed;
                    person.PhoneNumber = Dto.PhoneNumber;
                    person.CivilStatus = Dto.CivilStatus;
                    await dbEntities.SaveChangesAsync();
                    transaction.Commit();
                    return new PersonUpdateDto
                    {
                        Updated = true,
                        Finded = true,
                        ErrorMessage = "Te dhenat e personit u modifikuan me sukses"
                    };



                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new PersonUpdateDto
                    {
                        Updated = false,
                        ErrorMessage = "Personi nuk u modifikua."
                    };
                }
            }
        }
    }
}
