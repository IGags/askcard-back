using System.Text;
using System.Threading.Tasks;

namespace Core.Smtp.Interfaces;

public interface ISmtpSender
{
    public Task SendAsync(string toMail, string topic, string body, bool isHtml = false, Encoding bodyEncoding = null);
}