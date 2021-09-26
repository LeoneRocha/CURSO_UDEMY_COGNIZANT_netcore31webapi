using System.Collections.Generic;
using System.Threading.Tasks;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Fight;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Models;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
        Task<ServiceResponse<List<HighscoreDto>>> GetHighscore();

    }
}