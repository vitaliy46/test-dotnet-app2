using aiPeopleTracker.ViewModels;
using aiPeopleTracker.Wpf.Controls.Players;
using aiPeopleTracker.Wpf.Controls.Players.Model;
using NLog;
using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Dal.Api.Dto;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Data;
using aiPeopleTracker.Startup;
using Unity;

namespace aiPeopleTracker.Views
{
    /// <summary>
    /// Класс для отображения видеопотока с выбранной камеры
    /// </summary>
    public partial class CameraView : UserControl, IViewBase, IDisposable
    {
        // Разрешение стопкадра
        const int _dpi = 96;

        #region Конструктор

        // формат стопкадра
        private PixelFormat _pixelFormat = PixelFormats.Pbgra32;

        public event ViewOpenHandler ViewOpen;

        private CameraViewModel _model;

        private RecognizedObjectViewModel _multitimelineViewModel;

        private readonly ILogger _logger;
        
        private bool _playerSizeAdjusted;

        private bool _playerPositionSet;

        public CameraView(CameraViewModel model,
                          RecognizedObjectViewModel multitimelineViewModel,
                          ILogger logger)
        {
            InitializeComponent();
            _multitimelineViewModel = multitimelineViewModel;
            _logger = logger;
            DataContext = _model = model;
        }

        #endregion

        #region Реализация IViewBase

        public void InitializeView(ViewModelBase model)
        {
            DataContext = _model = (CameraViewModel)model;

            // запуск плеера
            _model.PlayerContext.State = PlayerState.Paused;
            _model.PlayerContext.State = PlayerState.Playing;
        }

        public bool CanClose()
        {
            return true;
        }

        #endregion

        #region Обработчики событий

        private void Player_VideoOpened(object sender, RoutedEventArgs e)
        {
            var player = sender as FileMediaPlayer;

            if (sender != null)
            {
                var width = player.GetVideoWidth();
                var height = player.GetVideoHeight();

                AdjustPlayerSize(width, height);
                SetVideoPosition();

#warning настройка таймлайна
                var duration = player.GetVideoDuration();

                if (duration.HasValue)
                {
                    timeLine.BaseDate = DateTime.Now.AddTicks(-1 * duration.Value.Ticks);
                    timeLine.Duration = duration.Value;

                    //// середина длительности
                    //timeLine.CurrentDate = timeLine.BaseDate.AddTicks(_model.PlayerContext.Position.Ticks/*timeLine.Duration.Ticks / 2*/);

                    timeLine.CurrentDate = timeLine.BaseDate.AddTicks((long)(timeLine.Duration.Ticks - timeLine.Duration.Ticks * timeLine.GetThumbRightMarginRatio()));
                    _model.PlayerContext.Position = TimeSpan.FromTicks((long)(duration.Value.Ticks - duration.Value.Ticks * timeLine.GetThumbRightMarginRatio()));
                }

                timeLine.IsTimeLineAvailable = true;
                timeLine.IsTimeLineAvailable = false;
            }
        }

        // изменение таймлайна
        private void TimeLine_TimeLineValueChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            // e.OldValue - момент начала записи
            // e.NewValue - текущее значение
            
            if (e.OldValue.HasValue && e.NewValue.HasValue)
            {
                var position = e.NewValue.Value - e.OldValue.Value;

                player.SetVideoCurrentPosition(position);
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            _model.PlayerContext.State = PlayerState.Playing;

#warning настройка таймлайна
            timeLine.IsTimeLineAvailable = false;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _model.PlayerContext.State = PlayerState.Paused;

            switch (_model.PlayerContext.CameraId)
            {
                case 1:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 7, 16));
                    break;
                case 2:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 7, 23));
                    break;
                case 3:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 7, 11));
                    break;
                case 4:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 8, 2));
                    break;
                case 5:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 7, 42));
                    break;
                case 6:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 5, 58));
                    break;
                case 7:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 6, 16));
                    break;
                case 8:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 6, 15));
                    break;
                case 9:
                    player.SetVideoCurrentPosition(new TimeSpan(0, 0, 5, 57));
                    break;
            }

