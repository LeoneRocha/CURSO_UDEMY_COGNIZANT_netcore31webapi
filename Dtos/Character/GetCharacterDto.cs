using System.Collections.Generic;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Skill;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.User;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Weapon;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Models;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto Weapon { get; set; }

        public List<GetSkillDto> Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; } 
        
        public GetUserDto User { get; set; } 
    }
}