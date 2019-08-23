using Wps.Client.Models.Requests;

namespace AgpWps.Model.Data
{
    public class ErrorReport
    {

        public string JobId { get; set; }

        public ExecuteRequest ExecuteRequest { get; set; }

        public string ExceptionMessage { get; set; }

    }
}
