namespace SecPortal.Commons.Infrastructures
{
    public interface ISMTPCredential
    {
        string WebRoot { get; set; }
        string FromEmail { get; set; }
        string FromName { get; set; }
        string Domain { get; set; }
        string Secret { get; set; }
        string Key { get; set; }
    }

    public class MailjetCredential : ISMTPCredential
    {
        public string WebRoot { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Domain { get; set; }
    }

    public class SendGridCredential : ISMTPCredential
    {
        public string Key { get; set; }
        public string WebRoot { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Domain { get; set; }
        public string Secret { get; set; }
    }
}
