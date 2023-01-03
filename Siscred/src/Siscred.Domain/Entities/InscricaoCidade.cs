namespace Siscred.Domain.Entities
{
    public class InscricaoCidade
    {
        public int Id { get; set; }

        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }

        public int InscricaoId { get; set; }
        public Inscricao Inscricao { get; set; }

        public int Preferencia { get; set; }
    }
}