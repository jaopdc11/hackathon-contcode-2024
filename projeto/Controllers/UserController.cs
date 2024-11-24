using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hackacont2024.Data;
using Hackacont2024.Models;
using Microsoft.AspNetCore.Http;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Endpoint para simular o login e redirecionar para o painel
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        // Simulando a verificação de login como sempre bem-sucedido
        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Senha) || loginRequest.Codigo == 0)
        {
            return BadRequest("Código e senha são obrigatórios!");
        }

        // Aqui a verificação sempre será "verdadeira"
        // Em um caso real, você buscaria os dados no banco, mas estamos simulando
        string usuario = "UsuarioSimulado"; // Nome do usuário simulado
        HttpContext.Session.SetString("UserLoggedIn", usuario);

        return Ok(new { success = true });
    }


    // Endpoint para registrar um novo usuário
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User usuario)
    {
        if (string.IsNullOrEmpty(usuario.Name) || string.IsNullOrEmpty(usuario.Senha))
        {
            return BadRequest("Nome e senha são obrigatórios!");
        }

        usuario.Codigo = GenerateUniqueCode();
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok("Usuário cadastrado com sucesso!");
    }

    // Método privado para gerar um código único para o usuário
    private int GenerateUniqueCode()
    {
        var random = new Random();
        int codigo;

        do
        {
            codigo = random.Next(10000, 99999);
        } while (_context.Usuarios.Any(u => u.Codigo == codigo));

        return codigo;
    }
}

// Modelo para os dados do login
public class LoginRequest
{
    public int Codigo { get; set; }
    public string Senha { get; set; }
}
