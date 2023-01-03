using System.Collections.Generic;

namespace Siscred.Domain.Entities
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Estado Estado { get; set; }
        
        public ICollection<Edital> Editais { get; set; }
        public ICollection<Inscricao> Inscricoes { get; set; }
        public ICollection<InscricaoCidade> InscricoesCidades { get; set; }
    }
}