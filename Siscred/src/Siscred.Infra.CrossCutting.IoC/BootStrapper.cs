using SimpleInjector;
using Siscred.Application.AppServices;
using Siscred.Application.Interfaces;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;
using Siscred.Domain.Services;
using Siscred.Infra.Data.Context;
using Siscred.Infra.Data.Repositories;

namespace Siscred.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static Container RegisterServices(Container container)
        {
            //App            
            container.Register<IUsuarioAppService, UsuarioAppService>(Lifestyle.Scoped);
            container.Register<ITokenAppService, TokenAppService>(Lifestyle.Scoped);
            container.Register<ITipoArquivoAppService, TipoArquivoAppService>(Lifestyle.Scoped);
            container.Register<IEditalAppService, EditalAppService>(Lifestyle.Scoped);
            container.Register<ICidadeAppService, CidadeAppService>(Lifestyle.Scoped);
            container.Register<IInscricaoAppService, InscricaoAppService>(Lifestyle.Scoped);
            container.Register<IProfissionalIndicadoAppService, ProfissionalIndicadoAppService>(Lifestyle.Scoped);
            container.Register<IArquivoAppService, ArquivoAppService>(Lifestyle.Scoped);

            //Domain            
            container.Register<IUsuarioService, UsuarioService>(Lifestyle.Scoped);
            container.Register<ITokenService, TokenService>(Lifestyle.Scoped);
            container.Register<ITipoArquivoService, TipoArquivoService>(Lifestyle.Scoped);
            container.Register<IEditalService, EditalService>(Lifestyle.Scoped);
            container.Register<ICidadeService, CidadeService>(Lifestyle.Scoped);
            container.Register<IInscricaoService, InscricaoService>(Lifestyle.Scoped);
            container.Register<IProfissionalIndicadoService, ProfissionalIndicadoService>(Lifestyle.Scoped);
            container.Register<IArquivoService, ArquivoService>(Lifestyle.Scoped);

            //Data           
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
            container.Register<ITokenRepository, TokenRepository>(Lifestyle.Scoped);
            container.Register<ITipoArquivoRepository, TipoArquivoRepository>(Lifestyle.Scoped);
            container.Register<IEditalRepository, EditalRepository>(Lifestyle.Scoped);
            container.Register<ICidadeRepository, CidadeRepository>(Lifestyle.Scoped);
            container.Register<IInscricaoRepository, InscricaoRepository>(Lifestyle.Scoped);
            container.Register<IProfissionalIndicadoRepository, ProfissionalIndicadoRepository>(Lifestyle.Scoped);
            container.Register<IArquivoRepository, ArquivoRepository>(Lifestyle.Scoped);

            //Context
            container.Register<MainContext>(Lifestyle.Scoped);

            return container;
        }
    }
}