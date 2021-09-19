using System.Collections.Generic;
using System.Threading.Tasks;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Character;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Models;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int idUser);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}