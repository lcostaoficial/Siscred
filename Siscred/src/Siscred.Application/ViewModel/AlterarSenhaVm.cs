using System.ComponentModel.DataAnnotations;

namespace Siscred.Application.ViewModel
{
    public class AlterarSenhaVm
    {
        [Display(Name = "Senha Antiga")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caracteres")]
        [MaxLength(16, ErrorMessage = "Máximo 16 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string SenhaAntiga { get; set; }

        [Display(Name = "Senha Nova")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caracteres")]
        [MaxLength(16, ErrorMessage = "Máximo 16 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string SenhaNova { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Captcha { get; set; }
    }
}