using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircuitController : ControllerBase
    {
        private readonly ICircuitRepository circuitRepository;

        public CircuitController(ICircuitRepository circuitRepository)
        {
            this.circuitRepository = circuitRepository;
        }

        [HttpPost]
        [Route("AddCircuit")]
        public async Task<ActionResult<CircuitResponseModel>> AddCircuitAsync([FromBody] Circuit circuit)
        {
            if (circuit == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var circuitResponseModel = await circuitRepository.AddCircuitAsync(circuit);

            if (circuitResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = circuitResponseModel.Message });

            if (circuitResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = circuitResponseModel.Message });

            return Ok(new { MessageId = circuitResponseModel.MessageID, MessageDescription = circuitResponseModel.Message, User = circuitResponseModel.Circuit });
        }

        [HttpPost]
        [Route("CircuitList")]
        public async Task<ActionResult<CircuitListResponseModel>> GetAllCircuitsAsync()
        {
            var circuitListResponseModel = await circuitRepository.GetAllCircuitsAsync();
            if (circuitListResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = circuitListResponseModel.Message });
            if (circuitListResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = circuitListResponseModel.Message });
            return Ok(new { MessageId = circuitListResponseModel.MessageID, MessageDescription = circuitListResponseModel.Message, Circuits = circuitListResponseModel.Circuits });
        }

        [HttpPost]
        [Route("CircuitDetails")]
        public async Task<ActionResult<CircuitResponseModel>> GetCircuitByIdAsync([FromBody] CircuitParamModel circuitParamModel)
        {
            if (circuitParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var circuitResponseModel = await circuitRepository.GetCircuitByIdAsync(circuitParamModel.CircuitID);
            if (circuitResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = circuitResponseModel.Message });
            if (circuitResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = circuitResponseModel.Message });
            return Ok(new { MessageId = circuitResponseModel.MessageID, MessageDescription = circuitResponseModel.Message, Circuit = circuitResponseModel.Circuit });
        }

        [HttpPost]
        [Route("UpdateCircuit")]
        public async Task<ActionResult<CircuitResponseModel>> UpdateCircuitAsync([FromBody] Circuit circuit)
        {
            if (circuit == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var circuitResponseModel = await circuitRepository.UpdateCircuitAsync(circuit);
            if (circuitResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = circuitResponseModel.Message });
            if (circuitResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = circuitResponseModel.Message });
            return Ok(new { MessageId = circuitResponseModel.MessageID, MessageDescription = circuitResponseModel.Message, User = circuitResponseModel.Circuit });
        }

    }
}
