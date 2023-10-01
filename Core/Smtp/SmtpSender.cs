using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Core.Settings.Models;
using Core.Smtp.Interfaces;
using Microsoft.Extensions.Options;

namespace Core.Smtp;

public class SmtpSender : ISmtpSender, IDisposable
{
    private readonly IOptions<SmtpSettings> _options;
    private readonly SmtpClient _client = new();
    
    public SmtpSender(IOptions<SmtpSettings> options)
    {
        _options = options;
        var value = options.Value;
        _client.Port = value.Port;
        _client.Host = value.Client;
        _client.EnableSsl = value.SslEnable;
        var credentials = new NetworkCredential(value.SenderId, value.MailPassword);
        _client.UseDefaultCredentials = false;
        _client.Credentials = credentials;
    }
    
    public async Task SendAsync(string toMail, string topic, string body, bool isHtml = false, Encoding bodyEncoding = null)
    {
        bodyEncoding ??= Encoding.UTF8;
        var message = new MailMessage(_options.Value.FromMail, toMail);
        message.Body = body;
        message.Subject = topic;
        message.BodyEncoding = bodyEncoding;
        message.IsBodyHtml = isHtml;
        
        _client.SendAsync(message, new object());
    }

    public void Dispose()
    {
        _client?.Dispose();
    }
}