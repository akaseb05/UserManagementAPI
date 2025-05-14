using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> Users = new List<User>();
        private static int nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers() => Ok(Users);

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            user.Id = nextId++;
            Users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, User updatedUser)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound("User not found.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.Age = updatedUser.Age;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound("User not found.");

            Users.Remove(user);
            return NoContent();
        }
    }
}