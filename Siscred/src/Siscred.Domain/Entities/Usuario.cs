using Siscred.Infra.CrossCutting.Helpers;
using System;
using System.Collections.Generic;

namespace Siscred.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }  
        public TipoUsuario TipoUsuario { get; set; }    
        
        public ICollection<Token> Tokens { get; set; }

        public void InverterAtivo()
        {
            Ativo = !Ativo;
        }

        public void AtivarConta()
        {
            Ativo = true;
        }

        public string EncriptarSenha(string senha)
        {
            return Senha = PasswordEncryption.ComputeHash(senha, "SHA512", null);
        }

        public string EncriptarSenha()
        {
            return Senha = PasswordEncryption.ComputeHash(Senha, "SHA512", null);
        }

        public bool ValidarSenha(string senha)
        {
            return PasswordEncryption.VerifyHash(senha, "SHA512", Senha);
        }
    }
}