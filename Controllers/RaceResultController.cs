using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceResultController : ControllerBase
    {
        private readonly IRaceResultRepository raceResultRepository;

        public RaceResultController(IRaceResultRepository raceResultRepository)
        {
            this.raceResultRepository = raceResultRepository;
        }

        [HttpPost]
        [Route("AddRaceResult")]
        public async Task<ActionResult<RaceResultResponseModel>> CreateRaceResultAsync([FromBody] RaceResult raceResult)
        {
            if (raceResult == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var raceResultResponseModel = await raceResultRepository.CreateRaceResultAsync(raceResult);

            if (raceResultResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = raceResultResponseModel.Message });

            if (raceResultResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = raceResultResponseModel.Message });

            return Ok(new { MessageId = raceResultResponseModel.MessageID, MessageDescription = raceResultResponseModel.Message, Driver = raceResultResponseModel.RaceResult });
        }

        [HttpPost]
        [Route("RaceResultList")]
        public async Task<ActionResult<RaceResultListResponseModel>> GetAllRaceResultsAsync()
        {
            var raceResultListResponseModel = await raceResultRepository.GetAllRaceResultsAsync();
            if (raceResultListResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = raceResultListResponseModel.Message });
            if (raceResultListResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = raceResultListResponseModel.Message });
            return Ok(new { MessageId = raceResultListResponseModel.MessageID, MessageDescription = raceResultListResponseModel.Message, RaceResults = raceResultListResponseModel.RaceResults });
        }
        [HttpPost]
        [Route("GetRaceResultbyID")]
        public async Task<ActionResult<RaceResultResponseModel>> GetRaceResultbyIDAsync([FromBody] RaceResult raceResult)
        {
            if (raceResult == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var raceResultResponseModel = await raceResultRepository.GetRaceResultbyIDAsync(raceResult.ResultID);
            if (raceResultResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = raceResultResponseModel.Message });
            if (raceResultResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = raceResultResponseModel.Message });
            return Ok(new { MessageId = raceResultResponseModel.MessageID, MessageDescription = raceResultResponseModel.Message, RaceResult = raceResultResponseModel.RaceResult });
        }

        [HttpPost]
        [Route("UpdateRaceResult")]
        public async Task<ActionResult<RaceResultResponseModel>> UpdateRaceResultAsync([FromBody] RaceResult raceResult)
        {
            if (raceResult == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var raceResultResponseModel = await raceResultRepository.UpdateRaceResultAsync(raceResult);
            if (raceResultResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = raceResultResponseModel.Message });
            if (raceResultResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = raceResultResponseModel.Message });
            return Ok(new { MessageId = raceResultResponseModel.MessageID, MessageDescription = raceResultResponseModel.Message, Driver = raceResultResponseModel.RaceResult });
        }
    }
}
