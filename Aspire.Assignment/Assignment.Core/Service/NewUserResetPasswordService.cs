using Assignment.Contracts.Data.Entities;
using Assignment.Core.Filters;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Assignment.Core.Service
{
    public static class NewUserResetPasswordService
    {
        public static void SendPasswordResetMail(User user,IConfiguration configuration)
        {
            try
            {
                var message = new MailMessage();
                string encryptUser = CryptFilter.Encrypt(string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), user.Username));
                string resetPasswordURL = string.Concat(configuration.GetValue<string>("mailSettings:smtp:resetPasswordUrl"), encryptUser);
                string loginURL = string.Format(configuration.GetValue<string>("mailSettings:smtp:loginUrl"));
                message.From= new MailAddress(configuration.GetValue<string>("mailSettings:smtp:email"));
                message.To.Add(user.Email);
                message.Subject = "E-Product - your Credentails";

                message.IsBodyHtml = true;
                var htmlView =
                    AlternateView.CreateAlternateViewFromString(
                            new StringBuilder().AppendFormat(
                                "<br>Hi {0}," +
                                "<br>Click on the link below to reset your password." +
                                "<br><a href = \"{1}\">Reset Password</a> <br>" +
                                "<br>Please make sure to reset your password when you next log in.<br><br><a href = \"{2}\">Return to Login</a></div><div style=\"padding:8px;text-align:center;color:#696969; border-top-color: #CFD6DE; border-bottom-color: #A5B3C5; border-left-color: #BAC5CF; border-right-color: #BAC5CF; padding:5px; background-color:#F1F5FB;\">&copy; E-Product " + DateTime.Now.Year + " - All Rights Reserved</div></div>",
                                user.Username, resetPasswordURL, loginURL).ToString(), null, System.Net.Mime.MediaTypeNames.Text.Html);
                NetworkCredential NetworkCred = new NetworkCredential(configuration.GetValue<string>("mailSettings:smtp:userName"), configuration.GetValue<string>("mailSettings:smtp:password"));
                message.AlternateViews.Add(htmlView);
                var smtp = new SmtpClient
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = NetworkCred,
                    DeliveryMethod=SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = configuration.GetValue<string>("mailSettings:smtp:specifiedPickupDirectory:pickupDirectoryLocation")
                };
                smtp.Send(message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
