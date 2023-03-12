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
        Task SendForgotPasswordEmail(int id);

        /// <summary>
        /// Method to send forgot password link through email
        /// </summary>
        /// <param name="user">Target user that has forgotten their password</param>
        /// <returns></returns>
        Task SendActivationEmailToUser(int id);

        Task UpdateStockReminder(string productName, int minimalStock);
    }
}
