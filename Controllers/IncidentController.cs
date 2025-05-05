using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentRepository incidentRepository;

        public IncidentController(IIncidentRepository incidentRepository)
        {
            this.incidentRepository = incidentRepository;
        }

        [HttpPost]
        [Route("AddIncident")]
        public async Task<ActionResult<IncidentResponseModel>> AddIncidentAsync([FromBody] Incident incident)
        {
            if (incident == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var IncidentResponseModel = await incidentRepository.AddIncidentAsync(incident);

            if (IncidentResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = IncidentResponseModel.Message });

            if (IncidentResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = IncidentResponseModel.Message });

            return Ok(new { MessageId = IncidentResponseModel.MessageID, MessageDescription = IncidentResponseModel.Message, User = IncidentResponseModel.Incident });
        }

        [HttpPost]
        [Route("IncidentList")]
        public async Task<ActionResult<IncidentListResponseModel>> GetAllIncidentsAsync()
        {
            var IncidentListResponseModel = await incidentRepository.GetAllIncidentsAsync();
            if (IncidentListResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = IncidentListResponseModel.Message });
            if (IncidentListResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = IncidentListResponseModel.Message });
            return Ok(new { MessageId = IncidentListResponseModel.MessageID, MessageDescription = IncidentListResponseModel.Message, Incidents = IncidentListResponseModel.Incidents });
        }
        [HttpPost]
        [Route("IncidentDetails")]
        public async Task<ActionResult<IncidentResponseModel>> GetIncidentByIdAsync([FromBody] IncidentParamModel incidentParamModel)
        {
            if (incidentParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var IncidentResponseModel = await incidentRepository.GetIncidentByIDAsync(incidentParamModel);
            if (IncidentResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = IncidentResponseModel.Message });
            if (IncidentResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = IncidentResponseModel.Message });
            return Ok(new { MessageId = IncidentResponseModel.MessageID, MessageDescription = IncidentResponseModel.Message, Incident = IncidentResponseModel.Incident });
        }
        [HttpPost]
        [Route("UpdateIncident")]
        public async Task<ActionResult<IncidentResponseModel>> UpdateIncidentAsync([FromBody] Incident incident)
        {
            if (incident == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var IncidentResponseModel = await incidentRepository.UpdateIncidentAsync(incident);
            if (IncidentResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = IncidentResponseModel.Message });
            if (IncidentResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = IncidentResponseModel.Message });
            return Ok(new { MessageId = IncidentResponseModel.MessageID, MessageDescription = IncidentResponseModel.Message, User = IncidentResponseModel.Incident });
        }
    }
}
