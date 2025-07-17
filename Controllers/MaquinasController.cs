using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMES.API.Commands;
using MiniMES.API.Data;
using MiniMES.API.DTOs;
using MiniMES.API.Models;

namespace MiniMES.API.Controllers
{
    [ApiController]
    [Route("maquinas")]
    public class MaquinasController : ControllerBase
    {
        private readonly MiniMESContext _context;
        private readonly IMapper _mapper;
        public MaquinasController(MiniMESContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaquinaDto>>> GetMaquinas()
        {
            var maquinas = await _context.Maquinas.ToListAsync();
            var maquinasDto = _mapper.Map<IEnumerable<MaquinaDto>>(maquinas);
            return Ok(maquinasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaquinaDto>> GetMaquina(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null) return NotFound();

            var maquinaDto = _mapper.Map<MaquinaDto>(maquina);
            return Ok(maquinaDto);
        }

        [HttpPost]
        public async Task<ActionResult<MaquinaDto>> CreateMaquina(CreateMaquinaCommand command)
        {
            var maquina = _mapper.Map<MaquinaModel>(command);

            _context.Maquinas.Add(maquina);
            await _context.SaveChangesAsync();

            var maquinaDto = _mapper.Map<MaquinaDto>(maquina);
            return CreatedAtAction(nameof(GetMaquina), new { id = maquina.Id }, maquinaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaquina(int id, CreateMaquinaCommand command)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null)
            {
                return NotFound();
            }

            _mapper.Map(command, maquina);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinaExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null) return NotFound();

            _context.Maquinas.Remove(maquina);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool MaquinaExists(int id)
        {
            return _context.Maquinas.Any(e => e.Id == id);
        }
    }
}