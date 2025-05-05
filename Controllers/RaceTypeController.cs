using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceTypeController : ControllerBase
    {
        private readonly IRaceTypeRepository raceTypeRepository;

        public RaceTypeController(IRaceTypeRepository raceTypeRepository)
        {
            this.raceTypeRepository = raceTypeRepository;
        }

        [HttpPost]
        [Route("AddRaceType")]
        public async Task<ActionResult<RaceTypeResponseModel>> CreateRaceTypeAsync([FromBody] RaceType raceType)
        {
            if (raceType == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var raceTypeResponseModel = await raceTypeRepository.CreateRaceTypeAsync(raceType);

            if (raceTypeResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = raceTypeResponseModel.MessageDescription });

            if (raceTypeResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = raceTypeResponseModel.MessageDescription });

            return Ok(new { MessageId = raceTypeResponseModel.MessageID, MessageDescription = raceTypeResponseModel.MessageDescription, User = raceTypeResponseModel.RaceType });
        }

        [HttpPost]
        [Route("RaceTypeList")]
        public async Task<ActionResult<RacetypeListResponseModel>> GetRaceTypeAllAsync()
        {
            var raceTypeResponseModel = await raceTypeRepository.GetAllRaceTypesAsync();

            if (raceTypeResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = raceTypeResponseModel.MessageDescription });

            if (raceTypeResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = raceTypeResponseModel.MessageDescription });

            return Ok(new { MessageId = raceTypeResponseModel.MessageID, MessageDescription = raceTypeResponseModel.MessageDescription, User = raceTypeResponseModel.RaceTypes});
        }

        [HttpPost]
        [Route("RaceTypeDetail")]
        public async Task<ActionResult<RaceTypeResponseModel>> GetRaceTypeByIDAsync(RaceTypeParamModel raceTypeParamModel)
        {
            var raceTypeResponseModel = await raceTypeRepository.GetRaceTypeByIdAsync(raceTypeParamModel.TypeID);

            if (raceTypeResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = raceTypeResponseModel.MessageDescription });

            if (raceTypeResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = raceTypeResponseModel.MessageDescription });

            return Ok(new { MessageId = raceTypeResponseModel.MessageID, MessageDescription = raceTypeResponseModel.MessageDescription, User = raceTypeResponseModel.RaceType });
        }

        [HttpPost]
        [Route("UpdateRaceType")]
        public async Task<ActionResult<RaceTypeResponseModel>> UpdateRaceTypeAsync([FromBody] RaceType raceType)
        {
            var raceTypeResponseModel = await raceTypeRepository.UpdateRaceTypeAsync(raceType);

            if (raceTypeResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = raceTypeResponseModel.MessageDescription });

            if (raceTypeResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = raceTypeResponseModel.MessageDescription });

            return Ok(new { MessageId = raceTypeResponseModel.MessageID, MessageDescription = raceTypeResponseModel.MessageDescription, User = raceTypeResponseModel.RaceType });
        }

    }
}
