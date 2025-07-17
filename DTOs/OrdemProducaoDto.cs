using MiniMES.API.Models;

namespace MiniMES.API.DTOs
{
    public class OrdemProducaoDto
    {
        public int Id { get; set; }
        public string Produto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public StatusOrdem Status { get; set; }
    }
}
