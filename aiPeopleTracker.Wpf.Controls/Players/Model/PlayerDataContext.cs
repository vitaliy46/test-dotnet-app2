
using System;
using System.Windows.Media;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Wpf.Controls.Players.Model
{
    public class PlayerDataContext : NotifyPropertyChanged
    {
        public int LayoutTemplateId { get; }
        public int CameraId { get; }        

        private Stretch _stretch;

        public Stretch Stretch
        {
            get { return _stretch; }
            set { SetField(ref _stretch, value); }
        }

        private PlayerState _state;

        public PlayerState State
        {
            get { return _state; }
            set { SetField(ref _state, value); }
        }

        private Uri _source;
        public Uri Source
        {
            get { return _source; }
            set { SetField(ref _source, value); }
        }

        private TimeSpan _position;
        public TimeSpan Position
        {
            get { return _position; }
            set { SetField(ref _position, value); }
        }

        public PlayerDataContext(int layoutTemplateId, int cameraId)
        {
            LayoutTemplateId = layoutTemplateId;
            CameraId = cameraId;
        }
    }
}