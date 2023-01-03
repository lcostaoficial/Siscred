using System.Collections.Generic;

namespace Siscred.Domain.Entities
{
    public class TipoArquivo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public string Modelo { get; set; }
        public string NomeOriginal { get; set; }
        public bool Obrigatorio { get; set; }
        public FinalidadeArquivo FinalidadeArquivo { get; set; }
        public bool Ativo { get; set; }

        public ICollection<Arquivo> Arquivos { get; set; }
        public ICollection<Edital> Editais { get; set; }

        public void Atualizar(TipoArquivo model)
        {
            Descricao = model.Descricao;
            Observacao = model.Observacao;
            Modelo = model.Modelo;
            NomeOriginal = model.NomeOriginal;
            Ativo = model.Ativo;
            FinalidadeArquivo = model.FinalidadeArquivo;
            Obrigatorio = model.Obrigatorio;
        }

        public void InverterAtivo()
        {
            Ativo = !Ativo;
        }
    }
}