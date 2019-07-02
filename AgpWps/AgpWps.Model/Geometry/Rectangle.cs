namespace AgpWps.Model.Geometry
{
    public class Rectangle
    {
        public Rectangle((double, double) leftBottom, (double, double) leftTop, (double, double) rightBottom, (double, double) rightTop)
        {
            LeftBottom = leftBottom;
            LeftTop = leftTop;
            RightBottom = rightBottom;
            RightTop = rightTop;
        }

        public (double, double) LeftBottom { get; }
        public (double, double) LeftTop { get; }
        public (double, double) RightBottom { get; }
        public (double, double) RightTop { get; }

    }
}
