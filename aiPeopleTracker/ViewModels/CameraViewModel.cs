using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Wpf.Controls.Players.Model;
using NLog;
using System;
using aiPeopleTracker.Business.Api.Data;

namespace aiPeopleTracker.ViewModels
{
    public class CameraViewModel : ViewModelBase
    {
        #region Конструктор

        public event EntitySelectedHandler EntitySelected;

        protected readonly IAnaliticService _aService;

        public CameraViewModel(IAnaliticService aService)
        {
            _aService = aService;
        }

        #endregion

        #region Свойства

        private PlayerDataContext _playerContext;

        /// <summary>Конекст плеера</summary>
        public PlayerDataContext PlayerContext
        {
            get { return _playerContext; }
            set { SetField(ref _playerContext, value); }
        }

        #endregion

        /// <summary>
        /// Запрос на распознавание всех людей находящхся на отправляемом стоп-кадре
        /// </summary>
        /// <param name="picture">стоп-кадр на котором необходимо распознать людей</param>
        /// <param name="cameraId"></param>
        public RecognizedPersonsScope GetRecognizedPersons(int cameraId, byte[] picture)
        {
            var recognizedPersonsScope = _aService.GetRecognizedPersons(cameraId, picture);

            return recognizedPersonsScope;
        }

        /// <summary>
        /// Запращиваются видеофрагменты обнаруживающие движения в 
        /// зоне обозначенной углами квадрата на изображении
        /// </summary>
        /// <param name="picture">картинка на которой выделен прямоугольником объект</param>
        /// <param name="leftBottomPoint">левая нижняя точка прямоугольника</param>
        /// <param name="rightTopPoint">правая верхняя точка прямоугольника</param>
        public IMultitimeline GetMultitimelineByObject(Point leftBottomPoint, Point rightTopPoint, byte[] picture)
        {
           IMultitimeline multitimeline = _aService.GetMultitimelineByObject(leftBottomPoint, rightTopPoint, _playerContext.CameraId, picture);

            return multitimeline;
        }
    }
}