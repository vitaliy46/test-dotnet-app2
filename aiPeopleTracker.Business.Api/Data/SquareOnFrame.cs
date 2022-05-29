using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Api.Data
{
    /// <summary>
    /// Рамка на стопкадре
    /// </summary>
    public class SquareOnFrame : NotifyPropertyChanged
    {
        /// <summary>
        /// Координата верхнего левого угла
        /// </summary>
        private int _x1;
        public int X1
        {
            get { return _x1; }
            set { SetField(ref _x1, value); }
        }

        /// <summary>
        /// Координата верхнего левого угла
        /// </summary>
        private int _y1;
        public int Y1
        {
            get { return _y1; }
            set { SetField(ref _y1, value); }
        }
        /// <summary>
        /// Координата правого нижнего угла
        /// </summary>
        private int _x2;
        public int X2
        {
            get { return _x2; }
            set { SetField(ref _x2, value); }
        }

        /// <summary>
        /// Координата правого нижнего угла
        /// </summary>
        private int _y2;
        public int Y2
        {
            get { return _y2; }
            set { SetField(ref _y2, value); }
        }
    }
}