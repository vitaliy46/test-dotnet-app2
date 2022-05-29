using System;
using System.ComponentModel;
using System.Diagnostics;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Api.Data
{
    /// <summary>
    /// Комбинированный таймлайн с с информацией о  видеофрагментах
    /// в которых обнаружена персона.
    /// При добавлении элемента видеопотока в коллекцию, происходит подпись
    /// его на событие изменения положения движка. Видеофрагменты отслеживают это событие и 
    /// при достижении значений начала или конца начинают вывод видеопотока и прекращают
    ///  его соответственно
    /// </summary>
    public interface IMultitimeline : INotifyPropertyChanged
    {
        /// <summary>
        /// Служебный идентификатор, позволяющий отслеживать сколько остается в памяти 
        /// неимпользуемых таймлайнов. Нужно своевременно отписываться от всех подписок
        /// чтобы грабич коллектор смог освободить память от неиспользуемых объектов этого типа
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// Полный состав видеоклипов предоставленный по запросу для воспроизведения
        /// в хронологическом порядке
        /// </summary>
        SortableObservableCollection<IVideoClip> VideoClips { get; set; }
        /// <summary>
        /// Видеоклипы, пересекающие положения движка и находящиеся в состоянии
        /// воспроизведения с того момента времени в озиции которой находится движок.
        /// Эта коллекция динамична и состав ее постоянно меняется по мере перемещения движка. 
        /// </summary>
        SortableObservableCollection<IVideoClip> PlayableVideoClips { get; set; }

        /// <summary>
        /// Левая временная граница таймлайна. 
        /// </summary>
        DateTime LeftEdge { get; }

        /// <summary>
        /// Правая временная граница таймлайна.
        /// </summary>
        DateTime RightEdge { get; }

        /// <summary>
        /// Величина таймлайна по продолжительности
        /// </summary>
        TimeSpan Duration { get; }
        
        /// <summary>
        /// Движек, управляющий выводом видеопотоков на экране
        /// </summary>
        ITracker Tracker { get; }
    }
}