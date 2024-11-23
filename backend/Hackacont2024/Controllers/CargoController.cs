using Microsoft.AspNetCore.Mvc;
using Hackacont2024.Data;
using Hackacont2024.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackacont2024.Controllers {
    [Route("api/controller")] // Defines the base route for all endpoints in this controller.
    [ApiController] // Specifies that this is an API controller, enabling features like model validation and automatic response formatting.
    public class CargoController : ControllerBase {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the application database context.
        public CargoController(ApplicationDbContext context) {
            _context = context;
        }

        // POST: api/controller
        // Adds a new cargo entry to the database.
        [HttpPost]
        public async Task<IActionResult> CreateCargo([FromBody] Cargo cargo) {
            // Validates the input data for required fields.
            if (string.IsNullOrEmpty(cargo.Category) || cargo.Quantity <= 0 || cargo.Weight <= 0) {
                return BadRequest("Categoria, quantidade e peso são obrigatórios!"); // Returns a bad request if validation fails.
            }

            _context.Cargas.Add(cargo); // Adds the new cargo to the context.
            await _context.SaveChangesAsync(); // Saves changes to the database.

            // Returns a response indicating the resource was created, with a reference to its details.
            return CreatedAtAction(nameof(GetCargo), new { id = cargo.Id }, cargo);
        }

        // GET: api/controller
        // Retrieves all cargo records from the database.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetCargo() {
            return await _context.Cargas.ToListAsync(); // Returns a list of all cargo entries.
        }

        // GET: api/controller/{id}
        // Retrieves a specific cargo entry by its ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id) {
            var cargo = await _context.Cargas.FindAsync(id); // Finds the cargo by ID.

            if (cargo == null) {
                return NotFound(); // Returns a 404 if the cargo does not exist.
            }

            return cargo; // Returns the found cargo.
        }

        // PUT: api/controller/{id}
        // Updates an existing cargo entry identified by its ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCargo(int id, [FromBody] Cargo cargo) {
            // Ensures the ID in the URL matches the ID in the provided data.
            if (id != cargo.Id) {
                return BadRequest("Carga inexistente!"); // Returns a bad request if the IDs do not match.
            }

            _context.Entry(cargo).State = EntityState.Modified; // Marks the cargo entry as modified.
            await _context.SaveChangesAsync(); // Saves the changes to the database.

            return NoContent(); // Returns a 204 No Content response indicating the update was successful.
        }

        // DELETE: api/controller/{id}
        // Deletes a specific cargo entry by its ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id) {
            var cargo = await _context.Cargas.FindAsync(id); // Finds the cargo by ID.

            if (cargo == null) {
                return NotFound(); // Returns a 404 if the cargo does not exist.
            }

            _context.Cargas.Remove(cargo); // Removes the cargo from the context.
            await _context.SaveChangesAsync(); // Saves changes to the database.

            return NoContent(); // Returns a 204 No Content response indicating the deletion was successful.
        }
    }
}
