using System;
using System.ComponentModel.DataAnnotations;

namespace Siscred.Application.ViewModel
{
    public class RedefinirSenhaVm
    {
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caractere")]
        [MaxLength(16, ErrorMessage = "Máximo 16 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }

        [Display(Name = "Repetir senha")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caractere")]
        [MaxLength(16, ErrorMessage = "Máximo 16 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string SenhaConfirmada { get; set; }

        public Guid Chave { get; set; }
    }
}