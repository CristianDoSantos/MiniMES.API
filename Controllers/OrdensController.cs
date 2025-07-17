using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMES.API.Commands;
using MiniMES.API.Data;
using MiniMES.API.DTOs;
using MiniMES.API.Models;

namespace MiniMES.API.Controllers
{
    [ApiController]
    [Route("ordens")]
    public class OrdensController : ControllerBase
    {
        private readonly MiniMESContext _context;
        private readonly IMapper _mapper;
        public OrdensController(MiniMESContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemProducaoDto>>> GetOrdens()
        {
            var ordens = await _context.Ordens.ToListAsync();
            var ordensDto = _mapper.Map<IEnumerable<OrdemProducaoDto>>(ordens);
            return Ok(ordensDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemProducaoDto>> GetOrdem(int id)
        {
            var ordem = await _context.Ordens.FindAsync(id);
            if (ordem == null) return NotFound();

            var ordemDto = _mapper.Map<OrdemProducaoDto>(ordem);
            return Ok(ordemDto);
        }

        [HttpPost]
        public async Task<ActionResult<OrdemProducaoDto>> CreateOrdem(CreateOrdemProducaoCommand command)
        {
            var ordem = _mapper.Map<OrdemProducaoModel>(command);

            _context.Ordens.Add(ordem);
            await _context.SaveChangesAsync();

            var ordemDto = _mapper.Map<OrdemProducaoDto>(ordem);
            return CreatedAtAction(nameof(GetOrdem), new { id = ordem.Id }, ordemDto);
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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrdem(int id, CreateOrdemProducaoCommand command)
        {
            var ordem = await _context.Ordens.FindAsync(id);
            if (ordem == null) return NotFound();

            _mapper.Map(command, ordem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdemExists(id)) return NotFound();
                else throw;
            }
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

        private bool OrdemExists(int id)
        {
            return _context.Ordens.Any(e => e.Id == id);
        }
    }
}
