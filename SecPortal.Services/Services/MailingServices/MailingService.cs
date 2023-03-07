using System;
using System.Linq;
using System.Threading.Tasks;
using SecPortal.Services.Services.MailingServices.MailingViewModels;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using SecPortal.Entities.Data;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Managers.LoggerManager;
using SecPortal.Commons.Infrastructures;
using Polly;
using Newtonsoft.Json.Linq;
using Mailjet.Client;
using Mailjet.Client.Resources;
using SecPortal.Commons.Exceptions;

namespace SecPortal.Services.Services.MailingServices
{
    //public class MailingService : IMailingService
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly ISMTPCredential _credential;
    //    private readonly IDataContext _context;
    //    private readonly ILoggerManager _loggerManager;

    //    public MailingService(ISMTPCredential credential, IDataContext context, UserManager<ApplicationUser> userManager, ILoggerManager loggerManager)
    //    {
    //        _credential = credential;
    //        _context = context;
    //        _userManager = userManager;
    //        _loggerManager = loggerManager;
    //    }

    //    #region EMAIL PROCESSOR
    //    /// <summary>
    //    /// Method to send forgot password link through email
    //    /// </summary>
    //    /// <param name="user">Target user that has forgotten their password</param>
    //    /// <returns></returns>
    //    public async Task SendForgotPasswordEmail(Guid id)
    //    {
    //        var user = _context.Users.SingleOrDefault(x => x.Id == id);
    //        try
    //        {
    //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    //            var template = new ResetPasswordLinkMessageTemplate(token, _credential.WebRoot, _credential.Domain);
    //            template.AddDestination(user.ToString(), user.Email);
    //            await this.SendMailAsync(template);
    //        }
    //        catch (Exception ex)
    //        {
    //            _loggerManager.LogException(nameof(SendForgotPasswordEmail), ex, id);
    //        }
    //    }

    //    /// <summary>
    //    /// Welcome email for Employee Representative
    //    /// </summary>
    //    /// <param name="userId">The target user that the system going to email</param>
    //    /// <returns></returns>
    //    public async Task SendActivationEmailToUser(Guid userId)
    //    {
    //        try
    //        {
    //            var user = _context.Users.Single(x => x.Id == userId);
    //            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //            var template = new EmployeeWelcomeEmailTemplate(user.Id, token, _credential.WebRoot, _credential.Domain);
    //            template.AddDestination(user.ToString(), user.Email);
    //            await this.SendMailAsync(template);
    //        }
    //        catch (Exception ex)
    //        {
    //            _loggerManager.LogException(nameof(SendActivationEmailToUser), ex, userId);
    //        }
    //    }

    //    public async Task UpdateStockReminder(string productName, int minimalStock)
    //    {
    //        var template = new StockWarningEmailTemplate(productName, minimalStock, _credential.WebRoot);
    //        template.AddDestination("Rheine Adithia", "rheine.adithia@gmail.com");
    //        await this.SendMailAsync(template);
    //    }
    //    #endregion

    //    #region CORE
    //    public async Task SendMailAsync(IMailingTemplate mailTemplate, bool isBcc = false)
    //    {
    //        MailjetClient client = new MailjetClient(_credential.Key, _credential.Secret)
    //        {
    //            Version = ApiVersion.V3_1,
    //        };

    //        MailjetRequest request = new MailjetRequest
    //        {
    //            Resource = Send.Resource,
    //        }
    //        .Property(Send.Messages, new JArray {
    //           new JObject
    //           {
    //               {
    //                   "From",
    //                   new JObject
    //                   {
    //                       {"Email", _credential.FromEmail},
    //                       {"Name", _credential.FromName}
    //                   }
    //               },
    //               {
    //                   "To",
    //                   mailTemplate.ConstructDestinations()
    //               },
    //               {
    //                    "Subject",
    //                    mailTemplate.Subject
    //               },
    //               {
    //                    "TextPart",
    //                    mailTemplate.ToMessageBodyText()
    //               },
    //               {
    //                    "HTMLPart",
    //                    mailTemplate.ToMessageBodyHtml()
    //               },
    //               {
    //                    "CustomID",
    //                    Guid.NewGuid()
    //               }
    //           }
    //        });

    //        var policy = Policy
    //            .Handle<HttpRequestException>()
    //            .Or<Exception>()
    //            .RetryAsync(3, (exception, retryCount, context) =>
    //            {
    //                _loggerManager.LogException(nameof(SendMailAsync), exception, mailTemplate);
    //            });

    //        await policy.ExecuteAsync(async () =>
    //        {
    //            MailjetResponse response = await client.PostAsync(request);
    //            if (!response.IsSuccessStatusCode)
    //            {
    //                _loggerManager.LogException(nameof(SendMailAsync), new CustomException("error sending email."), new
    //                {
    //                    Response = response,
    //                    Request = mailTemplate
    //                });
    //            }
    //        });
    //    }
    //    #endregion
    //}
}


