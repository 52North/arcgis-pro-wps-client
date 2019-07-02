using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping;
using System;
using System.Threading.Tasks;

namespace AgpWps.Client.Tools
{
    internal class SelectionTool : MapTool
    {

        public EventHandler<Geometry> SketchCompleted;
        public EventHandler SketchCancelled;

        public SelectionTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Rectangle;
            SketchOutputMode = SketchOutputMode.Map;
        }

        protected override Task<bool> OnSketchCancelledAsync()
        {
            SketchCancelled?.Invoke(this, null);
            return base.OnSketchCancelledAsync();
        }

        protected override Task OnToolDeactivateAsync(bool hasMapViewChanged)
        {
            SketchCancelled?.Invoke(this, null);
            return base.OnToolDeactivateAsync(hasMapViewChanged);
        }

        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            SketchCompleted?.Invoke(this, geometry);
            return base.OnSketchCompleteAsync(geometry);
        }

    }
}
