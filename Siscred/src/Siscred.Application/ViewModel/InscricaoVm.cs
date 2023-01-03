using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Siscred.Application.ViewModel
{
    public class InscricaoVm
    {
        public int Id { get; set; }
        public string Protocolo { get; set; }
        public DateTime Data { get; set; }

        [Display(Name = "Experiência")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Experiencia { get; set; }

        [Display(Name = "Razão Social")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome Fantasia")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Responsável Legal")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string ResponsavelLegal { get; set; }

        [Display(Name = "CNPJ")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(18, ErrorMessage = "Máximo 18 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cnpj { get; set; }

        [Display(Name = "Objeto Social")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string ObjetoSocial { get; set; }

        [Display(Name = "Telefone")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(14, ErrorMessage = "Máximo 14 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(15, ErrorMessage = "Máximo 14 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Celular { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]     
        public string EmailCorporativo { get; set; }

        [Display(Name = "CEP")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(9, ErrorMessage = "Máximo 9 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cep { get; set; }

        [Display(Name = "Rua")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Rua { get; set; }

        [Display(Name = "Número")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(5, ErrorMessage = "Máximo 5 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Numero { get; set; }

        [Display(Name = "Bairro")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        [MinLength(1, ErrorMessage = "Mínimo 1 caractere")]
        [MaxLength(255, ErrorMessage = "Máximo 255 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo obrigatório")] 
        [EnumDataType(typeof(EstadoVm), ErrorMessage = "Campo obrigatório")]
        public EstadoVm Estado { get; set; }

        public SituacaoInscricaoVm SituacaoInscricao { get; set; }

        public string Justificativa { get; set; }      

        public int UsuarioId { get; set; }
        public UsuarioVm Usuario { get; set; }

        public int EditalId { get; set; }
        public EditalVm Edital { get; set; }        

        public ICollection<ArquivoVm> Arquivos { get; set; }
        public ICollection<InscricaoCidadeVm> InscricoesCidades { get; set; }
        public ICollection<ProfissionalIndicadoVm> ProfissionaisIndicados { get; set; }

        [Display(Name = "1º Opção de microrregião")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int MicroRegiao1Id { get; set; }

        [Display(Name = "2º Opção de microrregião")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int MicroRegiao2Id { get; set; }

        [Display(Name = "3º Opção de microrregião")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int MicroRegiao3Id { get; set; }

        public void SetInscricoesCidades(bool HabilitarMicrorregioes)
        {
            if (HabilitarMicrorregioes)
            {
                InscricoesCidades = new List<InscricaoCidadeVm>
                {
                    new InscricaoCidadeVm { CidadeId = MicroRegiao1Id, Preferencia = 1 },
                    new InscricaoCidadeVm { CidadeId = MicroRegiao2Id, Preferencia = 2 },
                    new InscricaoCidadeVm { CidadeId = MicroRegiao3Id, Preferencia = 3 }
                };
            }
        }

        public void ValidarInscricoesCidades(string msg)
        {
            var microRegioes = new int[] { MicroRegiao1Id, MicroRegiao2Id, MicroRegiao3Id };
            if (microRegioes.Length != microRegioes.Distinct().Count()) throw new Exception(msg);           
        }
    }
}