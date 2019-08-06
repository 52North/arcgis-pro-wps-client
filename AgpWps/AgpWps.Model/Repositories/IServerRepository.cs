namespace AgpWps.Model.Services
{
    public interface IServerRepository
    {

        void AddServer(string serverUrl);

        void RemoveServer(string serverUrl);

        string[] GetServersUrl();

    }
}
