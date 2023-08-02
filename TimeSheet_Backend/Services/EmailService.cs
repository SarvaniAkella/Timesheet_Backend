using Azure;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TimeSheet_Backend.Services
{
    public class emailService
    {
        public interface IEmailService
        {
            Task SendVerificationEmailAsync(string email, string verificationToken);
            Task SendOtpVerificationEmailAsync(string email, string verificationToken);
        }



        public class EmailService : IEmailService
        {
            public async Task SendVerificationEmailAsync(string email, string verificationToken)
            {
                try
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse("sarvania@smbxl.com"));
                    message.To.Add(MailboxAddress.Parse(email));
                    message.Subject = "Account Verification";

                    string Response = "<div style=\"width:100%;background-color:white;text-align:center;margin:10px\">";
                    Response += "<h1>SMBXL Time Sheet</h1>";
                    Response += "<h3>Please use the following otp to verify your account</h3>";
                    Response += "<h1>OTP</h1>";
                    Response += $"<h1 style=\"font-size:50px;\">{verificationToken}</h1>";
                    Response += "<img style=\"width:50%\" src=\"https://business.adobe.com/customer-success-stories/media_11b3d9bfc1f37e690484ef1959da5c9eca88f96a3.png?width=750&format=png&optimize=medium\"/>";
                    Response += "</div>";


                    // Build the email body with the verification link containing the token
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = Response; 



                    message.Body = bodyBuilder.ToMessageBody();



                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                        client.Authenticate("sarvania@smbxl.com", "rxkndtxjmpvtdgjj");



                        await client.SendAsync(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or log errors related to sending emails
                    Console.WriteLine("Error sending email: " + ex.Message);
                }



            }
            public async Task SendOtpVerificationEmailAsync(string email, string verificationToken)
            {
                try
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse("vk4808606@gmail.com"));
                    message.To.Add(MailboxAddress.Parse(email));
                    message.Subject = "Otp Verification";


                    string Response = "<div style=\"width:100%;background-color:white;text-align:center;margin:10px\">";
                    Response += "<h1>SMBXL Time Sheet</h1>";                   
                    Response += "<h3>Please use the following otp to verify your account</h3>";
                    Response += "<h1>OTP</h1>";
                    Response += $"<h1 style=\"font-size:50px;\">{verificationToken}</h1>";
                    Response += "<img style=\"width:50%\" src=\"https://business.adobe.com/customer-success-stories/media_11b3d9bfc1f37e690484ef1959da5c9eca88f96a3.png?width=750&format=png&optimize=medium\"/>";
                    Response += "</div>";
                   
                    // Build the email body with the verification link containing the token
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = Response;



                    message.Body = bodyBuilder.ToMessageBody();



                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                        client.Authenticate("vk4808606@gmail.com", "xktqxpirwzxacqfz");



                        await client.SendAsync(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or log errors related to sending emails
                    Console.WriteLine("Error sending email: " + ex.Message);
                }



            }
        }
    }
}