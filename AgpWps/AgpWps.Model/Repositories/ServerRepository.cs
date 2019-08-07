using AgpWps.Model.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AgpWps.Model.Repositories
{
    public class ServerRepository : IServerRepository
    {

        private readonly IAppData _appData;
        private string FilePath => Path.Combine(_appData.DataDirectory, "servers.xml");

        private readonly XmlSerializer _xmlSerializer;
        private readonly List<string> _serverUrls = new List<string>();

        public ServerRepository(IAppData appData)
        {
            _appData = appData ?? throw new ArgumentNullException(nameof(appData));

            _xmlSerializer = new XmlSerializer(typeof(string[]));

            UpdateCache();
        }

        public void AddServer(string serverUrl)
        {
            var formattedUrl = serverUrl.Replace(" ", "").ToLower();
            if (!_serverUrls.Contains(formattedUrl))
            {
                _serverUrls.Add(formattedUrl);
                Save();
            }
        }

        public void RemoveServer(string serverUrl)
        {
            if (_serverUrls.Contains(serverUrl))
            {
                _serverUrls.Remove(serverUrl);
                Save();
            }
        }

        public string[] GetServersUrl()
        {
            return _serverUrls.ToArray();
        }

        private void UpdateCache()
        {
            try
            {
                using (var io = new FileStream(FilePath, FileMode.OpenOrCreate))
                {
                    var servers = (string[])_xmlSerializer.Deserialize(io);

                    _serverUrls.Clear();
                    _serverUrls.AddRange(servers ?? new string[0]);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Save()
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        _xmlSerializer.Serialize(writer, _serverUrls.ToArray());
                        File.WriteAllBytes(FilePath, stream.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
