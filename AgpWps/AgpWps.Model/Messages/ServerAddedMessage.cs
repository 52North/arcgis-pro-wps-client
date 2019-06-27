using System;

namespace AgpWps.Model.Messages
{
    public class ServerAddedMessage
    {
        public ServerAddedMessage(string serverUrl)
        {
            ServerUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
        }

        public string ServerUrl { get; }

    }
}
