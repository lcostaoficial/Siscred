namespace Siscred.Application.ViewModel
{
    public enum EstadoVm
    {
        AC = 1,
        AL = 2,
        AP = 3,
        AM = 4,
        BA = 5,
        CE = 6,
        DF = 7,
        ES = 8,
        GO = 9,
        MA = 10,
        MT = 11,
        MS = 12,
        MG = 13,
        PA = 14,
        PB = 15,
        PR = 16,
        PE = 17,
        PI = 18,
        RR = 19,
        RO = 20,
        RJ = 21,
        RN = 22,
        RS = 23,
        SC = 24,
        SP = 25,
        SE = 26,
        TO = 27
    }

    public enum TipoUsuarioVm
    {
        Gestor = 1,
        Empresa = 2
    }

    public enum TipoTokenVm
    {
        Ativacao = 1,
        Recuperacao = 2,
        Alteracao = 3
    }

    public enum FinalidadeArquivoVm
    {
        Todos = 1,
        Empresa = 2,
        Indicado = 3
    }

    public enum SituacaoInscricaoVm
    {
        Inscrito = 1,
        Homologado = 2,
        Reprovado = 3,
        Pendente = 4
    }

    public enum TipoIndicadoVm
    {
        Facilitador = 1,
        Selecionador = 2
    }
}