#warning настройка таймлайна
            timeLine.CurrentDate = timeLine.BaseDate.AddSeconds(player.GetVideCurrentPosition().TotalSeconds);
            timeLine.IsTimeLineAvailable = true;
        }

        private void Button_Recognize_Click(object sender, RoutedEventArgs e)
        {
            // Временно сделана инициализация UnityContainer
            var mapperConfig = MapperConfig.Create();
            var container = UnityConfig.Create(mapperConfig);
            var analiticService = (IAnaliticService)container.Resolve(typeof(IAnaliticService));

            var cameraRecognizedListViewModel = new CameraRecognizedListViewModel(analiticService)
            {
                PlayerContext = _model.PlayerContext,
                RecognizedPersonsScope = _model.GetRecognizedPersons(_model.PlayerContext.CameraId, null)
            };

            ViewOpen?.Invoke(this, cameraRecognizedListViewModel);
        }

        #endregion

        #region Закрытые методы

        // корректировка размеров грида-контейнера
        private void AdjustPlayerSize(int videoWidth, int videoHeight)
        {
            if (_playerSizeAdjusted)
            {
                return;
            }

            var playerContainerWidth = (double)pnlPlayer.ActualWidth;
            var playerContainerHeight = (double)pnlPlayer.ActualHeight;

            // считаем, что ширина видео больше высоты
            var frameRatio = (double)videoWidth / (double)videoHeight;

            var margin = (playerContainerWidth - (playerContainerHeight * frameRatio)) / 2.0d;

            // устанавливаем левую и правую границы, 
            // чтобы соотношение сторон грида-контейнера стало таким же как у видеокадра
            pnlPlayer.Margin = new Thickness(margin, 0.0, margin, 0.0);

            _playerSizeAdjusted = true;
        }

        private void SetVideoPosition()
        {
            if (_playerPositionSet)
            {
                return;
            }

            player.SetVideoCurrentPosition(_model.PlayerContext.Position);
        }

        #endregion

        #region Реализация IDisposable

        public void Dispose()
        {

        }

        #endregion

        #region tracking

        TrackingState trackingState;
        Rectangle rect=null;

        void SetTrackingState()
        {
            if ((int)trackingState > 1)
                trackingState = 0;
            else
                trackingState++;
        }
        /// <summary>
        /// Кнопка комбинированного действия:
        /// 1. Предоставляет возможность выделить прямоугольную  область на экране
        /// 2. Сделать запрос на сервис за мультитаймлайном и открыть новую форму
        /// просмотра RecognizedPersonView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_tracking_Click(object sender, RoutedEventArgs e)
        {
            SetTrackingState();
            switch(trackingState)
            {
                case TrackingState.Off:button_tracking.Content = "Выделить";break;
                case TrackingState.On:
                    {
                        button_tracking.Content = "Определить движение";
                        if (rect != null)
                            pnlPlayer.Children.Remove(rect);
                        break;
                    }
                case TrackingState.Sent:
                    {
                        if (rect != null)
                        {
                            // snapshot
                            
                            byte[] picture = player.GetSnapshort(_dpi,_pixelFormat);

                            // проверка
                            /*     
                                         string filename = Guid.NewGuid().ToString() + ".bmp";
                                         FileStream fs = new FileStream(filename, FileMode.Create);
                                         fs.Write(picture, 0, picture.Duration);
                                         fs.Close();
                                         */
                            //проверка

                            // стороны прямоугольника:
                            // x1 - left
                            // y1 - top
                            // x2 - right
                            // y2 - bottom
                            var x1 = (double)rect.GetValue(Canvas.LeftProperty);
                            var y1 = (double)rect.GetValue(Canvas.TopProperty);
                            var x2 = (double)rect.GetValue(Canvas.LeftProperty) + (double)rect.GetValue(Canvas.WidthProperty);
                            var y2 = (double)rect.GetValue(Canvas.TopProperty) + (double)rect.GetValue(Canvas.HeightProperty);

                            // левая нижняя точка прямоугольника
                            var leftBottomPoint = new Business.Api.Data.Point(Convert.ToInt32(x1), Convert.ToInt32(y2));
                            // правая верхняя точка прямоугольника
                            var rightTopPoint = new Business.Api.Data.Point(Convert.ToInt32(x2), Convert.ToInt32(y1));

                            // Отправляем запрос на получение мультитаймлайна  
                            var multitimeline = _model.GetMultitimelineByObject(leftBottomPoint, rightTopPoint, picture);
                            button_tracking.Content = "Отправлено";

                            _multitimelineViewModel.Multitimeline = multitimeline;
                            _multitimelineViewModel.LeftBottomPoint = leftBottomPoint;
                            _multitimelineViewModel.RightTopPoint = rightTopPoint;

                            //переход на форму RecognizedPersonView с передачей мультитаймлайна
                            ViewOpen?.Invoke(this, _multitimelineViewModel);
                        }
                        break;

                    }
            }

        }

        enum TrackingState { Off = 0, On = 1, Sent = 2 }

        private void PnlPlayer_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (trackingState == TrackingState.On)
                player.Cursor = Cursors.Cross;
            else
                player.Cursor = Cursors.Hand;
        }

        private void PnlPlayer_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           
        }

        private void PnlPlayer_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(trackingState==TrackingState.On)
            {
                if (rect != null)
                    pnlPlayer.Children.Remove(rect);
                var point = e.GetPosition(pnlPlayer);
                rect = new Rectangle();
                rect.Stroke = Brushes.White;
                rect.SetValue(Canvas.LeftProperty, point.X);
                rect.SetValue(Canvas.TopProperty, point.Y);
                pnlPlayer.Children.Add(rect);

            }
        }

        private void PnlPlayer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
          /*  if (rect != null)
            {
                SetTrackingState();
            }*/
        }

        private void PnlPlayer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (rect != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var point = e.GetPosition(pnlPlayer);
                    // Вычисляем размер прямоугольника
                    var width = (point.X > (double)rect.GetValue(Canvas.LeftProperty)) ? (point.X - (double)rect.GetValue(Canvas.LeftProperty)) : 0;
                    var height = (point.Y > (double)rect.GetValue(Canvas.TopProperty)) ? (point.Y - (double)rect.GetValue(Canvas.TopProperty)) : 0;
                    rect.SetValue(Canvas.WidthProperty, width);
                    rect.SetValue(Canvas.HeightProperty, height);
                }
            }
        }

        #endregion tracking

        private void FloorControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}