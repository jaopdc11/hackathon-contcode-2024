using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hackacont2024.Data;
using Hackacont2024.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {

    // The application database context to interact with the 'User' entity.
    private readonly ApplicationDbContext _context;

    // Constructor to inject the 'ApplicationDbContext' into the controller.
    public UserController(ApplicationDbContext context) {
        _context = context;
    }

    // Endpoint to register a new user.
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User usuario) {
        // Check if 'Name' or 'Senha' is missing in the request body.
        if (string.IsNullOrEmpty(usuario.Name) || string.IsNullOrEmpty(usuario.Senha)) {
            return BadRequest("Nome e senha são obrigatórios!"); // "Name and password are required!"
        } 

        // Generate a unique code for the new user.
        usuario.Codigo = GenerateUniqueCode();

        // Add the new user to the database.
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync(); // Save the changes asynchronously to the database.

        return Ok("Usuário cadastrado com sucesso!"); // "User registered successfully!"
    }

    // Private method to generate a unique code for each user.
    private int GenerateUniqueCode() {
        var random = new Random();
        int codigo;

        // Keep generating a random code until it's unique (not already used).
        do {
            codigo = random.Next(10000, 99999);
        } while (_context.Usuarios.Any(u => u.Codigo == codigo));

        return codigo;
    }

}
