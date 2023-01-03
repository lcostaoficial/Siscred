using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Siscred.Application.ViewModel
{
    public class ProfissionalIndicadoVm
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(14, ErrorMessage = "Máximo 14 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cpf { get; set; }

        [Display(Name = "Tipo de Credenciamento")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo obrigatório")]
        [EnumDataType(typeof(TipoIndicadoVm), ErrorMessage = "Campo obrigatório")]
        public TipoIndicadoVm TipoIndicado { get; set; }

        public int InscricaoId { get; set; }
        public InscricaoVm Inscricao { get; set; }

        public ICollection<ArquivoVm> Arquivos { get; set; }

        public int EditalId { get; set; }
    }
}