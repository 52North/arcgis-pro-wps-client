namespace AgpWps.Model.Repositories
{
    public interface IServerRepository
    {

        void AddServer(string serverUrl);

        void RemoveServer(string serverUrl);

        void RemoveAll();

        string[] GetServersUrl();

    }
}
