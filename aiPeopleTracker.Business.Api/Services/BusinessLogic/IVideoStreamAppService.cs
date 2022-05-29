using System;
using System.IO;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.BusinessLogic
{
    /// <summary>
    /// Сервис для запроса видеопотоков с СХД сервера
    /// </summary>
    public interface IVideoStreamAppService
    {
        /// <summary>
        /// Запрашивается видео поток для указанной камеры с указанной даты и времени
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        Stream GetVideoStream(Camera camera, DateTime startTime);
    }
}