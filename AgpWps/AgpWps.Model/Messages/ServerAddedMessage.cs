using System;

namespace AgpWps.Model.Messages
{
    /// <summary>
    /// Message used by the event bus to inform all the listeners that a server url has been requested by the user.
    /// </summary>
    public class ServerAddedMessage
    {
        public ServerAddedMessage(string serverUrl)
        {
            ServerUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
        }

        public string ServerUrl { get; }

    }
}
