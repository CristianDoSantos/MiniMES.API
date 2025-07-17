using MiniMES.API.Models;

namespace MiniMES.API.Commands
{
    public class CreateOrdemProducaoCommand
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public StatusOrdem? Status { get; set; }
    }
}