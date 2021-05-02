using PersonCore.Dto;
using PersonEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonCore.Interfaces
{
    public interface IPersonRepository
    {
        Task<PersonResponseDto> AddPersonAsync(PersonDto personDto);
        Task<List<PersonDto>> GetAllPersonsAsync();
        Task<List<PersonDto>> GetPersonByTextAsync( string search);
        Task<PersonUpdateDto> UpdatePersonAsync(PersonDto personDto);
        Task<SE_Persons> GetPersonById(int id);
        Task<bool> DeletePersonAsync(int id);
    }
}
