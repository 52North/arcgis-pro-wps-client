using System;

namespace AgpWps.Model.Geometry
{
    public class Rectangle
    {
        public Rectangle(Tuple<double, double> leftBottom, Tuple<double, double> rightTop)
        {
            LeftBottom = leftBottom;
            RightTop = rightTop;
        }

        public Tuple<double, double> LeftBottom { get; }
        public Tuple<double, double> RightTop { get; }

    }
}
