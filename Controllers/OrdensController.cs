using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMES.API.Commands;
using MiniMES.API.Data;
using MiniMES.API.Models;

namespace MiniMES.API.Controllers
{
    [ApiController]
    [Route("ordens")]
    public class OrdensController : ControllerBase
    {
        private readonly MiniMESContext _context;

        public OrdensController(MiniMESContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemProducaoModel>>> GetOrdens()
        {
            return await _context.Ordens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemProducaoModel>> GetOrdem(int id)
        {
            var ordem = await _context.Ordens.FindAsync(id);
            if (ordem == null) return NotFound();
            return ordem;
        }

        [HttpPost]
        public async Task<ActionResult<OrdemProducaoModel>> CreateOrdem(CreateOrdemProducaoCommand command)
        {
            OrdemProducaoModel ordem = new OrdemProducaoModel() 
            { 
                DataInicio = command.DataInicio,
                DataFim = command.DataFim,
                Produto = command.Produto,
                Quantidade = command.Quantidade,
                Status = command.Status ?? StatusOrdem.Pendente
            };

            _context.Ordens.Add(ordem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrdem), new { id = ordem.Id }, ordem);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusOrdem status)
        {
            var ordem = await _context.Ordens.FindAsync(id);
            if (ordem == null) return NotFound();

            ordem.Status = status;
            if (status == StatusOrdem.Concluida)
                ordem.DataFim = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdem(int id)
        {
            var ordem = await _context.Ordens.FindAsync(id);
            if (ordem == null) return NotFound();

            _context.Ordens.Remove(ordem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
