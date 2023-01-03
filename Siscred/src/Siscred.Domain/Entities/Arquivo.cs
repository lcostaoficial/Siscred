using System.Collections.Generic;

namespace Siscred.Domain.Entities
{
    public class Arquivo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }

        public int TipoArquivoId { get; set; }
        public TipoArquivo TipoArquivo { get; set; }

        public ICollection<Inscricao> Inscricoes { get; set; }
        public ICollection<ProfissionalIndicado> ProfissionaisIndicados { get; set; }   
    }
}