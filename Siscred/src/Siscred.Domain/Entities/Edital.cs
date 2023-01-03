using System;
using System.Collections.Generic;

namespace Siscred.Domain.Entities
{
    public class Edital
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime DataEncerramento { get; set; }
        public string AnexoEdital { get; set; }
        public string NomeOriginal { get; set; }
        public ICollection<Inscricao> Inscricoes { get; set; }
        public ICollection<Cidade> Cidades { get; set; }
        public ICollection<TipoArquivo> TiposArquivos { get; set; }
        public bool HabilitarMicrorregiao { get; set; }
        public bool Ativo { get; set; }

        public void InverterAtivo()
        {
            Ativo = !Ativo;
        }
    }
}