using SecPortal.Commons.Infrastructures;
using System.Text;

namespace SecPortal.Services.Services.MailingServices.MailingViewModels
{
    public class StockWarningEmailTemplate : BaseMailingTemplate
    {
        public StockWarningEmailTemplate(string products, int minimalStock, string webroot)
        {
            Subject = "SecPortal - Product Stock Alert";
            _products = products;
            _minimalStock = minimalStock;
            _webroot = webroot;
        }

        private readonly string _products;
        private readonly int _minimalStock;
        private string _webroot;

        public override string ToMessageBodyHtml()
        {
            var htmlPart = System.IO.File.ReadAllText($@"{_webroot}\templates\stock-warning-template.html");
            htmlPart = htmlPart.Replace("##@@PRODUCT_NAME@@##", _products);
            htmlPart = htmlPart.Replace("##@@MINIMAL_QTY@@##", _minimalStock.ToString("N0"));
            htmlPart = htmlPart.Replace("##@@SUBJECT@@##", Subject);
            return htmlPart;
        }

        public override string ToMessageBodyText()
        {
            var message = new StringBuilder();
            message.Append($"SecPortal stock alert, these product have stock with less than {_minimalStock} : {_products}");
            return message.ToString();
        }
    }
}
