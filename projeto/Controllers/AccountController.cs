using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Hackacont2024.Models;
using System.Threading.Tasks;

namespace Hackacont2024.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Action para registrar um novo usuário
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User usuario)
        {
            if (string.IsNullOrEmpty(usuario.Name) || string.IsNullOrEmpty(usuario.Senha))
            {
                return BadRequest("Nome e senha são obrigatórios!");
            }

            var user = new ApplicationUser { UserName = usuario.Name, Email = usuario.Email };
            var result = await _userManager.CreateAsync(user, usuario.Senha);

            if (result.Succeeded)
            {
                return Ok("Usuário cadastrado com sucesso!");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        // Action para realizar login de um usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (result.Succeeded)
            {
                return Ok("Usuário logado com sucesso!");
            }

            return BadRequest("Login ou senha incorretos.");
        }
    }
}
