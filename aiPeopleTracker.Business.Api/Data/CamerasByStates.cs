using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Api.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class CamerasByStates : NotifyPropertyChanged
    {
        private int _count;
        /// <summary>Общее число камер</summary>
        public int Count
        {
            get { return _count; }
            set { SetField(ref _count, value); }
        }

        private int _countActive;
        /// <summary>Число активных камер</summary>
        public int CountActive
        {
            get { return _countActive; }
            set { SetField(ref _countActive, value); }
        }

        private int _countInActive;
        /// <summary>Число неактивных камер</summary>
        public int CountInActive
        {
            get { return _countInActive; }
            set { SetField(ref _countInActive, value); }
        }
    }
}