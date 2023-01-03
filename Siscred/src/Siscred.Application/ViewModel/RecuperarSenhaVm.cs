using System.ComponentModel.DataAnnotations;

namespace Siscred.Application.ViewModel
{
    public class RecuperarSenhaVm
    {
        [Display(Name = "E-mail")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Captcha { get; set; }
    }
}