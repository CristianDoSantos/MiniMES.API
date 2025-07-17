namespace MiniMES.API.Commands
{
    public class CreateMaquinaCommand
    {
        public string Nome { get; set; }
        public string IP { get; set; }
        public bool Ativa { get; set; }
    }
}