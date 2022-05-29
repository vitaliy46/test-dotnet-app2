using System;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Data
{
    /// <summary>
    /// Видеофрагмент с камеры
    /// </summary>
    public class VideoClip : NotifyPropertyChanged, IVideoClip
    {
        private Camera _camera;
        public Camera Camera
        {
            get { return _camera; }
            set { SetField(ref _camera, value); }
        }
        
        private DateTime _beginTime;
        public DateTime BeginTime
        {
            get { return _beginTime; }
            set { SetField(ref _beginTime, value); }
        }

        public TimeSpan Duration { get; internal set; }

        /// <summary>Окончание видеофрагмента</summary>
        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetField(ref _endTime, value); }
        }
    }
}