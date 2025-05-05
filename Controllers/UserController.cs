using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<UserResponseModel>> CreatePost([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var userResponseModel = await userRepository.CreateUserAsync(user);

            if (userResponseModel.MessageId == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = userResponseModel.MessageDescription });

            if (userResponseModel.MessageId == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = userResponseModel.MessageDescription });

            return Ok(new { MessageId = userResponseModel.MessageId, MessageDescription = userResponseModel.MessageDescription, User = userResponseModel.User });
        }

        [HttpPost]
        [Route("UserList")]
        public async Task<ActionResult<UserListResponseModel>> GetAllUsers()
        {
            var userListResponseModel = await userRepository.GetAllUsersAsync();
            if (userListResponseModel.MessageId == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = userListResponseModel.MessageDescription });
            if (userListResponseModel.MessageId == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = userListResponseModel.MessageDescription });
            return Ok(new { MessageId = userListResponseModel.MessageId, MessageDescription = userListResponseModel.MessageDescription, Users = userListResponseModel.Users });
        }

        [HttpPost]
        [Route("UserDetails")]
        public async Task<ActionResult<UserResponseModel>> GetUserById([FromBody] UserParamModel userParamModel)
        {
            if (userParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var userResponseModel = await userRepository.GetUserByIdAsync(userParamModel.UserID);
            if (userResponseModel.MessageId == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = userResponseModel.MessageDescription });
            if (userResponseModel.MessageId == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = userResponseModel.MessageDescription });
            return Ok(new { MessageId = userResponseModel.MessageId, MessageDescription = userResponseModel.MessageDescription, User = userResponseModel.User });
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<ActionResult<UserResponseModel>> UpdateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var userResponseModel = await userRepository.UpdateUserAsync(user);
            if (userResponseModel.MessageId == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = userResponseModel.MessageDescription });
            if (userResponseModel.MessageId == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = userResponseModel.MessageDescription });
            return Ok(new { MessageId = userResponseModel.MessageId, MessageDescription = userResponseModel.MessageDescription, User = userResponseModel.User });
        }
    }
}
