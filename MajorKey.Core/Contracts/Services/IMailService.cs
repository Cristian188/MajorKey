using MajorKey.Core.Models;
using System.Threading.Tasks;

namespace MajorKey.Core.Contracts.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
