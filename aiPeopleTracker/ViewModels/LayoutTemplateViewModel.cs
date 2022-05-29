using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Wpf.Controls.Players.Model;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;

namespace aiPeopleTracker.ViewModels
{
    public class LayoutTemplateViewModel : ViewModelBase
    {
        public event EntitySelectedHandler EntitySelected;

        private readonly ILayoutTemplateCrudService _layoutTemplateCrudService;

        public LayoutTemplateViewModel(ILayoutTemplateCrudService layoutTemplateCrudService)
        {
            _layoutTemplateCrudService = layoutTemplateCrudService;

            _playersContexts = new ObservableCollection<PlayerDataContext>();
        }

        private LayoutTemplate _layoutTemplate;

        /// <summary>Шаблон</summary>
        public LayoutTemplate LayoutTemplate
        {
            get { return _layoutTemplate; }
            set
            {
                SetField(ref _layoutTemplate, value);

                if (_layoutTemplate != null)
                {
                    _layoutTemplate = _layoutTemplateCrudService.Get(_layoutTemplate.Id);
                }
            }
        }

        private ObservableCollection<PlayerDataContext> _playersContexts;

        /// <summary>Конексты плееров</summary>
        public ObservableCollection<PlayerDataContext> PlayersContexts
        {
            get { return _playersContexts; }
            set { SetField(ref _playersContexts, value); }
        }

        private PlayerState _commonState;

        /// <summary>Общее состояние всех плееров шаблона</summary>
        public PlayerState CommonState
        {
            get { return _commonState; }
            set
            {
                SetField(ref _commonState, value);

                // применение состояния ко всем плеерам
                PlayersContexts.ForEach(ctx => ctx.State = value);
            }
        }

        /// <summary>
        /// Наполнение списка контекстов плееров
        /// </summary>
        public void FillPlayersContexts()
        {
            for (var y = 0; y < this.LayoutTemplate.ItemsY; y++)
            {
                for (var x = 0; x < this.LayoutTemplate.ItemsX; x++)
                {
                    if (this.LayoutTemplate.CameraLinks == null)
                    {
#warning Заглушка, показывающая, что камеры не привязаны                        
                    }

                    var cameraLink = GetCameraLinkByCoordinates(x, y);

                    if (cameraLink != null)
                    {
                        if (cameraLink.Camera.State == CameraState.Active)
                        {
                            this.PlayersContexts.Add(new PlayerDataContext(cameraLink.LayoutTemplateId, cameraLink.CameraId)
                            {
                                Stretch = Stretch.Uniform,
                                State = PlayerState.Stopped,
                                Source = new Uri(cameraLink.Camera.CameraSettings.SourceAddress)
                            });
                        }
                        else
                        {
#warning Заглушка, сигнализирующая о том, что камера неактивна
                        }
                    }
                    else
                    {
#warning Заглушка, сигнализирующая об отсутствии камеры для ячейки шаблона
                    }
                }
            }
        }


        private LayoutTemplateCameraLink GetCameraLinkByCoordinates(int x, int y)
        {
            return this.LayoutTemplate?
                .CameraLinks?
                .Where(cl => cl.X == x + 1
                             && cl.Y == y + 1)
                .FirstOrDefault();
        }

        private void AddLayoutTemplateUsage(LayoutTemplate value)
        {
            
        }

    }
}