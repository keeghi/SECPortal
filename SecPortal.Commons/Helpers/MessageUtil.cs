namespace SecPortal.Common.Helpers
{
    public static class MessageUtil
    {
        public const string ContactAdminMessage = "Action Failed, Please Contact Administrator for more information";

        public static string EntityUsed(string entityName)
        {
            return $"{entityName} has a transaction in the database, please de-active account instead of deleting";
        }
    }
}
