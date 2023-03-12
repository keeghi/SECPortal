using Newtonsoft.Json.Linq;
using Lemondrop.Entities.Infrastructures;
using System;
using System.Collections.Generic;

namespace Lemondrop.Commons.Infrastructures
{
    public interface IMailingTemplate
    {
        List<ITransactionDetail> OrderDetails { get; set; }
        List<EmailDestination> EmailDestinations { get; set; }
        string Subject { get; set; }
        string ToMessageBodyHtml();
        string ToMessageBodyText();
        JArray ConstructDestinations();
    }

    public abstract class BaseMailingTemplate : IMailingTemplate
    {
        public List<EmailDestination> EmailDestinations { get; set; }
        public List<ITransactionDetail> OrderDetails { get; set; }
        public string Subject { get; set; }

        public BaseMailingTemplate()
        {
            EmailDestinations = new List<EmailDestination>();
            OrderDetails = new List<ITransactionDetail>();
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
