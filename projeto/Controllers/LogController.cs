using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hackacont2024.Data;
using Hackacont2024.Models;
using System.Data.Common;

namespace Hackacont2024.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog([FromBody] LogModel novoLog)
        {
            if (novoLog == null)
            {
                return BadRequest("Dados inválidos.");
            }

            if (string.IsNullOrEmpty(novoLog.Tipo) || (novoLog.Tipo != "Entrada" && novoLog.Tipo != "Saída"))
            {
                return BadRequest("O tipo do log deve ser 'Entrada' ou 'Saída'.");
            }

            novoLog.DataHora = DateTime.UtcNow; // Registra o horário atual do log

            try
            {
                _context.Log.Add(novoLog);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLog), new { id = novoLog.Id }, novoLog);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Erro ao salvar o log no banco de dados: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogModel>>> GetLogs()
        {
            try
            {
                return await _context.Log
                    .OrderByDescending(l => l.DataHora)
                    .ToListAsync();
            }
            catch (DbException ex)
            {
                return StatusCode(500, $"Erro ao acessar o banco de dados: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LogModel>> GetLog(int id)
        {
            var log = await _context.Log.FindAsync(id);

            if (log == null)
            {
                return NotFound("Log não encontrado.");
            }

            return log;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var log = await _context.Log.FindAsync(id);
            if (log == null)
            {
                return NotFound("Log não encontrado.");
            }

            try
            {
                _context.Log.Remove(log);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Erro ao deletar o log: {ex.Message}");
            }
        }
    }
}
