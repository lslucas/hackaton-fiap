using System.Net.Mail;
using System.Net;

namespace ConsultasMedicas.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("noreply@consultasmedicas.com", "Consultas Médicas");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "your-password"; // Not used by MailHog

            try
            {
                // Set up the SMTP client
                SmtpClient client = new SmtpClient
                {
                    Host = "localhost",
                    Port = 1025, // Port for TLS/STARTTLS
                    EnableSsl = false, // Enable SSL for secure connection
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                // Create the email message
                MailMessage mailMessage = new MailMessage
                {
                    From = fromAddress,
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false, // Set to true if your email body is HTML
                };

                // Add recipient
                mailMessage.To.Add(toEmail);

                // Send the email
                client.Send(mailMessage);
                Console.WriteLine("Email enviado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Houve um erro no envio: {ex.Message}");
            }
        }
    }
}
