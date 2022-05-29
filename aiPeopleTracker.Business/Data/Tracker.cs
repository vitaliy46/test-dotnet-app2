using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Data
{

    /// <summary>
    /// Программный элемент ползунка, на положение которого должны реагировать видеоклипы 
    /// и визуальный элемент на мультитаймлайне.
    /// В свою очередь, визуальный элемент может воздействовать на положение программного
    /// ползунка.
    /// </summary>
    public class Tracker : NotifyPropertyChanged, ITracker
    {

        /// <summary>
        /// Событие возникает при смене положения ползунка.
        /// На него подписываются VideoClip объекты из состава
        /// Multiline, чтобы в свою очередь генерировать события начала
        /// и окончания своего воспроизведения
        /// </summary>
        public event PositionChangeHandler PositionChange;

        /// <summary>
        /// Положение ползунка на временной оси 
        /// </summary>
        private DateTime _position;
        public DateTime Position
        {
            get { return _position; }
            set { SetField(ref _position, value); }
        }

        // изменяем свойство Position когда двигаем ползунок вручную
        public void TimeLinePositionChangedHandler(DateTime p)
        {
            Position = p;
            PositionChange?.Invoke(_position);
        }

        private TrackerState _state;
        public TrackerState State
        {
            get { return _state; }
            set { SetField(ref _state, value); }
        }

        /// <summary>
        /// Шаг с которым ползунок перемещается по временной шкале.
        /// Варируется в зависимость от требований к плавности прохождения
        /// ползунка по таймлайну и масштабного коэф-та отображения
        /// таймлайна в окне просмотра. Чем меньше масштаб (крупней элементы),
        /// тем меньше должен быть временной шаг
        /// </summary>
        private double _playSpeed;
        public double PlaySpeed
        {
            get { return _playSpeed; }
            set { SetField(ref _playSpeed, value); }
        }

        /// <summary>
        /// Шаг перемещения ползунка по временной шакле
        /// </summary>
        private TimeSpan _step;

        private IMultitimeline _multitimeline;

        public Tracker(IMultitimeline multitimeline)
        {
            _multitimeline = multitimeline;

            _playSpeed = 100;

            _step = TimeSpan.FromMilliseconds(_playSpeed);
        }


        /// <summary>
        /// Запускается процесс непрерывного перемещения ползунка по видеофрагментам
        /// При этом постоянно корректируется состав  PlayableVideoClips при пересечении
        /// ползунком видеоклипов.
        /// Этот метод должен вызываться в асинхронном режиме на UI
        /// </summary>
        /// <param name="_position"></param>
        public void Play(object sender, EventArgs e)
        {
            if (_position > _multitimeline.LeftEdge + _multitimeline.Duration)
            {
                Stop();
            }
            else if (!_multitimeline.PlayableVideoClips.Any())
            {
                _position = MoveToNextClip(_position);
            }
            else
            {
                _position += _step;
            }

            PositionChange?.Invoke(_position);
        }
    
        /// <summary>
        /// Перемещение ползунка на новую позицию влечет изменение в составе
        /// активно воспроизводимых клипов
        /// </summary>
        /// <param name="timePosition"></param>
        public void MoveTo(DateTime position)
        {
            _position = position;

            PositionChange(_position);
        }

        public void Stop()
        {
            State = TrackerState.Stopped;
            Debug.WriteLine("Ползунок остановился");
        }

       
        /// <summary>
        /// В случае обнаружения разрыва между клипами, ползунок должен переместиться 
        /// к следующему ближайшему клипу
        /// </summary>
        /// <returns></returns>
        private DateTime MoveToNextClip(DateTime currentPosition)
        {
            var nextClip = _multitimeline.VideoClips.FirstOrDefault(x => x.BeginTime > currentPosition);

            return  nextClip.BeginTime;
        }
    }
}
