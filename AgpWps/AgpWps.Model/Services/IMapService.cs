using AgpWps.Model.Geometry;
using System;
using System.Threading.Tasks;

namespace AgpWps.Model.Services
{
    public interface IMapService
    {

        /// <summary>
        /// Enable the zone selection tool.
        /// </summary>
        /// <param name="selectionCallback">Callback called when the selection is complete</param>
        Task SelectZone(Action<Rectangle> selectionCallback);

    }
}
