using MiniMES.API.Models;
using System.Xml.Serialization;

namespace MiniMES.API.Commands
{
    [XmlRoot("EventoMaquina")]
    public class CreateEventoProducaoXmlCommand
    {
        [XmlElement("Tipo")]
        public TipoEvento Tipo { get; set; }

        [XmlElement("DataHora")]
        public DateTime DataHora { get; set; }

        [XmlElement("MaquinaId")]
        public int MaquinaId { get; set; }

        [XmlElement("OrdemProducaoId")]
        public int OrdemProducaoId { get; set; }
    }
}
