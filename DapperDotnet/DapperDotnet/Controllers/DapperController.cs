using Data.DTO;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private readonly IDbInstance _dbInstance;
        public DapperController(IDbInstance dbInstance) {
            _dbInstance = dbInstance;
        }

        [HttpGet("UserList")]
        public async Task<IActionResult> GetData()
        {
            var data = await _dbInstance.GetData();  
            return Ok(data);
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var data = await _dbInstance.GetById(id);

            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest(new {message = "no data for the id"});
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserCreationDto data)
        {
            Boolean response = await _dbInstance.AddUser(data);
            if(response==true)return Ok(response);
            return BadRequest(new { message = "error in adding data" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto data)
        {
            bool response = await _dbInstance.UpdateUser(id, data);
            if (response == true) return Ok(response);
            return BadRequest(new { message = "Error in updating data" });
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User response = await _dbInstance.DeleteUser(id);
            if (response!= null) return Ok(response);
            return BadRequest(new { message = "element not found"});
        }
    }
}
