namespace MiniMES.API.Models
{
    public class MaquinaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string IP { get; set; } = string.Empty;
        public bool Ativa { get; set; } = true;

        public ICollection<EventoProducaoModel> Eventos { get; set; } = new List<EventoProducaoModel>();
    }
}