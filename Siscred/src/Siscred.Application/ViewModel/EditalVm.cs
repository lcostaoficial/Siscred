using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Siscred.Application.ViewModel
{
    public class EditalVm
    {
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "Data de Publicação")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataPublicacao { get; set; }

        [Display(Name = "Data de Término")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataEncerramento { get; set; }

        public string AnexoEdital { get; set; }

        public string NomeOriginal { get; set; }

        public ICollection<InscricaoVm> Inscricoes { get; set; }

        [Display(Name = "Micro-Regiões Abrangidas")]
        public int[] CidadesIds { get; set; }
        public ICollection<CidadeVm> Cidades { get; set; }

        [Display(Name = "Habilitar Microrregiões")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public bool HabilitarMicrorregiao { get; set; }

        [Display(Name = "Arquivos Obrigatórios")]
        public int[] TiposArquivosIds { get; set; }
        public ICollection<TipoArquivoVm> TiposArquivos { get; set; }

        public bool Ativo { get; set; } = true;

        [Display(Name = "Anexo Edital")]        
        public HttpPostedFileBase ArquivoEdital { get; set; }

        public void SetCidadesIds()
        {
            if (Cidades != null && Cidades.Any()) CidadesIds = Cidades.Select(x => x.Id).ToArray();
        }

        public void SetTiposArquivosIds()
        {
            if (TiposArquivos != null && TiposArquivos.Any()) TiposArquivosIds = TiposArquivos.Select(x => x.Id).ToArray();
        }
    }
}