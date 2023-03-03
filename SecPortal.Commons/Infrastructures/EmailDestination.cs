namespace SecPortal.Commons.Infrastructures
{
    public class EmailDestination
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public EmailDestination()
        {

        }

        public EmailDestination(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
