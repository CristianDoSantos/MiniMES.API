namespace MiniMES.API.Models
{
    public enum StatusOrdem
    {
        Pendente,
        Executando,
        Concluida,
        Erro
    }
    public class OrdemProducaoModel
    {
        public int Id { get; set; }
        public string Produto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public StatusOrdem Status { get; set; } = StatusOrdem.Pendente;

        public ICollection<EventoProducaoModel> Eventos { get; set; } = new List<EventoProducaoModel>();
    }
}