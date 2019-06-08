using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace ItSys.Common
{
    public class MailHelper
    {
        private readonly SmtpClient _smtpClient;
        private string _senderAddress;
        private string _senderName;

        public MailHelper(string smtpHost, string senderAddress, string password) :
            this(smtpHost, senderAddress, senderAddress, senderAddress, password, false, 0)
        {

        }

        public MailHelper(string smtpHost, string senderAddress, string senderName, string user, string password, bool isSsl, int port)
        {
            _senderAddress = senderAddress;
            _senderName = senderName;
            _smtpClient = new SmtpClient();
            _smtpClient.UseDefaultCredentials = true;
            _smtpClient.Host = smtpHost;
            _smtpClient.EnableSsl = isSsl;
            _smtpClient.Port = port == 0 ? (isSsl ? 465 : 25) : port;
            _smtpClient.Credentials = new NetworkCredential(user, password);
        }
        private MailMessage _buildMailMessage(string[] addressList, string title, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_senderAddress, _senderName ?? _senderAddress);
            foreach (var address in addressList)
            {
                mailMessage.To.Add(address);
            }
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Subject = title;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.Body = body;
            return mailMessage;
        }
        public string Send(string[] addressList, string title, string body)
        {
            var message = _buildMailMessage(addressList, title, body);
            string errorMessage = "";
            try
            {
                _smtpClient.Send(message);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            return errorMessage;

        }
        public async Task SendAsync(string[] addressList, string title, string body)
        {
            var message = _buildMailMessage(addressList, title, body);
            await _smtpClient.SendMailAsync(message);

        }
    }
}
