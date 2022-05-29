using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Business.Data;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Services.BusinessLogic
{
    /// <summary>
    /// Строит и конфигурирует сложно-составной экземпляр мультитаймлайна.
    /// Работает по принцыпу синглтона
    /// </summary>
    public interface IMultitimelineBuilder
    {
        /// <summary>
        /// Основной метод построения мультитаймлайна.
        /// В системе не должно существовать более одного мультитаймлайна
        /// </summary>
        /// <param name="videoClips"></param>
        /// <returns></returns>
        IMultitimeline Build(IList<IVideoClip> videoClips);
    }

    public class MultitimelineBuilder : IMultitimelineBuilder
    {
        private static Multitimeline _multitimeline;

        public IMultitimeline Build(IList<IVideoClip> videoClips)
        {
            if (_multitimeline == null)
            {
                _multitimeline = new Multitimeline ();

                _multitimeline.Tracker = new Tracker(_multitimeline);

                ///Для корректировки положения таймлайна на форме
                _multitimeline.Tracker.PositionChange += _multitimeline.ComplectPlayableVideoClips_Handler;
            }

            _multitimeline.PlayableVideoClips = new SortableObservableCollection<IVideoClip>();

            _multitimeline.VideoClips = new SortableObservableCollection<IVideoClip>();

            ConfigureVideoClips(_multitimeline, videoClips);

            _multitimeline.LeftEdge = _multitimeline.VideoClips.First().BeginTime;

            var lastClip = _multitimeline.VideoClips.Last();

            _multitimeline.RightEdge = lastClip.BeginTime + lastClip.Duration;

            //Один шаг отступается для того, чтобы произошел скачек на первый клип
            //и это правильно заставит отработать сетку для вывода первого  видео клипа
            _multitimeline.Tracker.MoveTo(_multitimeline.LeftEdge - new TimeSpan(0,0,0,5));

            return _multitimeline;
        }

        /// <summary>
        /// Настройка видеоклипов с позиционированию их к положению 
        /// на таймлайне в условных единицах измерений
        /// </summary>
        /// <param name="_multitimeline"></param>
        /// <param name="videoClips"></param>
        private void ConfigureVideoClips(Multitimeline _multitimeline, IList<IVideoClip> videoClips)
        {
            foreach (var v in videoClips)
            {
                var videoClip = v as VideoClip;

                //Относительные единицы измерений таймлайна приняты в размерности
                //милисекунд. Так проще будет преходить от абсолютных значений времени
                //к относительным единицам таймлайна
                videoClip.Duration = videoClip.EndTime - videoClip.BeginTime;

                _multitimeline.VideoClips.Add(videoClip);
            }

            _multitimeline.VideoClips.Sort(x => x.BeginTime);

        }
    }
}
