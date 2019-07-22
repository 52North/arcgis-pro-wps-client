using AgpWps.Client.Tools;
using AgpWps.Model.Geometry;
using AgpWps.Model.Services;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework;
using System;
using System.Threading.Tasks;

namespace AgpWps.Client.Services
{
    /// <summary>
    /// The ArcGIS Pro specific service used to manipulate the ArcGIS Pro tools.
    /// </summary>
    public class MapService : IMapService
    {

        private bool _isUsingSelectionTool;

        public async Task SelectZone(Action<Rectangle> selectionCallback)
        {
            if (selectionCallback == null) throw new ArgumentNullException(nameof(selectionCallback));

            if (_isUsingSelectionTool)
            {
                throw new InvalidOperationException("The zone selection tool is already in use.");
            }

            _isUsingSelectionTool = true;

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
                        _isUsingSelectionTool = false;
                    }
                };

                st.SketchCancelled += (_, __) =>
                {
                    _isUsingSelectionTool = false;
                    selectionCallback.Invoke(null);
                };
            }
            else
            {
                _isUsingSelectionTool = false;
                selectionCallback.Invoke(null);
            }
        }
    }
}
