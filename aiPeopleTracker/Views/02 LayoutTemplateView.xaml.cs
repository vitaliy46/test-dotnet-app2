using aiPeopleTracker.ViewModels;
using aiPeopleTracker.Wpf.Controls.Helpers;
using aiPeopleTracker.Wpf.Controls.Players;
using NLog;
using System;
using System.Windows;
using System.Windows.Controls;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Startup;
using Unity;

namespace aiPeopleTracker.Views
{
    /// <summary>
    /// Класс для отображения сетки с видеопотоками
    /// </summary>
    public partial class LayoutTemplateView : UserControl, IViewBase, IDisposable
    {
        #region Конструктор и данные

        public event ViewOpenHandler ViewOpen;

        private LayoutTemplateViewModel _model;

        private readonly ILogger _logger;

        private bool _gridSizeAdjusted; // исправлены соотношения сторон грида-контейнера

        //LayoutTemplateViewModel model,
        //    ILogger logger
        public LayoutTemplateView()
        {
            InitializeComponent();

            //_logger = logger;

            //DataContext = _model = model;
        }

        #endregion

        #region Реализация IViewBase

        public void InitializeView(ViewModelBase model)
        {
            _gridSizeAdjusted = false;

            DataContext = _model = (LayoutTemplateViewModel)model;

            // наполнение списка контекстов плееров
            _model.FillPlayersContexts();

            // запуск всех плееров
            _model.CommonState = PlayerState.Playing;
        }

        public bool CanClose()
        {
            return true;
        }

        #endregion

        #region Обработчики событий

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            _model.CommonState = PlayerState.Playing;

#warning настройка таймлайна
            timeLine.IsTimeLineAvailable = false;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _model.CommonState = PlayerState.Paused;

#warning настройка таймлайна
            var position = GetFirstCameraCurrentPosition();

            timeLine.CurrentDate = timeLine.BaseDate.AddSeconds(position.TotalSeconds);

            timeLine.IsTimeLineAvailable = true;
        }

        private void Player_DoubleClick(object sender, PlayerDoubleClickEventArgs e)
        {
            e.Data.Position = GetFirstCameraCurrentPosition();

            //EntityViewOpen?.Invoke(this, e.Data);

            // Временно сделана инициализация UnityContainer
            var mapperConfig = MapperConfig.Create();
            var container = UnityConfig.Create(mapperConfig);
            var analiticService = (IAnaliticService)container.Resolve(typeof(IAnaliticService));

            var cameraViewModel = new CameraViewModel(analiticService)
            {
                PlayerContext = e.Data
            };

            ViewOpen?.Invoke(this, cameraViewModel);
        }

        private void Player_VideoOpened(object sender, RoutedEventArgs e)
        {
            var player = sender as FileMediaPlayerWithMarkers;

            if (sender != null)
            {
                var width = player.GetVideoWidth();
                var height = player.GetVideoHeight();

                var needToSetupTimeLine = !_gridSizeAdjusted;

                //AdjustUniformGridSize(width, height);

#warning настройка таймлайна
                if (needToSetupTimeLine)
                {
                    var duration = player.GetVideoDuration();

                    if (duration.HasValue)
                    {
                        timeLine.BaseDate = DateTime.Now.AddTicks(-1 * duration.Value.Ticks);
                        timeLine.Duration = duration.Value;

                        // середина длительности
                        timeLine.CurrentDate = timeLine.BaseDate.AddTicks(timeLine.Duration.Ticks / 2);
                    }

                    timeLine.IsTimeLineAvailable = true;
                    timeLine.IsTimeLineAvailable = false;
                }
            }
        }

        private void TimeLine_TimeLineValueChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            // e.OldValue - момент начала записи
            // e.NewValue - текущее значение

            if (e.OldValue.HasValue && e.NewValue.HasValue)
            {
                var position = e.NewValue.Value - e.OldValue.Value;

                foreach(var ctx in _model.PlayersContexts)
                {
                    ctx.Position = position;
                }
            }
        }

        #endregion

        #region Закрытые методы

        // корректировка размеров грида-контейнера
        private void AdjustUniformGridSize(int videoWidth, int videoHeight)
        {
            if (_gridSizeAdjusted)
            {
                return;
            }

            var gridWidth = (double)gridCameras.ActualWidth;
            var gridHeight = (double)gridCameras.ActualHeight;

            // считаем, что ширина видео больше высоты
            var frameRatio = (double)videoWidth / (double)videoHeight;

            var margin = (gridWidth - (gridHeight * frameRatio)) / 2.0d;

            // устанавливаем левую и правую границы, 
            // чтобы соотношение сторон грида-контейнера стало таким же как у видеокадра
            gridCameras.Margin = new Thickness(margin, 0.0, margin, 0.0);

            _gridSizeAdjusted = true;
        }

        private TimeSpan GetFirstCameraCurrentPosition()
        {
            var result = default(TimeSpan);

            var firstItem = gridCameras.Items[0];

            var container = gridCameras.ItemContainerGenerator.ContainerFromItem(firstItem);

            var itemControls = ControlTemplateHelper.GetChildren(container);

            if (itemControls != null)
            {
                foreach (var ctrl in itemControls)
                {
                    var player = ctrl as FileMediaPlayerWithMarkers;

                    if (player != null)
                    {
                        result = player.GetVideCurrentPosition();

                        break;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Реализация IDisposable

        public void Dispose()
        {
           // _model.PlayersContexts[0].(); //
        }

        #endregion
    }
}