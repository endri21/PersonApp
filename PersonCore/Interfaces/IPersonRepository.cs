using PersonCore.Dto;
using PersonEntity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonCore.Interfaces
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Shton nje rekord te ri ne db te tipit person
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        Task<PersonResponseDto> AddPersonAsync(PersonDto personDto);
        /// <summary>
        /// Mer te gjithe personat qe jane Invalidated = 20
        /// </summary>
        /// <returns></returns>
        Task<List<PersonDto>> GetAllPersonsAsync();
        /// <summary>
        /// Kerkon nje person me emrin dhe mbiemri (search mund te jete ose te emri ose te mbiemri)
        /// nuk eshte case sensitive
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<List<PersonDto>> GetPersonByTextAsync( string search);
        /// <summary>
        /// Ben modifikimin e te dhenave te nje personi 
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        Task<PersonUpdateDto> UpdatePersonAsync(PersonDto personDto);
        /// <summary>
        /// Mer te dhenat e nje personi me id e dhene si parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SE_Persons> GetPersonById(int id);
        /// <summary>
        /// Ben Invalidated = 10 te nje personi 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletePersonAsync(int id);
    }
}
