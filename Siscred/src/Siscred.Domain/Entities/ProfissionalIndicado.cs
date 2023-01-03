using Siscred.Infra.CrossCutting.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Siscred.Domain.Entities
{
    public class ProfissionalIndicado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public TipoIndicado TipoIndicado { get; set; }

        public int InscricaoId { get; set; }
        public Inscricao Inscricao { get; set; }

        public ICollection<Arquivo> Arquivos { get; set; }

        public void ValidarArquivosExigidos(IEnumerable<TipoArquivo> tiposExigidos, ICollection<Arquivo> arquivosInseridos)
        {
            var ids = new List<int>();
            foreach (var tipo in tiposExigidos)
            {
                if (arquivosInseridos.All(x => x.TipoArquivoId != tipo.Id)) ids.Add(tipo.Id);
            }
            if (ids.Any()) throw new Exception(Error.FileRequired);
        }
    }
}