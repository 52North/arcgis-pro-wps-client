using AgpWps.Client.Tools;
using AgpWps.Model.Geometry;
using AgpWps.Model.Services;
using ArcGIS.Desktop.Framework;
using System;
using System.Threading.Tasks;

namespace AgpWps.Client.Services
{
    public class MapService : IMapService
    {
        public async Task SelectZone(Action<Rectangle> selectionCallback)
        {
            if (selectionCallback == null) throw new ArgumentNullException(nameof(selectionCallback));

            await FrameworkApplication.SetCurrentToolAsync("AgpWps_Client_Tools_SelectionTool");
            if (FrameworkApplication.ActiveTool is SelectionTool st)
            {
                st.SketchCompleted += (tool, geometry) =>
                {
                    selectionCallback.Invoke(new Rectangle((0, 0), (0, 0), (0, 0), (0, 0))); // WIP: Transform geometry into a rectangle
                };

                st.SketchCancelled += (_, __) => selectionCallback.Invoke(null);
            }
            else
            {
                selectionCallback.Invoke(null);
            }
        }
    }
}
