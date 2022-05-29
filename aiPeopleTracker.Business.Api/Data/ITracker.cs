using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace aiPeopleTracker.Business.Api.Data
{
    /// <summary>
    /// Тип события позникающего при программном перемещении ползунка
    /// </summary>
    /// <param name="position"></param>
    public delegate void PositionChangeHandler(DateTime position);

    /// <summary>
    /// Программный элемент ползунка, на положение которого должны реагировать видеоклипы 
    /// и визуальный элемент на мультитаймлайне.
    /// В свою очередь, визуальный элемент может воздействовать на положение программного
    /// ползунка.
    /// </summary>
    public interface ITracker : INotifyPropertyChanged
    {
        /// <summary>
        /// Положение движка на временной шкале
        /// </summary>
        DateTime Position { get; set; }

        // изменяем свойство Position когда двигаем ползунок вручную
        void TimeLinePositionChangedHandler(DateTime p);

        /// <summary>
        /// Событие возникает при смене положения ползунка.
        /// На него подписываются VideoClip объекты из состава
        /// Multiline, чтобы в свою очередь генерировать события начала
        /// и окончания своего воспроизведения
        /// </summary>
        event PositionChangeHandler PositionChange;

        /// <summary>
        /// Коэф-т скорости воспроизведения
        /// </summary>
        double PlaySpeed { get; set; }

        /// <summary>
        /// Запускается процесс непрерывного перемещения ползунка по видеофрагментам
        /// При этом постоянно корректируется состав  PlayableVideoClips при пересечении
        /// ползунком видеоклипов. На изменение состава PlayableVideoClips подписаны
        /// управляющие элементы и реагируют изменением вывода видеопотоков.
        /// Этот метод должен вызываться в асинхронном режиме на UI
        /// </summary>
        /// <param name="_position">указание с какой позиции ползунка на таймлайне
        /// нежно воспроизводить видеофрагменты </param>

        void Play(object sender, EventArgs e);

        /// <summary>
        /// Перемещение движка на новую позицию влечет изменение в составе
        /// активно воспроизводимых клипов
        /// </summary>
        /// <param name="position">передается с фронта положение
        /// в которое пользователь переместил ползунок</param>
        void MoveTo(DateTime position);

        /// <summary>
        /// Дирректива остановки проигрывания
        /// Возвращает позицию остановки ползунка
        /// </summary>
        void Stop();
    }
}