using System;

namespace AgpWps.Model.Messages
{
    public class ServerRemovedMessage
    {
        public string ServerUrl { get; }

        public ServerRemovedMessage(string serverUrl)
        {
            ServerUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
        }

    }
}
