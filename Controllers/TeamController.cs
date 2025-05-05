using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        [HttpPost]
        [Route("AddTeam")]
        public async Task<ActionResult<TeamResponseModel>> CreatePost([FromBody] Team team)
        {
            if (team == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var teamResponseModel = await teamRepository.AddTeamAsync(team);

            if (teamResponseModel.MessageId == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = teamResponseModel.Message });

            if (teamResponseModel.MessageId == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = teamResponseModel.Message });

            return Ok(new { MessageId = teamResponseModel.MessageId, MessageDescription = teamResponseModel.Message, User = teamResponseModel.Team});
        }

        [HttpPost]
        [Route("GetAllTeams")]
        public async Task<ActionResult<TeamListResponseModel>> GetAllTeams()
        {
            var teamListResponseModel = await teamRepository.GetAllTeamsAsync();
            if (teamListResponseModel.MessageId == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = teamListResponseModel.Message });
            if (teamListResponseModel.MessageId == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = teamListResponseModel.Message });
            return Ok(new { MessageId = teamListResponseModel.MessageId, MessageDescription = teamListResponseModel.Message, Teams = teamListResponseModel.Teams });
        }

        [HttpPost]
        [Route("TeamDetails")]
        public async Task<ActionResult<TeamResponseModel>> GetTeamById([FromBody] TeamDetailParamModel teamDetailParamModel)
        {
            if (teamDetailParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var teamResponseModel = await teamRepository.GetTeamByIdAsync(teamDetailParamModel.TeamID);
            if (teamResponseModel.MessageId == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = teamResponseModel.Message });
            if (teamResponseModel.MessageId == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = teamResponseModel.Message });
            return Ok(new { MessageId = teamResponseModel.MessageId, MessageDescription = teamResponseModel.Message, User = teamResponseModel.Team });
        }

        [HttpPost]
        [Route("UpdateTeam")]
        public async Task<ActionResult<TeamResponseModel>> UpdateTeam([FromBody] Team team)
        {
            if (team == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var teamResponseModel = await teamRepository.UpdateTeamAsync(team);
            if (teamResponseModel.MessageId == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = teamResponseModel.Message });
            if (teamResponseModel.MessageId == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = teamResponseModel.Message });
            return Ok(new { MessageId = teamResponseModel.MessageId, MessageDescription = teamResponseModel.Message, User = teamResponseModel.Team });
        }
    }
}
