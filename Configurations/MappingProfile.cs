using AutoMapper;
using MiniMES.API.Commands;
using MiniMES.API.DTOs;
using MiniMES.API.Models;

namespace MiniMES.API.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventoProducaoModel, EventoProducaoDto>()
                .ForMember(dest => dest.MaquinaNome, opt => opt.MapFrom(src => src.Maquina.Nome))
                .ForMember(dest => dest.OrdemProducaoProduto, opt => opt.MapFrom(src => src.OrdemProducao.Produto))
                .ForMember(dest => dest.OrdemProducaoStatus, opt => opt.MapFrom(src => src.OrdemProducao.Status));
            CreateMap<MaquinaModel, MaquinaDto>();
            CreateMap<OrdemProducaoModel, OrdemProducaoDto>();

            CreateMap<CreateEventoProducaoCommand, EventoProducaoModel>()
                .ForMember(dest => dest.DataHora, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CreateMaquinaCommand, MaquinaModel>();

            CreateMap<CreateOrdemProducaoCommand, OrdemProducaoModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? StatusOrdem.Pendente));

            CreateMap<CreateEventoProducaoXmlCommand, EventoProducaoModel>();

        }
    }
}
