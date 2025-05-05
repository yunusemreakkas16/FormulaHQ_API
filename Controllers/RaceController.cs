using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IRaceRepository raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            this.raceRepository = raceRepository;
        }

        [HttpPost]
        [Route("AddRace")]
        public async Task<ActionResult<CarResponseModel>> AddRaceAsync([FromBody] Race race)
        {
            if (race == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var RaceResponseModel = await raceRepository.AddRaceAsync(race);

            if (RaceResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = RaceResponseModel.Message });

            if (RaceResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = RaceResponseModel.Message });

            return Ok(new { MessageId = RaceResponseModel.MessageID, MessageDescription = RaceResponseModel.Message, User = RaceResponseModel.Race });
        }

        [HttpPost]
        [Route("RaceList")]
        public async Task<ActionResult<CarListResponseModel>> RaceListAsync()
        {
            var RaceResponseModel = await raceRepository.GetAllRaceAsync();

            if (RaceResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = RaceResponseModel.Message });

            if (RaceResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = RaceResponseModel.Message });

            return Ok(new { MessageId = RaceResponseModel.MessageID, MessageDescription = RaceResponseModel.Message, User = RaceResponseModel.Races });
        }

        [HttpPost]
        [Route("RaceDetail")]
        public async Task<ActionResult<CarListResponseModel>> RaceDetailListAsync(RaceParamModel raceParamModel)
        {
            if (raceParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var RaceResponseModel = await raceRepository.GetRaceByIdAsync(raceParamModel.RaceID);

            if (RaceResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = RaceResponseModel.Message });

            if (RaceResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = RaceResponseModel.Message });

            return Ok(new { MessageId = RaceResponseModel.MessageID, MessageDescription = RaceResponseModel.Message, User = RaceResponseModel.Race });
        }

        [HttpPost]
        [Route("UpdateRace")]
        public async Task<ActionResult<CarResponseModel>> UpdateRaceAsync([FromBody] Race race)
        {
            if (race == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var RaceResponseModel = await raceRepository.UpdateRaceAsync(race);

            if (RaceResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = RaceResponseModel.Message });

            if (RaceResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = RaceResponseModel.Message });

            return Ok(new { MessageId = RaceResponseModel.MessageID, MessageDescription = RaceResponseModel.Message, User = RaceResponseModel.Race });
        }

    }
}
