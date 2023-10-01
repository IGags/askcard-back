using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Core.Settings.Models;
using Core.Smtp.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Core.Smtp;

public class SmtpSender : ISmtpSender, IDisposable
{
    private readonly IOptions<SmtpSettings> _options;
    private readonly SmtpClient _client = new();
    
    public SmtpSender(IOptions<SmtpSettings> options)
    {
        _options = options;
        var value = options.Value;
        _client.Connect(value.Client,value.Port, value.SslEnable);
        _client.Authenticate(value.SenderId, value.MailPassword);
    }
    
    public async Task SendAsync(string toMail, string topic, string body, bool isHtml = false, Encoding bodyEncoding = null)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(bodyEncoding, _options.Value.FromMail, _options.Value.FromMail));
        email.To.Add(new MailboxAddress(bodyEncoding, toMail, toMail));
        var textFormat = isHtml ? TextFormat.Html : TextFormat.Text;
        email.Body = new TextPart(textFormat)
        {
            Text = body
        };
        email.Subject = topic;
        
        await _client.SendAsync(email);
    }

    public void Dispose()
    {
        _client.Disconnect(true);
        _client?.Dispose();
    }
}