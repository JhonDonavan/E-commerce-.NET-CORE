using LojaVirtual.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class Contatoemail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("jhondonavan@gmail.com", "5674982546f#");
            smtp.EnableSsl = true;

            string corpoMsg = string.Format("<h3>Contato - loja virtual</h3>" +
                "<b>Nome: </b>{0} <br/>" +
                "<b>E-Mail: </b>{1} <br/>" +
                "<b>Text: </b>{2} <br/>" + 
                "<br/>" +
                "E-Mail enviado automaticamente do site LojaVirtual.",
                contato.Nome,
                contato.Email,
                contato.Texto
                );

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("jhondonavan@gmail.com");
            mensagem.To.Add("jhondonavan@gmail.com");
            mensagem.Subject = "Contato - loja virtual - E-Mail: " + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            smtp.Send(mensagem);
        }
    }
}
