using System.Collections.Generic;
using System.Web;

namespace Siscred.Application.ViewModel
{
    public class ArquivoVm
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Caminho { get; set; }

        public int TipoArquivoId { get; set; }
        public TipoArquivoVm TipoArquivo { get; set; }

        public ICollection<InscricaoVm> Inscricoes { get; set; }

        public ICollection<ProfissionalIndicadoVm> ProfissionaisIndicados { get; set; }
      
        public HttpPostedFileBase ArquivoBinario { get; set; }
    }
}