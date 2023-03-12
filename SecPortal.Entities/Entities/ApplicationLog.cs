using SecPortal.Entities.Infrastructures;

namespace SecPortal.Entities.Entities
{
    public class ApplicationLog : BaseEntities
    {
        public string Caller { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public string Parameter { get; set; }

        public ApplicationLog()
        {

        }

        public ApplicationLog(string caller, string exception, string stackTrace, object parameter)
        {
            this.Caller = caller;
            this.Exception = exception;
            this.StackTrace = stackTrace;
            this.Parameter = System.Text.Json.JsonSerializer.Serialize(parameter);
        }
    }
}
