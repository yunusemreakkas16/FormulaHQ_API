using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository carRepository;

        public CarController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        [HttpPost]
        [Route("AddCar")]
        public async Task<ActionResult<CarResponseModel>> AddCircuitAsync([FromBody] Car car)
        {
            if (car == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var carResponseModel = await carRepository.AddCarAsync(car);

            if (carResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = carResponseModel.Message });

            if (carResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = carResponseModel.Message });

            return Ok(new { MessageId = carResponseModel.MessageID, MessageDescription = carResponseModel.Message, User = carResponseModel.Car });
        }
        [HttpPost]
        [Route("CarDetail")]
        public async Task<ActionResult<CarResponseModel>> GetCarByIDAsync([FromQuery] CarParamModel carParamModel)
        {
            if (carParamModel == null || carParamModel.CarID == Guid.Empty)
                return BadRequest(new { MessageId = -2, MessageDescription = "Car ID is required." });
            var carResponseModel = await carRepository.GetCarByIDAsync(carParamModel.CarID);
            if (carResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = carResponseModel.Message });
            if (carResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = carResponseModel.Message });
            return Ok(new { MessageId = carResponseModel.MessageID, MessageDescription = carResponseModel.Message, User = carResponseModel.Car });
        }

        [HttpPost]
        [Route("CarList")]
        public async Task<ActionResult<CarListResponseModel>> GetAllCarsAsync()
        {
            var carListResponseModel = await carRepository.GetAllCarsAsync();
            if (carListResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = carListResponseModel.Message });
            if (carListResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = carListResponseModel.Message });
            return Ok(new { MessageId = carListResponseModel.MessageID, MessageDescription = carListResponseModel.Message, Cars = carListResponseModel.Cars });

        }
        [HttpPost]
        [Route("UpdateCar")]
        public async Task<ActionResult<CarResponseModel>> UpdateCarAsync([FromBody] Car car)
        {
            if (car == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var carResponseModel = await carRepository.UpdateCarAsync(car);
            if (carResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = carResponseModel.Message });
            if (carResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = carResponseModel.Message });
            return Ok(new { MessageId = carResponseModel.MessageID, MessageDescription = carResponseModel.Message, User = carResponseModel.Car });
        }
    }
}
