using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Siscred.Application.ViewModel
{
    public class UsuarioVm
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caracteres")]
        [MaxLength(16, ErrorMessage = "Máximo 16 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }

        public TipoUsuarioVm TipoUsuario { get; set; }

        public bool Ativo { get; set; }       

        [Display(Name = "Código")]       
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Captcha { get; set; }

        [Display(Name = "Repetir senha")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caracteres")]
        [MaxLength(16, ErrorMessage = "Máximo 16 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string SenhaConfirmada { get; set; }

        public string ReturnUrl { get; set; }

        public ICollection<TokenVm> Tokens { get; set; }

        public void SetUsuarioEmpresa()
        {
            TipoUsuario = TipoUsuarioVm.Empresa;
        }

        public void SetUsuarioGestor()
        {
            TipoUsuario = TipoUsuarioVm.Gestor;
        }

        public void SetPrimeiroCadastroEmpresa()
        {
            Ativo = false;
        }

        public Guid AcoplarToken()
        {
            Tokens = new List<TokenVm>();
            var token = new TokenVm
            {
                Chave = Guid.NewGuid(),
                DataEmissao = DateTime.Now,
                TipoToken = TipoTokenVm.Ativacao,
                Ativo = true
            };
            Tokens.Add(token);
            return token.Chave;
        }
    }
}