using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AgpWps.Model.Repositories
{
    public class ResultsRepository : IResultRepository
    {
        private readonly IAppData _appData;
        private string FilePath => Path.Combine(_appData.DataDirectory, "results.xml");

        private readonly XmlSerializer _xmlSerializer;
        private readonly List<ResultViewModel> _results = new List<ResultViewModel>();

        public ResultsRepository(IAppData appData)
        {
            _appData = appData ?? throw new ArgumentNullException(nameof(appData));

            _xmlSerializer = new XmlSerializer(typeof(ResultViewModel[]));

            UpdateCache();
        }

        public void AddResult(ResultViewModel result)
        {
            if (!_results.Contains(result))
            {
                _results.Add(result);
                Save();
            }
        }

        public void RemoveResult(ResultViewModel result)
        {
            if (_results.Contains(result))
            {
                _results.Remove(result);
                Save();
            }
        }

        public ResultViewModel[] GetResults()
        {
            return _results.ToArray();
        }

        private void UpdateCache()
        {
            try
            {
                using (var io = new FileStream(FilePath, FileMode.OpenOrCreate))
                {
                    var servers = (ResultViewModel[])_xmlSerializer.Deserialize(io);

                    _results.Clear();
                    _results.AddRange(servers ?? new ResultViewModel[0]);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Save()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    _xmlSerializer.Serialize(writer, _results.ToArray());
                    File.WriteAllBytes(FilePath, stream.ToArray());
                }
            }
        }
    }
}
