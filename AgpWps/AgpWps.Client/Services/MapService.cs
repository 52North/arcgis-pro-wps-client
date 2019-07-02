using AgpWps.Model.Services;
using System.Collections.Generic;
using System.Linq;

namespace AgpWps.Client.Services
{
    public class MapService : IMapService
    {
        public void TriggerZoneSelection()
        {
            
        }

        public IReadOnlyList<string> GetMapLayers() => ArcGIS.Desktop.Mapping.MapView.Active.Map.Layers.Select(l => l.Name).ToList();
    }
}
