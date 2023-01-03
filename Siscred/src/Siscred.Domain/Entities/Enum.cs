namespace Siscred.Domain.Entities
{
    public enum Estado
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

    public enum TipoUsuario
    {     
        Gestor = 1,
        Empresa = 2
    }

    public enum TipoToken
    {
        Ativacao = 1,
        Recuperacao = 2,
        Alteracao = 3
    }

    public enum FinalidadeArquivo
    {
        Todos = 1,
        Empresa = 2,
        Indicado = 3
    }

    public enum SituacaoInscricao
    {
        Inscrito = 1,
        Homologado = 2,
        Reprovado = 3,
        Pendente = 4
    }

    public enum TipoIndicado
    {
        Facilitador = 1,
        Selecionador = 2
    }
}