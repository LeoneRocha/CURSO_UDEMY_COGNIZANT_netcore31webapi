using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Data;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Character;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        /* private static List<Character> characters = new List<Character>
         {
             new Character(),
             new Character(){ Id = 1 , Name = "Sam" },

         };*/
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            /* 
             var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
             Character addCharacter = _mapper.Map<Character>(newCharacter);
             addCharacter.Id = characters.Max(c => c.Id) + 1;
             serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
             return serviceResponse;
             */
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            /*
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
            */
 
            int iduser = GetUserId();

            var charactersQuery = _context.Characters
             .Include(c => c.Weapon)
             .Include(c => c.Skills)
             .Where(c => c.User.Id == iduser);


            charactersQuery = GetUserRole().Equals("Admin") ? _context.Characters
                                  .Include(c => c.Weapon)
                                  .Include(c => c.Skills)
                                  .Include(c => c.User) : charactersQuery;

            var charactersSel = GetUserRole().Equals("Admin")
                    ? await charactersQuery.ToListAsync()
                    : await charactersQuery.Where(c => c.User.Id == GetUserId()).ToListAsync();


            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = charactersSel.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            /*
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
             serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
             return serviceResponse
             ;
             */
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters
            .Include(c => c.Weapon)
            .Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

            if (dbCharacter != null)
            {
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
                serviceResponse.Success = true;
                serviceResponse.Message = "Character found.";
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found.";
            }


            return serviceResponse;

        }
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            /*var serviceResponse = new ServiceResponse<GetCharacterDto>(); 
            try
            {
                Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
            */
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters
                //.Include(c=>c.User)
                .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id
                && c.User.Id == GetUserId());
                if (character != null)
                {
                    character.Name = updatedCharacter.Name;
                    character.HitPoints = updatedCharacter.HitPoints;
                    character.Strength = updatedCharacter.Strength;
                    character.Defense = updatedCharacter.Defense;
                    character.Intelligence = updatedCharacter.Intelligence;
                    character.Class = updatedCharacter.Class;

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            /* var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
             try
             { 
                 Character character = characters.First(c => c.Id == id);
                 characters.Remove(character);
                 serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
             }
             catch (Exception ex)
             {
                 serviceResponse.Success = false;
                 serviceResponse.Message = ex.Message;
             }
             return serviceResponse;
             */
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User.Id == GetUserId());
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }

                character.Skills.Add(skill);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}