using System.Collections.Generic;

namespace Siscred.Application.ViewModel
{
    public class CidadeVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public EstadoVm Estado { get; set; }
        
        public ICollection<EditalVm> Editais { get; set; }
        public ICollection<InscricaoVm> Inscricoes { get; set; }
        public ICollection<InscricaoCidadeVm> InscricoesCidades { get; set; }
    }
}