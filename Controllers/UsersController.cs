using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ShelfSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ShelfSpaceContext _context;

        public UsersController(ShelfSpaceContext context)
        {
            _context = context;
        }

        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] User user)
        {
            if (user == null || !_context.User.Any(u => u.Id == user.Id))
            {
                return BadRequest("Invalid user data.");
            }

            var existingUser = _context.User.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;
            existingUser.Phone = user.Phone;
            existingUser.BirthDate = user.BirthDate;

            _context.SaveChanges();

            return Ok("User profile updated successfully.");
        }
    }
}
