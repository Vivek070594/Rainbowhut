using MimeKit;
using Rainbowhut1._0.Helper;
using Rainbowhut1._0.Model;
using System.Net.Mail;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Runtime;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Rainbowhut1._0.Domain
{
    public class MailDomain
    {
        private readonly IConfiguration configuration;
        public MailDomain(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        public async Task<int> SendEmailAsync(ContactUs mailRequest)
        {
            int result = 0;

            Templates tmp = new Templates();
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(configuration.GetSection("MailSettings").GetSection("DisplayNameforMail").Value, configuration.GetSection("MailSettings").GetSection("FromMail").Value));
            email.Sender = new MailboxAddress(configuration.GetSection("MailSettings").GetSection("DisplayNameforMail").Value, configuration.GetSection("MailSettings").GetSection("FromMail").Value);
            //email.Sender = MailboxAddress.Parse(configuration.GetSection("MailSettings").GetSection("FromMail").Value);
            email.To.Add(MailboxAddress.Parse(configuration.GetSection("MailSettings").GetSection("ToMail").Value));
            email.Subject = "Customer Mail";
            var builder = new BodyBuilder();
            builder.HtmlBody = tmp.CustomerMail(mailRequest).ToString();
            email.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(configuration.GetSection("MailSettings").GetSection("Host").Value, Convert.ToInt32(configuration.GetSection("MailSettings").GetSection("Port").Value), true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(configuration.GetSection("MailSettings").GetSection("FromMail").Value, configuration.GetSection("MailSettings").GetSection("Password").Value);
                    await client.SendAsync(email);
                    result = 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }


            return result;
        }
        public async Task<int> SendOtp()
        {
            int result = 0;
            int otp = GenerateRandomNo();
            Templates tmp = new Templates();
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(configuration.GetSection("MailSettings").GetSection("DisplayNameforOTP").Value, configuration.GetSection("MailSettings").GetSection("FromMail").Value));
            email.Sender = new MailboxAddress(configuration.GetSection("MailSettings").GetSection("DisplayNameforOTP").Value, configuration.GetSection("MailSettings").GetSection("FromMail").Value);
            //email.Sender = MailboxAddress.Parse(configuration.GetSection("MailSettings").GetSection("FromMail").Value);
            email.To.Add(MailboxAddress.Parse(configuration.GetSection("MailSettings").GetSection("ToMail").Value));
            email.Subject = "OTP";
            var builder = new BodyBuilder();
            builder.HtmlBody = tmp.OtpMail(otp).ToString();
            email.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(configuration.GetSection("MailSettings").GetSection("Host").Value, Convert.ToInt32(configuration.GetSection("MailSettings").GetSection("Port").Value), true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(configuration.GetSection("MailSettings").GetSection("FromMail").Value, configuration.GetSection("MailSettings").GetSection("Password").Value);
                    await client.SendAsync(email);
                    result = otp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }


            return result;
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

    }
}
