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
    [Route("eventos")]
    public class EventosController : ControllerBase
    {
        private readonly MiniMESContext _context;
        private readonly IMapper _mapper;
        public EventosController(MiniMESContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoProducaoDto>>> GetEventos()
        {
            var eventos =  await _context.Eventos
                .Include(e => e.Maquina)
                .Include(e => e.OrdemProducao)
                .ToListAsync();

            var eventosDto = _mapper.Map<IEnumerable<EventoProducaoDto>>(eventos);

            return Ok(eventosDto);
        }

        [HttpPost]
        public async Task<ActionResult<EventoProducaoDto>> CreateEvento(CreateEventoProducaoCommand command)
        {
            var evento = _mapper.Map<EventoProducaoModel>(command);

            var maquina = await _context.Maquinas.FindAsync(command.MaquinaId);
            var ordem = await _context.Ordens.FindAsync(command.OrdemProducaoId);

            if (maquina == null || ordem == null)
                return BadRequest("Máquina ou Ordem inválida.");

            evento.Maquina = maquina;
            evento.OrdemProducao = ordem;

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            var eventoDto = _mapper.Map<EventoProducaoDto>(evento);

            return CreatedAtAction(nameof(GetEventoById), new { id = evento.Id }, eventoDto);            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoProducaoDto>> GetEventoById(int id)
        {
            var evento = await _context.Eventos
                .Include(e => e.Maquina)
                .Include(e => e.OrdemProducao)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            var eventoDto = _mapper.Map<EventoProducaoDto>(evento);
            return Ok(eventoDto);
        }

        [HttpPost("xml")]
        [Consumes("application/xml")]
        public async Task<ActionResult<EventoProducaoDto>> CreateEventoFromXml([FromBody] CreateEventoProducaoXmlCommand command)
        {
            var evento = _mapper.Map<EventoProducaoModel>(command);

            var maquina = await _context.Maquinas.FindAsync(command.MaquinaId);
            var ordem = await _context.Ordens.FindAsync(command.OrdemProducaoId);

            if (maquina == null || ordem == null)
            {
                return BadRequest("Máquina ou Ordem de Produção inválida (do XML).");
            }

            evento.Maquina = maquina;
            evento.OrdemProducao = ordem;

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            var eventoDto = _mapper.Map<EventoProducaoDto>(evento);
            return CreatedAtAction(nameof(GetEventoById), new { id = evento.Id }, eventoDto);
        }
    }
}
