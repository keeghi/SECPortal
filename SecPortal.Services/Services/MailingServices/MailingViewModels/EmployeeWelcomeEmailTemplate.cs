using SecPortal.Commons.Helpers;
using SecPortal.Commons.Infrastructures;
using System;
using System.Net;
using System.Text;

namespace SecPortal.Services.Services.MailingServices.MailingViewModels
{
    public class EmployeeWelcomeEmailTemplate : BaseMailingTemplate
    {
        public EmployeeWelcomeEmailTemplate(Guid id, string activateToken, string webroot, string domain)
        {
            Subject = "SecPortal - Activate your Account";
            _webroot = webroot;
            _id = id;
            _activateToken = activateToken;
            _domain = domain;
        }

        private string _webroot;
        private string _domain;
        private readonly Guid _id;
        private string _activateToken;

        public override string ToMessageBodyHtml()
        {
            var htmlPart = System.IO.File.ReadAllText($@"{_webroot}\templates\activate-account.html");
            htmlPart = htmlPart.Replace("##@@URL@@##", $"{_domain}/activate-account?token={WebUtility.UrlEncode(_activateToken)}&id={_id}");
            htmlPart = htmlPart.Replace("##@@CURRENT_YEAR@@##", DateHelper.Now.ToLocalTime().Year.ToString());
            htmlPart = htmlPart.Replace("##@@SUBJECT@@##", Subject);
            return htmlPart;
        }

        public override string ToMessageBodyText()
        {
            var message = new StringBuilder();
            message.Append($"Please visit this link to activate account {_domain}/activate-account?token={WebUtility.UrlEncode(_activateToken)}&id={_id}");
            return message.ToString();
        }
    }
}
