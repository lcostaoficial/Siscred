using System;

namespace Siscred.Domain.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }        
        public Guid Chave { get; set; }
        public TipoToken TipoToken { get; set; }
        public bool Ativo { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public void DesativarToken()
        {
            Ativo = false;
        }
    }
}