using aiPeopleTracker.ViewModels;
using aiPeopleTracker.Wpf.Controls.Players;
using aiPeopleTracker.Wpf.Controls.Players.Model;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Views
{
    /// <summary>
    /// Класс для отображения видеопотока с выбранной камеры, справа выводится список распознанных персон
    /// </summary>
    public partial class CameraRecognizedListView : UserControl, IViewBase, IDisposable
    {
        #region Конструктор

        public event ViewOpenHandler ViewOpen;

        private CameraRecognizedListViewModel _model;

        private readonly ILogger _logger;

        private bool _playerSizeAdjusted;

        private bool _playerPositionSet;

        public CameraRecognizedListView(CameraRecognizedListViewModel model,
            ILogger logger)
        {
            InitializeComponent();

            _logger = logger;
        }

        #endregion

        #region Реализация IViewBase

        public void InitializeView(ViewModelBase model)
        {
            DataContext = _model = (CameraRecognizedListViewModel)model;

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

        /// <summary>
        /// Обработчик отрисовки формы
        /// </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var recognizedPersonCardList = new ArrayList();

            foreach (var recognizedPerson in _model.RecognizedPersonsScope.RecognizedPeople)
            {
                var recognizedRectangle = new Border();
                recognizedRectangle.SetValue(Canvas.LeftProperty, (double)recognizedPerson.PointLeftDown.X);
                recognizedRectangle.SetValue(Canvas.TopProperty, (double)recognizedPerson.PointRightUp.Y);
                recognizedRectangle.Width = recognizedPerson.PointRightUp.X - recognizedPerson.PointLeftDown.X;
                recognizedRectangle.Height = recognizedPerson.PointLeftDown.Y - recognizedPerson.PointRightUp.Y;
                recognizedRectangle.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                recognizedRectangle.BorderThickness = new Thickness(2);
                recognizedRectangle.CornerRadius = new CornerRadius(10.0d);

                recognizedRectanglesCanvas.Children.Add(recognizedRectangle);

                // Берем байтовый массив из базы и преобразуем в BitmapImage изображение для вставки в UI
                var image = new BitmapImage();
                using (var memoryStream = new MemoryStream(recognizedPerson.Person.Photo))
                {
                    memoryStream.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = memoryStream;
                    image.EndInit();
                }
                image.Freeze();

                // Используем анонимный тип для карточки распознанной персоны чтобы не задавать отдельный класс
                var recognizedPersonCard = new { Fio = $"{recognizedPerson.Person.Surname} {recognizedPerson.Person.Name} {recognizedPerson.Person.Patronymic}", Image = image };
                recognizedPersonCardList.Add(recognizedPersonCard);
            }

            // Задаем форму ItemsControl для размещения в нем карточек распознанных персон
            FrameworkElementFactory factoryPanel = new FrameworkElementFactory(typeof(UniformGrid));
            factoryPanel.SetValue(UniformGrid.RowsProperty, _model.RecognizedPersonsScope.RecognizedPeople.Count);
            factoryPanel.SetValue(UniformGrid.ColumnsProperty, 1);
            RecognizedPersonsList.ItemsPanel = new ItemsPanelTemplate(factoryPanel);

            RecognizedPersonsList.ItemsSource = recognizedPersonCardList;
        }

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

                    // середина длительности
                    timeLine.CurrentDate = timeLine.BaseDate.AddTicks(_model.PlayerContext.Position.Ticks/*timeLine.Duration.Ticks / 2*/);
                }

                timeLine.IsTimeLineAvailable = true;
                timeLine.IsTimeLineAvailable = false;
            }

            _model.PlayerContext.State = PlayerState.Paused;
            timeLine.IsTimeLineAvailable = true;

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

#warning настройка таймлайна
            timeLine.CurrentDate = timeLine.BaseDate.AddSeconds(player.GetVideCurrentPosition().TotalSeconds);
            timeLine.IsTimeLineAvailable = true;
        }

        private void Person_Card_Click(object sender, RoutedEventArgs e)
        {
            var recognizedPersonViewModel = new RecognizedPersonViewModel();
            recognizedPersonViewModel.Multitimeline = _model.GetMultitimelineByPerson(1);

            ViewOpen?.Invoke(this, recognizedPersonViewModel);
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

    }
}