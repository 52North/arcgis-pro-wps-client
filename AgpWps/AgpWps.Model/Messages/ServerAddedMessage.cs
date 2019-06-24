namespace AgpWps.Model.Messages
{
    public class ServerAddedMessage
    {
        public ServerAddedMessage(string serverUrl)
        {
            ServerUrl = serverUrl;
        }

        public string ServerUrl { get; }

    }
}
