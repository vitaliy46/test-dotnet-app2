using aiPeopleTracker.ViewModels;
using aiPeopleTracker.Wpf.Controls.Players;
using aiPeopleTracker.Wpf.Controls.Players.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Wpf.Controls.Helpers;
using System.Collections.Specialized;
using System.Linq;
using aiPeopleTracker.Wpf.Controls.Multitimeline;

namespace aiPeopleTracker.Views
{
    /// <summary>
    /// Вывод таймлайна с отрезками видеопотоков на которых появляется отслеживаемая персона
    /// </summary>
    public partial class RecognizedObjectView : UserControl, IViewBase, IDisposable
    {
        public event ViewOpenHandler ViewOpen;

        private RecognizedObjectViewModel _model;

        private MultitimelineObjectControl _multitimelineControl;

        /// <summary>
        /// Признак того, что соотношения сторон грида-контейнера исправлены
        /// </summary>
        private bool _isGridSizeAdjusted; 

        /// <summary>
        /// используется для перемещения ползунка по таймлайну
        /// с заданными временными интервалами
        /// </summary>
        private DispatcherTimer _timer;

        private readonly IAnaliticService _analiticService;

        /// <summary>
        /// Времено используется для избежания повтороной инициализации мультитаймлайна 
        /// при повторном рендеренги формы
        /// </summary>
        private bool _isAlreadyMultitimelinePrepared;

        // Временно используется для загрузки первого кадра первого клипа
        // Меняем на значение true когда показан первый кадр первого клипа
        private bool _firstVideoClipFrameIsShown = false;

        public RecognizedObjectView(IAnaliticService analiticService, MultitimelineObjectControl multitimelineControl)
        {
            InitializeComponent();

            _multitimelineControl = multitimelineControl;

            _analiticService = analiticService ?? throw new ArgumentNullException(nameof(analiticService));

            _timer = new DispatcherTimer();
        }
        #region Реализация IViewBase

        public void InitializeView(ViewModelBase model)
        {
            _isGridSizeAdjusted = false;

            DataContext = _model = (RecognizedObjectViewModel) model;

            // запуск всех плееров
            _model.CommonState = PlayerState.Paused;
        }
        
        //TODO разобраться, что это за странный метод
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

            //для избежания повторного вызова процесса инициализации вьюхи 
            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/89a2c964-aa03-4f1a-abb4-228791132d84/onrender-only-called-once?forum=wpf
            //Вообще инициализацию нужно делать не в этом методе, a  в методе InitializeView()
            //if (_isAlreadyMultitimelinePrepared) return;

            drawRecognizedRectangle();


            // Временно добавлена проверка пока не убран 2-й вызов OnRender
            if (_multitimelineControl != null)
            {
                _model.Multitimeline.PlayableVideoClips.CollectionChanged -= PlayableVideoClipsCollectionChangedHandler;
            }

            // Растягиваем мультитаймлайн по ширине внешнего контейнера
            _multitimelineControl.Width = multitimelineContainer.ActualWidth;

            // Добавление  мультитаймлайн-контрола в разметку вьюхи
            if (!multitimelineContainer.Children.Contains(_multitimelineControl))
            {
                multitimelineContainer.Children.Add(_multitimelineControl);
            }

            PrepareMultitimeline();

            // запускаем воспроизведение чтобы выйти на первый кадр первого видеоклипа
            MultitimeLinePlay();
        }

        /// <summary>
        /// Прорисовывается квадрат, фокусирующийся на отслеживаемом объекте
        /// </summary>
        private void drawRecognizedRectangle()
        {
            var recognizedRectangle = new Border();
            recognizedRectangle.SetValue(Canvas.LeftProperty, (double)_model.LeftBottomPoint.X);
            recognizedRectangle.SetValue(Canvas.TopProperty, (double)_model.RightTopPoint.Y);
            recognizedRectangle.Width = _model.RightTopPoint.X - _model.LeftBottomPoint.X;
            recognizedRectangle.Height = _model.LeftBottomPoint.Y - _model.RightTopPoint.Y;
            recognizedRectangle.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            recognizedRectangle.BorderThickness = new Thickness(2);
            recognizedRectangle.CornerRadius = new CornerRadius(10.0d);

            // Добавление прямоугольника указывающего на отслеживаемый объект
            recognizedRectangleCanvas.Children.Add(recognizedRectangle);

        }

        private void PrepareMultitimeline()
        {
            if (_model.Multitimeline == null)
            {
                throw new Exception("В представление передана неподготовленная модель ");
            }

            // передача IMultitimeline элементу управления _multitimelineControl
            _multitimelineControl.Initialize(_model.Multitimeline);

            // Подписка на изменение коллекции видеоклипов, проигрываемых на данный момент
            _model.Multitimeline.PlayableVideoClips.CollectionChanged += PlayableVideoClipsCollectionChangedHandler;

            //параметр по сути определяет дискретность, а значит и плавность прохождения движка по таймлайну
            _timer.Interval = TimeSpan.FromMilliseconds(_model.Multitimeline.Tracker.PlaySpeed);

            _timer.Tick += _model.Multitimeline.Tracker.Play;

           // _isAlreadyMultitimelinePrepared = true;
        }

