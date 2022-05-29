using System;
using System.IO;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;

namespace aiPeopleTracker.Business.Services.BusinessLogic
{
    /// <summary>
    /// Сервис для запроса видеопотоков с СХД сервера
    /// </summary>
    public class VideoStreamAppService : IVideoStreamAppService
    {
        /// <summary>
        /// Запрашивается видео поток для указанной камеры с указанной даты и времени
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public  Stream GetVideoStream(Camera camera, DateTime startTime)
        {
            //https://www.youtube.com/watch?v=9H7aa3g3TEI
            FileStream stream = new FileStream(camera.CameraSettings.SourceAddress, FileMode.Open, FileAccess.Read);

            return new BufferedStream(stream);
        }

    }
}