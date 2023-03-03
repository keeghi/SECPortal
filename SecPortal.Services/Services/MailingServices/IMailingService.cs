using System;
using System.Threading.Tasks;

namespace SecPortal.Services.Services.MailingServices
{
    public interface IMailingService
    {
        /// <summary>
        /// Method to send forgot password link through email
        /// </summary>
        /// <param name="user">Target user that has forgotten their password</param>
        /// <returns></returns>
        Task SendForgotPasswordEmail(Guid id);

        /// <summary>
        /// Method to send forgot password link through email
        /// </summary>
        /// <param name="user">Target user that has forgotten their password</param>
        /// <returns></returns>
        Task SendActivationEmailToUser(Guid id);

        Task UpdateStockReminder(string productName, int minimalStock);
    }
}
