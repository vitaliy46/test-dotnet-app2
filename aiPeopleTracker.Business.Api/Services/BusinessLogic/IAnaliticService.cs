using System;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.BusinessLogic
{
    /// <summary>
    /// Проксирует запросы во внешний сервис аналитики
    /// </summary>
    public interface IAnaliticService
    {
        /// <summary>
        /// Запрос на сервис аналитики распознования лиц на стопкадре
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="picture">Картинка со стопкадром</param>
        /// <returns></returns>
        RecognizedPersonsScope GetRecognizedPersons(int cameraId, byte[] picture);

        /// <summary>
        /// Запрашивается поиск видео фрагментов с присутствием персоны
        /// в зоне видимости любых камер для отрисовки мультитаймлайна 
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="seachTime">время стопкадра на котором выбрана персона</param>
        /// <returns></returns>
        IMultitimeline GetMultitimelineByPerson(int personId);

        /// <summary>
        /// Запращиваются видеофрагменты обнаруживающие движения в 
        /// зоне обозначенной углами квадрата на изображении
        /// </summary>
        /// <param name="leftBottomPoint">угол выделенного прямоуголника</param>
        /// <param name="rightTopPoint">угол выделенного прямоуголника</param>
        /// <param name="cameraId">камера на которой следует следить за выделенным объектом</param>
        /// <param name="picture">картинка на которой выделен прямоугольником объект</param>
        /// <returns></returns>
        IMultitimeline GetMultitimelineByObject(Point leftBottomPoint, Point rightTopPoint, int cameraId, byte[] picture);
    }
}
