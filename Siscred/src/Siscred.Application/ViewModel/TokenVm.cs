using System;

namespace Siscred.Application.ViewModel
{
    public class TokenVm
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public Guid Chave { get; set; }
        public TipoTokenVm TipoToken { get; set; }
        public bool Ativo { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioVm Usuario { get; set; }
    }
}