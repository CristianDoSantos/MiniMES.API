using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMES.API.Commands;
using MiniMES.API.Data;
using MiniMES.API.Models;

namespace MiniMES.API.Controllers
{
    [ApiController]
    [Route("maquinas")]
    public class MaquinasController : ControllerBase
    {
        private readonly MiniMESContext _context;

        public MaquinasController(MiniMESContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaquinaModel>>> GetMaquinas()
        {
            return await _context.Maquinas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaquinaModel>> GetMaquina(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null) return NotFound();
            return maquina;
        }

        [HttpPost]
        public async Task<ActionResult<MaquinaModel>> CreateMaquina(CreateMaquinaCommand command)
        {
            MaquinaModel maquina = new MaquinaModel()
            {
                Nome = command.Nome,
                IP = command.IP,
                Ativa = command.Ativa,
            };
            _context.Maquinas.Add(maquina);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMaquina), new { id = maquina.Id }, maquina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaquina(int id, CreateMaquinaCommand command)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null)
                return NotFound();

            maquina.Ativa = command.Ativa;
            maquina.IP = command.IP;
            maquina.Nome = command.Nome;
            
            _context.Entry(maquina).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null) return NotFound();

            _context.Maquinas.Remove(maquina);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
