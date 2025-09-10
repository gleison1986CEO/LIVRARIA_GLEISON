using System.Net.Mail;
using System.Net;

namespace sistema.Classes
{
    public class Email
    {
        public String Send(String? email, String? auth2fa)
        {
                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress("devgleisonsilveira@gmail.com");
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("devgleisonsilveira@gmail.com",  "vttc aida yzft xopm "); 
                smtp.Host = "smtp.gmail.com";            
                mail.To.Add(new MailAddress(email));
                mail.Subject = "SISTEMA";
                mail.IsBodyHtml = true;
                string st = "<strong>TOKEN: <p><h1>" + auth2fa + "</h1></p></strong><p>Copie o Token para fazer o login na plataforma Axis App!</p>";

                mail.Body = st;
                smtp.Send(mail);

            return "";

        }

       public String SendNotificationAtivo(String? email, String? auth2fa)
        {
                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress("devgleisonsilveira@gmail.com");
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("devgleisonsilveira@gmail.com",  "vttc aida yzft xopm "); 
                smtp.Host = "smtp.gmail.com";            
                mail.To.Add(new MailAddress(email));
                mail.Subject = "SISTEMA";
                mail.IsBodyHtml = true;
                string st = "<strong>USUÁRIO: <p><h1>" + email + "</h1></p></strong><p><h2>Já está logado no sistema.Para criar uma nova conexão deslogue o usuário do sistema!</h2></p>";

                mail.Body = st;
                smtp.Send(mail);

            return "";

        }

        public String SendConfirmation(String? email, String? auth2fa)
        {
                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress("devgleisonsilveira@gmail.com");
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("devgleisonsilveira@gmail.com",  "vttc aida yzft xopm "); 
                smtp.Host = "smtp.gmail.com";            
                mail.To.Add(new MailAddress(email));
                mail.Subject = "SISTEMA";
                mail.IsBodyHtml = true;
                string st = "<strong>Ola! Seja bem vindo ao SISTEMA. <p><h1>" + email + "</h1></p></strong><p><h2>Este é o seu primeiro acesso a nossa plataforma!</h2></p><p><h2>Este é o seu primeiro token para acessar nossa plataforma: " + auth2fa +"</h2></p>";

                mail.Body = st;
                smtp.Send(mail);

            return "";

        }



        public String SendError(String? email)
        {
                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress("devgleisonsilveira@gmail.com");
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("devgleisonsilveira@gmail.com",  "vttc aida yzft xopm "); 
                smtp.Host = "smtp.gmail.com";            
                mail.To.Add(new MailAddress(email));
                mail.Subject = "SISTEMA";
                mail.IsBodyHtml = true;
                string st = "<strong>Ola! Tivemos um problema ao ativar seu usuário. <p><h1>" + email + "</h1></p></strong><p><h2>Porfavor Contate o surporte!</h2></p>";

                mail.Body = st;
                smtp.Send(mail);

            return "";

        }
        
        public String SendConfirmationLogin(String? email)
        {
                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress("devgleisonsilveira@gmail.com");
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("devgleisonsilveira@gmail.com",  "vttc aida yzft xopm "); 
                smtp.Host = "smtp.gmail.com";            
                mail.To.Add(new MailAddress(email));
                mail.Subject = "SISTEMA";
                mail.IsBodyHtml = true;
                string st = "<strong>Usuário conectado ao SISTEMA. <p><h1>" + email + "</h1></p></strong><p><h2>Logado com sucesso!</h2></p><p><h2>* Para dúvidas, problemas ou sugestões nao exite em contactar o suporte!</h2></p>";

                mail.Body = st;
                smtp.Send(mail);

            return "";

        }
    }
}