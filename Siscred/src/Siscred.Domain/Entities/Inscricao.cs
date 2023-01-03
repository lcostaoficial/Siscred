using Siscred.Infra.CrossCutting.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Siscred.Domain.Entities
{
    public class Inscricao
    {
        public int Id { get; set; }
        public string Protocolo { get; set; }
        public DateTime Data { get; set; }
        public string Experiencia { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string ResponsavelLegal { get; set; }
        public string Cnpj { get; set; }
        public string ObjetoSocial { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string EmailCorporativo { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public Estado Estado { get; set; }
        public SituacaoInscricao SituacaoInscricao { get; set; }
        public string Justificativa { get; set; }    

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int EditalId { get; set; }
        public Edital Edital { get; set; }

        public ICollection<Arquivo> Arquivos { get; set; }
        public ICollection<InscricaoCidade> InscricoesCidades { get; set; }
        public ICollection<ProfissionalIndicado> ProfissionaisIndicados { get; set; }

        public void SetarProtocolo()
        {
            Protocolo = new Random().Next(100000, 999999).ToString();
        }

        public void SetarData()
        {
            Data = DateTime.Now;
        }

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