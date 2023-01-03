using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Siscred.Application.ViewModel
{
    public class TipoArquivoVm
    {
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "Observação")]    
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]       
        public string Observacao { get; set; }

        public string Modelo { get; set; }

        public string NomeOriginal { get; set; }

        [Display(Name = "Obrigatório")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public bool Obrigatorio { get; set; }

        [Display(Name = "Finalidade do Arquivo")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo obrigatório")]
        [EnumDataType(typeof(FinalidadeArquivoVm), ErrorMessage = "Campo obrigatório")]
        public FinalidadeArquivoVm FinalidadeArquivo { get; set; }

        public bool Ativo { get; set; } = true;

        [Display(Name = "Modelo")]      
        public HttpPostedFileBase ModeloArquivo { get; set; }

        public ICollection<ArquivoVm> Arquivos { get; set; }
        public ICollection<EditalVm> Editais { get; set; }
    }
}