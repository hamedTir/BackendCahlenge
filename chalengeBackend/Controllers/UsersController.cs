using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChalengeBackend.Database.Model;
using ChalengeBackend.Database;
using ChalengeBackend.DTOs;

namespace ChalengeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Notes) // Include the related Notes
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;

        }

        // GET: api/Users/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<userUpdateModelDto>> GetUserById(int id)
        {
            var user = await _context.Users
               
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            var userById = new userUpdateModelDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Email = user.Email,
                Website = user.Website
            };
            return userById;

        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Check if the email address is already in use by another user
            var isEmailTaken = await _context.Users
                .AnyAsync(u => u.Email == user.Email);

            if (isEmailTaken)
            {
                return BadRequest("Email address is already in use by another user.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // GET: api/Users/{userId}/notes
        [HttpGet("{userId}/notes")]
        public async Task<ActionResult<IEnumerable<Note>>> GetUserNotes(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var notes = await _context.Notes
                .Where(n => n.UserId == userId)
                .ToListAsync();

            return notes;
        }


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, userUpdateModelDto user)
        {
            // Check if the user with the given ID exists
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            // Check if the email address is already in use by another user
            var isEmailTaken = await _context.Users
                .AnyAsync(u => u.Email == user.Email && u.Id != id);

            if (isEmailTaken)
            {
                return BadRequest("Email address is already in use by another user.");
            }

            // Apply updates to the existing user
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Age = user.Age;
            existingUser.Website = user.Website;

            try
            {
                // Update the user in the database
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();

                return NoContent(); // Return 204 No Content on successful update
            }
            catch (Exception ex)
            {
                // Handle any database update errors and return a 500 Internal Server Error
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
