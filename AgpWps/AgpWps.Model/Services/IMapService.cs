using System.Collections.Generic;

namespace AgpWps.Model.Services
{
    public interface IMapService
    {

        void TriggerZoneSelection();
        IReadOnlyList<string> GetMapLayers();

    }
}
