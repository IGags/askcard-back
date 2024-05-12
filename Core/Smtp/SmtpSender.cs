using System;
using System.Text;
using System.Threading.Tasks;
using Core.Settings.Models;
using Core.Smtp.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;

namespace Core.Smtp;

public class SmtpSender : ISmtpSender, IDisposable
{
    private readonly IOptions<SmtpSettings> _options;
    private readonly IOptions<DebugSettings> _debugOptions;
    private readonly ILogger<SmtpSender> _logger;
    private readonly SmtpClient _client = new();
    
    public SmtpSender(IOptions<SmtpSettings> options, IOptions<DebugSettings> debugOptions, ILogger<SmtpSender> logger)
    {
        _options = options;
        _debugOptions = debugOptions;
        _logger = logger;
        if (_debugOptions.Value.IsDebug is null || !_debugOptions.Value.IsDebug.Value)
        {
            var value = options.Value;
            _client.Connect(value.Client, value.Port, value.SslEnable);
            _client.Authenticate(value.SenderId, value.MailPassword);
        }
    }
    
    public async Task SendAsync(string toMail, string topic, string body, bool isHtml = false, Encoding bodyEncoding = null)
    {
        bodyEncoding ??= Encoding.UTF8;
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(bodyEncoding, _options.Value.FromMail, _options.Value.FromMail));
        email.To.Add(new MailboxAddress(bodyEncoding, toMail, toMail));
        var textFormat = isHtml ? TextFormat.Html : TextFormat.Text;
        email.Body = new TextPart(textFormat)
        {
            Text = body
        };
        email.Subject = topic;
        if (_debugOptions.Value.IsDebug is null || !_debugOptions.Value.IsDebug.Value)
        {
            await _client.SendAsync(email);
        }
#if DEBUG
        _logger.Log(LogLevel.Debug, $"SMTP MESSAGE WAS SENT {email}");
#endif
    }

    public void Dispose()
    {
        _client.Disconnect(true);
        _client?.Dispose();
    }
}