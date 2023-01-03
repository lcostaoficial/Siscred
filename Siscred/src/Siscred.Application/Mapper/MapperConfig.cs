using AutoMapper;
using Siscred.Application.ViewModel;
using Siscred.Domain.Entities;

namespace Siscred.Application.Mapper
{
    public class MapperConfig
    {
        public static IMapper Mapper;

        public static void RegisterMappings()
        {
            Mapper = new MapperConfiguration(x =>
            {
                x.CreateMap<Arquivo, ArquivoVm>().PreserveReferences().ReverseMap();
                x.CreateMap<Cidade, CidadeVm>().PreserveReferences().ReverseMap();
                x.CreateMap<Edital, EditalVm>().PreserveReferences().ReverseMap();
                x.CreateMap<Inscricao, InscricaoVm>().PreserveReferences().ReverseMap();    
                x.CreateMap<TipoArquivo, TipoArquivoVm>().PreserveReferences().ReverseMap();
                x.CreateMap<Usuario, UsuarioVm>().PreserveReferences().ReverseMap();
                x.CreateMap<Usuario, LoginVm>().PreserveReferences().ReverseMap();
                x.CreateMap<Usuario, RecuperarSenhaVm>().PreserveReferences().ReverseMap();
                x.CreateMap<Usuario, GestorVm>().PreserveReferences().ReverseMap();
                x.CreateMap<Token, TokenVm>().PreserveReferences().ReverseMap();
                x.CreateMap<ProfissionalIndicado, ProfissionalIndicadoVm>().PreserveReferences().ReverseMap();
            }).CreateMapper();
        }
    }
}