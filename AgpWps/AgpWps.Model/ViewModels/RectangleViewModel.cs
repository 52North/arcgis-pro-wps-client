using GalaSoft.MvvmLight;
using System;

namespace AgpWps.Model.ViewModels
{
    public class RectangleViewModel : ViewModelBase
    {

        private Tuple<double, double> _leftBottom;
        public Tuple<double, double> LeftBottom
        {
            get => _leftBottom;
            set => Set(ref _leftBottom, value);
        }

        private Tuple<double, double> _rightTop;
        public Tuple<double, double> RightTop
        {
            get => _rightTop;
            set => Set(ref _rightTop, value);
        }

        public RectangleViewModel(Tuple<double, double> leftBottom, Tuple<double, double> rightTop)
        {
            _leftBottom = leftBottom;
            _rightTop = rightTop;
        }

    }
}
