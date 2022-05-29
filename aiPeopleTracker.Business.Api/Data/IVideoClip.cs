using System;
using System.ComponentModel;
using System.Dynamic;
using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Data
{
    /// <summary>
    /// Видеофрагмент с камеры
    /// </summary>
    public interface IVideoClip : INotifyPropertyChanged
    {
        Camera Camera { get;  }
        /// <summary>Начало видеофрагмента относительно
        /// всего записанного видео</summary>
        DateTime BeginTime { get;  }

        /// <summary>
        /// Длинна видеофрагмента в приведенных ед. имерений
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>Окгнчание видеофрагмента относительно
        /// всего записанного видео</summary>
        DateTime EndTime { get; }
    }
}