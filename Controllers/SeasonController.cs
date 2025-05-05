using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonRepository seasonRepository;

        public SeasonController(ISeasonRepository seasonRepository)
        {
            this.seasonRepository = seasonRepository;
        }

        [HttpPost]
        [Route("AddSeason")]
        public async Task<ActionResult<SeasonResponseModel>> CreatePost([FromBody] Season season)
        {
            if (season == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var seasonResponseModel = await seasonRepository.AddSeasonAsync(season);

            if (seasonResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = seasonResponseModel.Message });

            if (seasonResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = seasonResponseModel.Message });

            return Ok(new { MessageId = seasonResponseModel.MessageID, MessageDescription = seasonResponseModel.Message , User = seasonResponseModel.Season });
        }

        [HttpPost]
        [Route("GetAllSeasons")]
        public async Task<ActionResult<SeasonListResponseModel>> GetAllSeasons()
        {
            var seasonListResponseModel = await seasonRepository.GetAllSeasonsAsync();
            if (seasonListResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = seasonListResponseModel.Message });
            if (seasonListResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = seasonListResponseModel.Message });
            return Ok(new { MessageId = seasonListResponseModel.MessageID, MessageDescription = seasonListResponseModel.Message, Seasons = seasonListResponseModel.Seasons });
        }

        [HttpPost]
        [Route("SeasonDetail")]
        public async Task<ActionResult<SeasonResponseModel>> GetSeasonById([FromBody] SeasonParamModel seasonParamModel)
        {
            if (seasonParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var seasonResponseModel = await seasonRepository.GetSeasonByIdAsync(seasonParamModel.SeasonID);
            if (seasonResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = seasonResponseModel.Message });
            if (seasonResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = seasonResponseModel.Message });
            return Ok(new { MessageId = seasonResponseModel.MessageID, MessageDescription = seasonResponseModel.Message, User = seasonResponseModel.Season });
        }

        [HttpPost]
        [Route("UpdateSeason")]
        public async Task<ActionResult<SeasonResponseModel>> UpdatePost([FromBody] Season season)
        {
            if (season == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var seasonResponseModel = await seasonRepository.UpdateSeasonAsync(season);
            if (seasonResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = seasonResponseModel.Message });
            if (seasonResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = seasonResponseModel.Message });
            return Ok(new { MessageId = seasonResponseModel.MessageID, MessageDescription = seasonResponseModel.Message, User = seasonResponseModel.Season });
        }

        [HttpPost]
        [Route("DeleteSeason")]
        public async Task<ActionResult<SeasonResponseModel>> DeletePost([FromBody] SeasonParamModel seasonParamModel)
        {
            if (seasonParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var seasonResponseModel = await seasonRepository.DeleteSeasonAsync(seasonParamModel.SeasonID);
            if (seasonResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = seasonResponseModel.Message });
            if (seasonResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = seasonResponseModel.Message });
            return Ok(new { MessageId = seasonResponseModel.MessageID, MessageDescription = seasonResponseModel.Message, User = seasonResponseModel.Season });
        }
    }
}
