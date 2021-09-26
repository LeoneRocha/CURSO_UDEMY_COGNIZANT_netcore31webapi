using System.Threading.Tasks;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Dtos.Fight;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Models;
using CURSO_UDEMY_COGNIZANT_netcore31webapi.Services.FightService;
using Microsoft.AspNetCore.Mvc;

namespace CURSO_UDEMY_COGNIZANT_netcore31webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(WeaponAttackDto request)
        {
            return Ok(await _fightService.WeaponAttack(request));
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> SkillAttack(SkillAttackDto request)
        {
            return Ok(await _fightService.SkillAttack(request));
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultDto>>> Fight(FightRequestDto request)
        {
            return Ok(await _fightService.Fight(request));
        }

        [HttpGet("GetHighscore")]
        public async Task<ActionResult<ServiceResponse<HighscoreDto>>> GetHighscore()
        {
            return Ok(await _fightService.GetHighscore());
        }
    }
}