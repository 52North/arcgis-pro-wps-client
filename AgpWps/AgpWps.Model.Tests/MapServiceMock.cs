using AgpWps.Model.Geometry;
using AgpWps.Model.Services;
using System;
using System.Threading.Tasks;

namespace AgpWps.Model.Tests
{
    public class MapServiceMock : IMapService
    {
        public Task SelectZone(Action<Rectangle> selectionCallback)
        {
            return Task.CompletedTask;
        }
    }
}
