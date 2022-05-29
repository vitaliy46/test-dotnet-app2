using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Wpf.Controls.Players.Model;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using System.Collections.Specialized;
using aiPeopleTracker.Wpf.Controls;
using System.Collections.Generic;

namespace aiPeopleTracker.ViewModels
{
    public class CameraRecognizedListViewModel : ViewModelBase
    {
        protected readonly IAnaliticService _analiticService;

        private PlayerDataContext _playerContext;

        /// <summary>Конекст плеера</summary>
        public PlayerDataContext PlayerContext
        {
            get { return _playerContext; }
            set { SetField(ref _playerContext, value); }
        }

        private RecognizedPersonsScope _recognizedPersonsScope;

        /// <summary>Общее состояние всех плееров шаблона</summary>
        public RecognizedPersonsScope RecognizedPersonsScope
        {
            get { return _recognizedPersonsScope; }
            set
            {
                SetField(ref _recognizedPersonsScope, value);
            }
        }

        public CameraRecognizedListViewModel(IAnaliticService analiticService)
        {
            _analiticService = analiticService ?? throw new ArgumentNullException(nameof(analiticService));
        }

        /// <summary>
        /// Запращиваются видеофрагменты обнаруживающие движения в 
        /// зоне обозначенной углами квадрата на изображении
        /// </summary>
        /// <param name="personId">персона, для которой неоходимо получить мультитаймлайн с отрезками видео, где она появляется</param>
        public IMultitimeline GetMultitimelineByPerson(int personId)
        {
            IMultitimeline multitimeline = _analiticService.GetMultitimelineByPerson(personId);

            return multitimeline;
        }
    }
}