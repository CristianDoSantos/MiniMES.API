using MiniMES.API.Models;

namespace MiniMES.API.DTOs
{
    public class EventoProducaoDto
    {
        public int Id { get; set; }
        public TipoEvento Tipo { get; set; }
        public DateTime DataHora { get; set; }
        public int MaquinaId { get; set; }
        public string MaquinaNome { get; set; } = string.Empty;
        public int OrdemProducaoId { get; set; }
        public string OrdemProducaoProduto { get; set; } = string.Empty;
        public StatusOrdem OrdemProducaoStatus { get; set; }
    }
}
