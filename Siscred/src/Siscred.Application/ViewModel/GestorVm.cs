using System.ComponentModel.DataAnnotations;

namespace Siscred.Application.ViewModel
{
    public class GestorVm
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

        public string Senha { get; set; }

        public TipoUsuarioVm TipoUsuario { get; set; }

        public bool Ativo { get; set; } = true;
    }
}