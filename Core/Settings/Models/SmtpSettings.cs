using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Settings.Models;

/// <summary>
/// Настройки отправки сообщений
/// </summary>
public class SmtpSettings : IValidateOptions<SmtpSettings>
{
    private const string SectionName = "SmtpSettings";
    private const string FromMailAddress = "FromMailAddress";
    private const string SmtpClient = "SmtpClient";
    private const string SmtpClientPort = "SmtpClientPort";
    private const string MailBoxId = "MailBoxId";
    private const string Password = "Password";
    private const string EnableSsl = "EnableSsl";

    public SmtpSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        FromMail = section.GetSection(FromMailAddress).Value;
        Client = section.GetSection(SmtpClient).Value;
        
        if (int.TryParse(section.GetSection(SmtpClientPort).Value, out var port))
        {
            Port = port;
        }
        else
        {
            Port = -1;
        }
        
        
        SenderId = section.GetSection(MailBoxId).Value;
        MailPassword = section.GetSection(Password).Value;
        
        SslEnable = bool.TryParse(section.GetSection(EnableSsl).Value, out var sslEnable) && sslEnable;
    }
    
    public string FromMail { get; }
    
    public string Client { get; }
    
    public int Port { get; }
    
    public string SenderId { get; }
    
    public string MailPassword { get; }
    
    public bool SslEnable { get; }
    
    public ValidateOptionsResult Validate(string name, SmtpSettings options)
    {
        var failureMessages = new List<string>();
        
        if (string.IsNullOrWhiteSpace(FromMail))
        {
            failureMessages.Add("Ящик отправителя не указан");
        }
        
        if (string.IsNullOrWhiteSpace(Client))
        {
            failureMessages.Add("Smtp клиент не указан");
        }
        
        if (string.IsNullOrWhiteSpace(SenderId))
        {
            failureMessages.Add("Идентификатор отправителя не указан");
        }
        
        if (string.IsNullOrWhiteSpace(MailPassword))
        {
            failureMessages.Add("Пароль не указан");
        }
        
        if (Port == -1)
        {
            failureMessages.Add("Порт не указан");
        }
        
        if (failureMessages.Any())
        {
            throw new OptionsValidationException(name, typeof(SmtpSettings), failureMessages);
        }

        return new ValidateOptionsResult();
    }
}