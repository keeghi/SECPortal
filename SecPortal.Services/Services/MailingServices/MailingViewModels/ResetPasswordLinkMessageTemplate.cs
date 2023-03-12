using SecPortal.Commons.Helpers;
using SecPortal.Commons.Infrastructures;
using System.Net;
using System.Text;

namespace SecPortal.Services.Services.MailingServices.MailingViewModels
{
    public class ResetPasswordLinkMessageTemplate : BaseMailingTemplate
    {
        public ResetPasswordLinkMessageTemplate(string resetToken, string webRoot, string domain)
        {
            Subject = "Password Reset for SecPortal Website";
            _resetToken = resetToken;
            _domain = domain;
            _webroot = webRoot;
        }

        private string _domain;
        private string _resetToken;
        private string _webroot;

        public override string ToMessageBodyHtml()
        {
            var htmlPart = System.IO.File.ReadAllText($@"{_webroot}\templates\password-reset.html");
            htmlPart = htmlPart.Replace("##@@URL@@##", $"{_domain}/reset-password?token={WebUtility.UrlEncode(_resetToken)}");
            htmlPart = htmlPart.Replace("##@@CURRENT_YEAR@@##", DateHelper.Now.ToLocalTime().Year.ToString());
            htmlPart = htmlPart.Replace("##@@SUBJECT@@##", Subject);
            return htmlPart;
        }

        public override string ToMessageBodyText()
        {
            var message = new StringBuilder();
            message.Append($"Please visit this link to reset your password {_domain}/reset-password?token={_resetToken}");
            return message.ToString();
        }
    }
}
