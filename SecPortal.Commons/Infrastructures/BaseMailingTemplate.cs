using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SecPortal.Commons.Infrastructures
{
    public interface IMailingTemplate
    {
        List<EmailDestination> EmailDestinations { get; set; }
        string Subject { get; set; }
        string ToMessageBodyHtml();
        string ToMessageBodyText();
        JArray ConstructDestinations();
    }

    public abstract class BaseMailingTemplate : IMailingTemplate
    {
        public List<EmailDestination> EmailDestinations { get; set; }
        public string Subject { get; set; }

        public BaseMailingTemplate()
        {
            EmailDestinations = new List<EmailDestination>();
        }

        public virtual string ToMessageBodyHtml()
        {
            throw new NotImplementedException();
        }

        public virtual string ToMessageBodyText()
        {
            throw new NotImplementedException();
        }

        public void AddDestination(string name, string email)
        {
            EmailDestinations.Add(new EmailDestination(name, email));
        }

        public JArray ConstructDestinations()
        {
            var emailDestinations = new JArray();
            foreach (var item in EmailDestinations)
            {
                emailDestinations.Add(
                new JObject
                {
                   {
                       "Email",
                       item.Email
                   },
                   {
                       "Name",
                       item.Name
                   }
                });
            }

            return emailDestinations;
        }
    }
}
