using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Data
{
  /// <summary>
    /// Комбинированный таймлайн с с информацией о  видеофрагментах
    /// в которых обнаружена персона.
    /// При добавлении элемента видеопотока в коллекцию, происходит подпись
    /// его на событие изменения положения ползунка. Видеофрагменты отслеживают это событие и 
    /// при достижении значений начала или конца начинают вывод видеопотока и прекращают
    ///  его соответственно
    /// </summary>
    public class Multitimeline : NotifyPropertyChanged, IMultitimeline
    {
        public Guid Id { get; }

        /// <summary>
        /// Полный состав видеоклипов предоставленный по запросу для воспроизведения
        /// в хронологическом порядке
        /// </summary>
        private SortableObservableCollection<IVideoClip> _videoClips;
        public SortableObservableCollection<IVideoClip> VideoClips
        {
            get { return _videoClips; }
            set { SetField(ref _videoClips, value); }
        }

        /// <summary>
        /// Видеоклипы, пересекающие положения ползунка и находящиеся в состоянии
        /// воспроизведения с того момента времени в озиции которой находится ползунок.
        /// Эта коллекция динамична и состав ее постоянно меняется по мере перемещения ползунка. 
        /// </summary>
        private SortableObservableCollection<IVideoClip> _playableVideoClips;
        public SortableObservableCollection<IVideoClip> PlayableVideoClips
        {
            get { return _playableVideoClips; }
            set { SetField(ref _playableVideoClips, value); }
        }

        /// <summary>
        /// Движек, управляющий выводом видеопотоков на экране
        /// </summary>
        private ITracker _tracker;


        public ITracker Tracker
        {
            get { return _tracker; }
            set { SetField(ref _tracker, value); }
        }

        /// <summary>
        /// Левая временная граница таймлайна. Изменяется при масштабировании
        /// или при приближении ползунка к правой границе
        /// </summary>
        private DateTime _leftEdge;
        public DateTime LeftEdge
        {
            get { return _leftEdge; }
            set { SetField(ref _leftEdge, value); }
        }

        /// <summary>
        /// Правая граница изменяется при тех же условиях, что и левая
        /// </summary>
        private DateTime _rightEdge;
        public DateTime RightEdge
        {
            get { return _rightEdge; }
            set { SetField(ref _rightEdge, value); }

        }

        public TimeSpan Duration
        {
            get { return _rightEdge - _leftEdge; }
        }

        /// <summary>
        /// Минимально допустимая величина приближения ползунка к краю в условыых
        /// единицах  после чего начинается перемешение таймлайна относительно экрана
        /// </summary>
        private double _edgeNearPosition;

        /// <summary>
        /// Шаг смещения таймлайна по горизонтали в условых единицах
        /// </summary>
        private long _stepTimelineMove;

        /// <param name="edgeNearPosition">Минимально допустимая величина приближения ползунка к краю</param>
        /// <param name="stepTimelineMoveInSecond">время шага смещения таймлайна в секундах</param>
        public Multitimeline(ulong edgeNearPosition = 1000, uint stepTimelineMoveInSecond = 500)
        {
            Id = Guid.NewGuid();
            _edgeNearPosition = edgeNearPosition;
            _stepTimelineMove = stepTimelineMoveInSecond;
        }

        /// <summary>
        /// Изменение масштаба таймлайна
        /// TODO требует уточнения и доработки
        /// </summary>
        public void ChangeScale(TimeLineScales scaleValue)
        {
            ////Вычисляется положение середины таймлайна в тиках
            //var midleTime = TimeSpan.FromMilliseconds((_rightEdge - _leftEdge).TotalMilliseconds/2);
            ////Прирощение может быть как положительным так и отрицательным в зависимости от
            ////знака масштабирующего коэф-та
            //var deltaTime = midleTime * (int)scaleValue;
            ////Находится приращение временной правой границы от масштабирования и добавляется
            //_rightEdge = deltaTime>0?
            //    _rightEdge - midleTime
            //  : _rightEdge + midleTime;

            //SetField(ref _rightEdge, _rightEdge);

            ////Находится приращение временной левой границы от масштабирования и добавляется
            //_leftEdge = deltaTime > 0 ?
            //    _leftEdge + midleTime
            //    : _leftEdge - midleTime;

            //SetField(ref _leftEdge, _leftEdge);
        }


        /// <summary>
        /// Обработчик события перемещения ползунка
        /// Комплектует список проигрываемых клипов после анализа положения
        /// ползунка
        /// </summary>
        /// <param name="position"></param>
        internal void ComplectPlayableVideoClips_Handler(DateTime position)
        {

            var activeClips = VideoClips.Where(x => position >= x.BeginTime
                                                 && position <= x.EndTime).ToList();
           
                var playableVideoClips = PlayableVideoClips.ToList();

                var clipsForRemove = playableVideoClips.Except(activeClips).ToList();

                var clipsForAdd = activeClips.Except(playableVideoClips).ToList();

            if (clipsForRemove.Any())
            {
                clipsForRemove.ForEach(clip => PlayableVideoClips.Remove(clip));
            }
            if (clipsForAdd.Any())
            {
                clipsForAdd.ForEach(clip => PlayableVideoClips.Add(clip));
            }

        }

       
    }
}