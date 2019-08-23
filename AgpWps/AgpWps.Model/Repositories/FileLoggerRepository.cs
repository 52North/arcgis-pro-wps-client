using AgpWps.Model.Data;
using AgpWps.Model.Services;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Wps.Client.Models.Requests;
using Wps.Client.Services;
using Directory = System.IO.Directory;

namespace AgpWps.Model.Repositories
{
    public class FileLoggerRepository : ILoggerRepository
    {

        private readonly IAppData _appData;
        private string DirectoryPath => Path.Combine(_appData.DataDirectory, "ErrorReports");

        private readonly XmlSerializer _xmlSerializer;

        public FileLoggerRepository(IAppData appData)
        {
            _appData = appData ?? throw new ArgumentNullException(nameof(appData));

            _xmlSerializer = new XmlSerializer(typeof(ErrorReport));

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
        }

        public string LogExceptionReport(string jobId, ExecuteRequest request, Exception ex)
        {
            var serializer = new XmlSerializationService();
            var report = new ErrorReport
            {
                JobId = jobId,
                ExceptionMessage = ex.ToString(),
                ExecuteRequest = request
            };

            return Save(report);
        }

        private string Save(ErrorReport report)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    var filePath = Path.Combine(DirectoryPath, $"{report.JobId}.xml");

                    _xmlSerializer.Serialize(writer, report);
                    File.WriteAllBytes(filePath, stream.ToArray());

                    return filePath;
                }
            }
        }

    }
}
