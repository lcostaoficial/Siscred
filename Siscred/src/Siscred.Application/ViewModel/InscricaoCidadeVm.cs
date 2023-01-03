namespace Siscred.Application.ViewModel
{
    public class InscricaoCidadeVm
    {
        public int Id { get; set; }

        public int CidadeId { get; set; }
        public CidadeVm Cidade { get; set; }

        public int InscricaoId { get; set; }
        public InscricaoVm Inscricao { get; set; }

        public int Preferencia { get; set; }
    }
}