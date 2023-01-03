using System.ComponentModel.DataAnnotations;

namespace Siscred.Application.ViewModel
{
    public class LoginVm
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]      
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]        
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }

        public string ReturnUrl { get; set; }
    }
}