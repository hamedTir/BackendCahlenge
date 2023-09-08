using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChalengeBackend.Database;
using ChalengeBackend.Database.Model;
using ChalengeBackend.DTOs;

namespace ChalengeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes(int userId)
        {
            // Get all notes for a specific user
            var notes = await _context.Notes
                .Where(n => n.UserId == userId)
                .ToListAsync();
            return notes;
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }



        // POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote([FromBody] NotesDTO noteDtoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if ModelState is not valid
            }

            try
            {
                // Ensure that the specified user exists (you should add proper validation here)
                var user = await _context.Users.FindAsync(noteDtoModel.UserId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Create a new Note with the provided content
                var newNote = new Note
                {
                    Content = noteDtoModel.Content,
                    DateCreated = DateTime.UtcNow, // Set DateCreated to current UTC time
                    DateModified = DateTime.UtcNow, // Initially, DateModified is the same as DateCreated
                    Views = 0, // Initialize Views to 0
                    Published = false, // Initially, set Published to false
                    UserId = noteDtoModel.UserId // Associate the note with the specified user
                };

                // Add the new note to the context and save it to the database
                _context.Notes.Add(newNote);
                await _context.SaveChangesAsync();

                // Return a 201 Created response with the created note
                return CreatedAtAction(nameof(GetNote), new { id = newNote.Id }, newNote);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors and return a 500 Internal Server Error
                return StatusCode(500, "An error occurred while creating the note.");
            }
        }



        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditNote(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
