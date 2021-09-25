using AutoMapper;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Character;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Skill;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Weapon;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Models;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}