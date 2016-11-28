namespace Devager.Net
{
    using System;
    using System.Net;
    using System.Net.Mail;
    public class eParams
    {
        public string GonderenAdres { get; set; }
        public string GonderenSifre { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string Kim { get; set; }
        public string Kime { get; set; }

    }

    public static class Mail
    {
        public static string MailGonder(eParams param)
        {
            //https://www.google.com/settings/security/lesssecureapps
            //Hata durumunda bu adresten ayarları kaldırıyoruz...

            try
            {
                var fromAddress = new MailAddress(param.GonderenAdres);
                var fromPassword = param.GonderenSifre;
                var toAddress = new MailAddress(param.Kim);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(toAddress.Address, param.Kime)
                {
                    Subject = param.Baslik,
                    Body = param.Icerik
                })

                    smtp.Send(message);

                return "Mail başarıyla gönderildi..";
            }
            catch (Exception ex)
            {
                return "Hata : " + ex.Message + "\r\n" +
                    "https://www.google.com/settings/security/lesssecureapps";
            }

        }
    }
}
