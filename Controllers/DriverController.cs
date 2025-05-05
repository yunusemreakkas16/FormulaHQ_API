using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository driverRepository;

        public DriverController(IDriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        [HttpPost]
        [Route("AddDriver")]
        public async Task<ActionResult<DriverResponseModel>> CreatePost([FromBody] Driver driver)
        {
            if (driver == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var driverResponseModel = await driverRepository.AddDriverAsync(driver);

            if (driverResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = driverResponseModel.Message });

            if (driverResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = driverResponseModel.Message });

            return Ok(new { MessageId = driverResponseModel.MessageID, MessageDescription = driverResponseModel.Message, Driver = driverResponseModel.Driver });
        }

        [HttpPost]
        [Route("DriverList")]
        public async Task<ActionResult<DriverListResponseModel>> GetAllDrivers()
        {
            var driverListResponseModel = await driverRepository.GetAllDriversAsync();
            if (driverListResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = driverListResponseModel.Message });
            if (driverListResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = driverListResponseModel.Message });
            return Ok(new { MessageId = driverListResponseModel.MessageID, MessageDescription = driverListResponseModel.Message, Drivers = driverListResponseModel.Drivers });
        }
        [HttpPost]
        [Route("DriverDetail")]
        public async Task<ActionResult<DriverResponseModel>> GetDriverById([FromBody] DriverParamModel driverParamModel)
        {
            if (driverParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var driverResponseModel = await driverRepository.GetDriverByIdAsync(driverParamModel.DriverID);
            if (driverResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = driverResponseModel.Message });
            if (driverResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = driverResponseModel.Message });
            return Ok(new { MessageId = driverResponseModel.MessageID, MessageDescription = driverResponseModel.Message, Driver = driverResponseModel.Driver });
        }
        [HttpPost]
        [Route("UpdateDriver")]
        public async Task<ActionResult<DriverResponseModel>> UpdateDriver([FromBody] Driver driver)
        {
            if (driver == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var driverResponseModel = await driverRepository.UpdateDriverAsync(driver);
            if (driverResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = driverResponseModel.Message });
            if (driverResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = driverResponseModel.Message });
            return Ok(new { MessageId = driverResponseModel.MessageID, MessageDescription = driverResponseModel.Message, Driver = driverResponseModel.Driver });
        }
    }
}
