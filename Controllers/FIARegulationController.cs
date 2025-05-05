using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FIARegulationController : ControllerBase
    {
        private readonly IFIARegulationRepository fIARegulationRepository;

        public FIARegulationController(IFIARegulationRepository FIARegulationRepository)
        {
            fIARegulationRepository = FIARegulationRepository;
        }

        [HttpPost]
        [Route("AddFIARegulation")]
        public async Task<ActionResult<FIARegulationResponseModel>> CreateFIARegulationAsync([FromBody] FIARegulation fiaRegulation)
        {
            if (fiaRegulation == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var fiaRegulationResponseModel = await fIARegulationRepository.AddFIARegulationAsync(fiaRegulation);
            if (fiaRegulationResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = fiaRegulationResponseModel.Message });
            if (fiaRegulationResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = fiaRegulationResponseModel.Message });
            return Ok(new { MessageId = fiaRegulationResponseModel.MessageID, MessageDescription = fiaRegulationResponseModel.Message, FIARegulation = fiaRegulationResponseModel.Regulation });
        }
        [HttpPost]
        [Route("ListFIARegulation")]
        public async Task<ActionResult<FIARegulationListResponseModel>> GetAllFIARegulationsAsync()
        {
            var fiaRegulationResponseModel = await fIARegulationRepository.GetAllFIARegulationsAsync();
            if (fiaRegulationResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = fiaRegulationResponseModel.Message });
            if (fiaRegulationResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = fiaRegulationResponseModel.Message });
            return Ok(new { MessageId = fiaRegulationResponseModel.MessageID, MessageDescription = fiaRegulationResponseModel.Message, FIARegulations = fiaRegulationResponseModel.Regulations });
        }
        [HttpPost]
        [Route("GetFIARegulationByID")]
        public async Task<ActionResult<FIARegulationResponseModel>> GetFIARegulationByIDAsync([FromBody] FIARegulationParamModel regulationParam)
        {
            if (regulationParam == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var fiaRegulationResponseModel = await fIARegulationRepository.GetFIARegulationByIDAsync(regulationParam);
            if (fiaRegulationResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = fiaRegulationResponseModel.Message });
            if (fiaRegulationResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = fiaRegulationResponseModel.Message });
            return Ok(new { MessageId = fiaRegulationResponseModel.MessageID, MessageDescription = fiaRegulationResponseModel.Message, FIARegulation = fiaRegulationResponseModel.Regulation });
        }
        [HttpPost]
        [Route("UpdateFIARegulation")]
        public async Task<ActionResult<FIARegulationResponseModel>> UpdateFIARegulationAsync([FromBody] FIARegulation fiaRegulation)
        {
            if (fiaRegulation == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var fiaRegulationResponseModel = await fIARegulationRepository.UpdateFIARegulationAsync(fiaRegulation);
            if (fiaRegulationResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = fiaRegulationResponseModel.Message });
            if (fiaRegulationResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = fiaRegulationResponseModel.Message });
            return Ok(new { MessageId = fiaRegulationResponseModel.MessageID, MessageDescription = fiaRegulationResponseModel.Message, FIARegulation = fiaRegulationResponseModel.Regulation });
        }
        [HttpPost]
        [Route("DeleteFIARegulation")]
        public async Task<ActionResult<FIARegulationResponseModel>> DeleteFIARegulationAsync([FromBody] FIARegulationParamModel regulationParam)
        {
            if (regulationParam == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var fiaRegulationResponseModel = await fIARegulationRepository.DeleteFIARegulationAsync(regulationParam.RegulationID);
            if (fiaRegulationResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = fiaRegulationResponseModel.Message });
            if (fiaRegulationResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = fiaRegulationResponseModel.Message });
            return Ok(new { MessageId = fiaRegulationResponseModel.MessageID, MessageDescription = fiaRegulationResponseModel.Message, FIARegulation = fiaRegulationResponseModel.Regulation });
        }
    }
}
