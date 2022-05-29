using System;
using System.Windows.Controls;

namespace aiPeopleTracker.Wpf.Api
{
    /// <summary>
    /// Управление масштабированием и нанесением масштабной шкалы 
    /// для просмотра таймлайна через элемент управления окна просмотра
    /// </summary>
    public interface IScaleComponent
    {
        /// <summary>
        /// Пересчитывает все позиции визуальных элементов в окне просмотра таймлайна
        /// </summary>
        /// <param name="scaleScope"></param>
        /// <returns></returns>
        ScaleScope ZoomOut(ScaleScope scaleScope);

        /// <summary>
        /// Прорисовывает шкалу на окне просмотра таймлайна
        /// </summary>
        void DrawScale(IMultitimelineWindowControl multitimelineControl, Canvas canvas);
    }

   
}
