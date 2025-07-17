namespace MiniMES.API.Models
{
    public enum TipoEvento
    {
        Inicio,
        Parada,
        Alerta,
        Fim
    }
    public class EventoProducaoModel
    {
        public int Id { get; set; }
        public TipoEvento Tipo { get; set; }
        public DateTime DataHora { get; set; } = DateTime.UtcNow;

        public int MaquinaId { get; set; }
        public MaquinaModel Maquina { get; set; } = null!;

        public int OrdemProducaoId { get; set; }
        public OrdemProducaoModel OrdemProducao { get; set; } = null!;
    }
}