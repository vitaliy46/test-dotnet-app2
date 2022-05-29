using System;

namespace aiPeopleTracker.Wpf.Api
{
    /// <summary>
    /// Содержит сведения об окне просмотра и содержимом мультитаймлайна
    /// параметры которых меняются при масштабировании
    /// </summary>
    public class ScaleScope
    {
        /// <summary>
        /// Текущее положение ползунка, являющееся центром масштабирования
        /// </summary>
        public DateTime TrackerPosition { get; set; }

        /// <summary>
        /// Левая граница окна просмотра таймлайн
        /// </summary>
        public DateTime LeftDate { get; set; }

        /// <summary>
        /// Правая граница окна просмотра таймлайна
        /// </summary>
        public DateTime RightDate { get; set; }

    }
}
