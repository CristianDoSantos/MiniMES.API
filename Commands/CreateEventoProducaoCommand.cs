using MiniMES.API.Models;

namespace MiniMES.API.Commands
{
    public class CreateEventoProducaoCommand
    {
        public TipoEvento Tipo { get; set; }
        public int MaquinaId { get; set; }
        public int OrdemProducaoId { get; set; }

    }
}
