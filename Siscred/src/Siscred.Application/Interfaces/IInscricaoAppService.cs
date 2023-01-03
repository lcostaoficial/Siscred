using Siscred.Application.ViewModel;
using System.Collections.Generic;

namespace Siscred.Application.Interfaces
{
    public interface IInscricaoAppService : IAppServiceBase<InscricaoVm>
    {
        InscricaoVm ObterPorCnpj(string cnpj);
        InscricaoVm ObterPorUsuarioId(int usuarioId);
        InscricaoVm ValidarDuplicidadeInscricaoUsuario(int usuarioId, int editalId);
        InscricaoVm ValidarDuplicidadeInscricaoProfissional(string cpf, int inscricaoId);
        IEnumerable<InscricaoVm> ObterTodosPorEditalId(int editalId);
        InscricaoVm ObterPorIdCustom(int id);
        int TotalInscricoes();
        InscricaoVm ObterPorIdParaComprovante(int id);
        InscricaoVm ObterPorIdParaFicha(int id);
    }
}