        /// <summary>
        /// Обработчик события. Изменяет видеопотоки в сетке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayableVideoClipsCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            _model.FillPlayersContexts(_multitimelineControl, e);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            MultitimeLinePlay();
        }

        private void MultitimeLinePlay()
        {
            // При проигрывании ползунок слайдера подписывается на изменение позиции трекера чтобы перемещаться синхронно с ним, 
            // отменяем зависимость трекера от положения ползунка сайдера
            _model.Multitimeline.Tracker.PositionChange += _multitimelineControl.PositionChangeHandler;
            _multitimelineControl.ThumbPositionChanged -= _model.Multitimeline.Tracker.TimeLinePositionChangedHandler;
            // Обработка события, изменение позиции проигрываемых клипов при ручном изменения позиции трекера с помощью ползунка
            // отписка от события
            _model.Multitimeline.Tracker.PositionChange -= TrackerPositionChangeOnPausedHandler;

            _model.CommonState = PlayerState.Playing;

            _timer.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            MultitimeLineStop();
        }

        private void MultitimeLineStop()
        {
            // когда проигрывание остановлено, ползунок слайдера управляет позицией трекера с помощью события TimeLinePositionChanged
            // слайдер мультитаймлайна отписывается от изменения позиции трекера
            _model.Multitimeline.Tracker.PositionChange -= _multitimelineControl.PositionChangeHandler;
            _multitimelineControl.ThumbPositionChanged += _model.Multitimeline.Tracker.TimeLinePositionChangedHandler;
            // Обработка события, изменение позиции проигрываемых клипов при ручном изменения позиции трекера с помощью ползунка
            // подписка на событие
            _model.Multitimeline.Tracker.PositionChange += TrackerPositionChangeOnPausedHandler;

            _model.CommonState = PlayerState.Paused;

            _timer.Stop();
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            // Записываем состояние воспроизведения в переменную чтобы потом восстановить
            var commonState = _model.CommonState;

            // Останавливаем воспроизведение (если оно запущено), чтобы иметь возможность изменять на позицию трекера
            MultitimeLineStop();

            // Текущий клип, поторый воспроизводится на данный момент, нам надо найти предыдущий
            var currentClip = _model.Multitimeline.VideoClips.LastOrDefault(x => x.BeginTime < _model.Multitimeline.Tracker.Position);

            if (currentClip != null)
            {
                // Вычисляем индекс предыдущего клипа и получаем его объект
                var currentClipIndex = _model.Multitimeline.VideoClips.IndexOf(currentClip);
                var previousClipIndex = currentClipIndex != 0 ? currentClipIndex - 1 : 0;
                var previousClip = _model.Multitimeline.VideoClips.ElementAt(previousClipIndex);

                if (previousClip != null)
                {
                    // Во избежание скачка сразу через 2 клипа передвигаем позицию трекера вперед на величину PlaySpeed
                    _model.Multitimeline.Tracker.Position = previousClip.BeginTime.AddMilliseconds(_model.Multitimeline.Tracker.PlaySpeed);
                    _multitimelineControl.PositionChangeHandler(_model.Multitimeline.Tracker.Position);
                }
            }

            // Восстанавливаем состояние воспроизведения
            if (commonState == PlayerState.Playing) MultitimeLinePlay();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            // Записываем состояние воспроизведения в переменную чтобы потом восстановить
            var commonState = _model.CommonState;

            // Останавливаем воспроизведение (если оно запущено), чтобы иметь возможность изменять на позицию трекера
            MultitimeLineStop();

            // Получаем объект следующего клипа
            var nextClip = _model.Multitimeline.VideoClips.FirstOrDefault(x => x.BeginTime > _model.Multitimeline.Tracker.Position);
            if (nextClip != null)
            {
                // Чтобы переход на следующий клип осуществлялся всегда при единственном щелчке на кнопке, передвигаем позицию трекера вперед на величину PlaySpeed
                _model.Multitimeline.Tracker.Position = nextClip.BeginTime.AddMilliseconds(_model.Multitimeline.Tracker.PlaySpeed);
                _multitimelineControl.PositionChangeHandler(_model.Multitimeline.Tracker.Position);
            }

            // Восстанавливаем состояние воспроизведения
            if (commonState == PlayerState.Playing) MultitimeLinePlay();
        }

        private void Player_DoubleClick(object sender, PlayerDoubleClickEventArgs e)
        {
        }

        private void Player_VideoOpened(object sender, RoutedEventArgs e)
        {
            if (!_firstVideoClipFrameIsShown)
            {
                // Как только вышли на первый кадр первого видео клипа, останавливаем воспроизведение
                MultitimeLineStop();
                _firstVideoClipFrameIsShown = true;
            }
        }

        // Обработка события, изменение позиции проигрываемых клипов при ручном изменения позиции трекера с помощью ползунка
        private void TrackerPositionChangeOnPausedHandler(DateTime position)
        {
            _model.TrackerPositionChangeOnPaused(_multitimelineControl);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
 
        #endregion

        #region Реализация IDisposable

        public void Dispose()
        {
        }

        #endregion

    }
}