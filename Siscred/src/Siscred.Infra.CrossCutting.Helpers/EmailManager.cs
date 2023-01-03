using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Siscred.Infra.CrossCutting.Helpers
{
    public class EmailManager
    {        
        public string Assunto { get; set; }     
        public string Corpo { get; set; }      
        public ArrayList Destinatarios { get; set; }
        public string Remetente => SettingsManager.EmailRemetente;
        
        public EmailManager(ArrayList destinatarios)
        {
            Destinatarios = destinatarios;
        }

        public async Task<bool> Enviar()
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(Remetente, "SEBRAE-RO"),
                Priority = MailPriority.Normal,
                IsBodyHtml = true,
                Subject = Assunto,
                Body = Corpo,
                SubjectEncoding = Encoding.GetEncoding("ISO-8859-1"),
                BodyEncoding = Encoding.GetEncoding("ISO-8859-1")
            };    
            foreach (var d in Destinatarios) mailMessage.To.Add(d.ToString());
            var smtp = new SmtpClient
            {
                Host = SettingsManager.EmailHost,
                EnableSsl = SettingsManager.EmailSeguro,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(Remetente, SettingsManager.EmailSenha),
                Port = SettingsManager.EmailPorta
            };
            await smtp.SendMailAsync(mailMessage);
            return true;
        }

        public void AtivarConta(string usuario, string destinatario, string url)
        {
            Assunto = "Ativar conta no sistema de credenciamento do SEBRAE-RO";
            Destinatarios = new ArrayList { destinatario.ToLower() };
            Corpo = $@"
                    <section style='width: 400px; padding: 1em;'>
                        <h1 style='text-align: center; text-transform: uppercase;margin: 0'>Sistema de Credenciamento</h1>
                        <hr />
                        <p>Olá {usuario}<br /><br />Para ativar sua conta clique <a href='http://{url}'> aqui</a>, ou no botão abaixo:</p>
                        <hr />
                        <a href='http://{url}' style='display: block;
                                           background: #E0E1E2;
                                           color: rgba(0, 0, 0, .6); 
                                           padding: 1em; 
                                           margin:1em 0;
                                           text-transform: uppercase;
                                           font-weight: 700; 
                                           text-align: center; 
                                           text-decoration: none;
                                           border-radius: .28571429rem; 
                                           box-shadow: 0 0 0 1px transparent inset, 0 0 0 0 rgba(34, 36, 38, .15) inset;
                                           -webkit-transition: opacity .1s ease, background-color .1s ease, color .1s ease, box-shadow .1s ease, background .1s ease;
                                           transition: opacity .1s ease, background-color .1s ease, color .1s ease, box-shadow .1s ease, background .1s ease; '>
                            Ativar conta
                        </a>                        
                        <hr />
                        <h5 style='text-align: center; color: rgba(0, 0, 0, .5); text-transform: uppercase;margin: 0'>UTIC - Unidade de Tecnologia da Informação e Comunicação </h5>
                    </section>
                ";
        }

        public void RecuperarConta(string usuario, string destinatario, string url)
        {
            Assunto = "Recuperar conta no sistema de credenciamento do SEBRAE-RO";
            Destinatarios = new ArrayList { destinatario.ToLower() };
            Corpo = $@"
                    <section style='width: 400px; padding: 1em;'>
                        <h1 style='text-align: center; text-transform: uppercase;margin: 0'>Sistema de Credenciamento</h1>
                        <hr />
                        <p>Olá {usuario}<br /><br />Para recuperar sua conta clique <a href='http://{url}'> aqui</a>, ou no botão abaixo:</p>
                        <hr />
                        <a href='http://{url}' style='display: block;
                                           background: #E0E1E2;
                                           color: rgba(0, 0, 0, .6); 
                                           padding: 1em; 
                                           margin:1em 0;
                                           text-transform: uppercase;
                                           font-weight: 700; 
                                           text-align: center; 
                                           text-decoration: none;
                                           border-radius: .28571429rem; 
                                           box-shadow: 0 0 0 1px transparent inset, 0 0 0 0 rgba(34, 36, 38, .15) inset;
                                           -webkit-transition: opacity .1s ease, background-color .1s ease, color .1s ease, box-shadow .1s ease, background .1s ease;
                                           transition: opacity .1s ease, background-color .1s ease, color .1s ease, box-shadow .1s ease, background .1s ease; '>
                            Recuperar conta
                        </a>                        
                        <hr />
                        <h5 style='text-align: center; color: rgba(0, 0, 0, .5); text-transform: uppercase;margin: 0'>UTIC - Unidade de Tecnologia da Informação e Comunicação </h5>
                    </section>
                ";
        }
    }
}