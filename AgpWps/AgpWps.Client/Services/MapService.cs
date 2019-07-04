using AgpWps.Client.Tools;
using AgpWps.Model.Geometry;
using AgpWps.Model.Services;
using ArcGIS.Core.Geometry;
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
                    if (geometry is Polygon p)
                    {
                        var twoDimensionalPoints = p.Points.Copy2DCoordinatesToList();
                        if (!(twoDimensionalPoints.Count >= 4)) // TODO: Correctly check whether the polygon is a rectangle. Currently the array has 5 points but should have 4.
                            throw new InvalidOperationException("The shape is not a rectangle");

                        var leftBottomPoint = new Tuple<double, double>(twoDimensionalPoints[0].X, twoDimensionalPoints[0].Y);
                        var rightTopPoint = new Tuple<double, double>(twoDimensionalPoints[2].X, twoDimensionalPoints[2].Y);
                        selectionCallback.Invoke(new Rectangle(leftBottomPoint, rightTopPoint));
                    }
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
