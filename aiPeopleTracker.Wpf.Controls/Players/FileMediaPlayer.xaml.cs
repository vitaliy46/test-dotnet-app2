using aiPeopleTracker.Wpf.Controls.Players.Model;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using aiPeopleTracker.Business.Api.Constants;
using System.Windows.Media.Imaging;
using System.IO;

namespace aiPeopleTracker.Wpf.Controls.Players
{
    public partial class FileMediaPlayer : UserControl, ISnapshort
    {
        #region Свойства зависимости

        public string SourceFileName
        {
            get { return (string)GetValue(SourceFileNameProperty); }
            set { SetValue(SourceFileNameProperty, value); }
        }

        public static readonly DependencyProperty SourceFileNameProperty = DependencyProperty.Register("SourceFileName", typeof(string), typeof(FileMediaPlayer));

        public PlayerState State
        {
            get { return (PlayerState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(PlayerState), typeof(FileMediaPlayer),
            new FrameworkPropertyMetadata(PlayerState.Stopped, new PropertyChangedCallback(OnStateChanged)));

        private static void OnStateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as FileMediaPlayer;

            if (ctrl != null)
            {
                var stateOld = (PlayerState)e.OldValue;
                var stateNew = (PlayerState)e.NewValue;

                if (stateOld != stateNew)
                {
                    switch (stateNew)
                    {
                        case PlayerState.Stopped:
                            {
                                if (stateOld == PlayerState.Playing)
                                {
                                    ctrl.State = stateNew;
                                    ctrl.player.Stop();
                                }
                                break;
                            }
                        case PlayerState.Paused:
                            {
                                if (stateOld == PlayerState.Playing)
                                {
                                    ctrl.State = stateNew;
                                    ctrl.player.Pause();
                                }
                                break;
                            }
                        case PlayerState.Playing:
                            {
                                if (   (   stateOld == PlayerState.Paused 
                                        || stateOld == PlayerState.Stopped) 
                                    && (ctrl.player.Source != null))
                                {
                                    ctrl.State = stateNew;
                                    ctrl.player.Play();
                                }
                                break;
                            }
                    }
                }
            }
        }

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(FileMediaPlayer));

        public bool IsBottomPanelVisible
        {
            get { return (bool)GetValue(IsBottomPanelVisibleProperty); }
            set { SetValue(IsBottomPanelVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsBottomPanelVisibleProperty = DependencyProperty.Register("IsBottomPanelVisible", typeof(bool), typeof(FileMediaPlayer), new PropertyMetadata(false));

        public bool IsScalingEnabled
        {
            get { return (bool)GetValue(IsScalingEnabledProperty); }
            set { SetValue(IsScalingEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsScalingEnabledProperty = DependencyProperty.Register("IsScalingEnabled", typeof(bool), typeof(FileMediaPlayer), new PropertyMetadata(false));

        /// <summary>
        /// Текущее положение воспроизведения
        /// </summary>
        public TimeSpan? CurrentPosition
        {
            get { return (TimeSpan?)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }

        public static readonly DependencyProperty CurrentPositionProperty = DependencyProperty.Register("SliderValue", typeof(TimeSpan?), typeof(FileMediaPlayer),
            new FrameworkPropertyMetadata(default(TimeSpan?), new PropertyChangedCallback(OnCurrentPositionChanged)));

        private static void OnCurrentPositionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as FileMediaPlayer;

            if (ctrl != null)
            {
                var position = (TimeSpan?)e.NewValue;

                if (position.HasValue)
                {
                    ctrl.player.Position = position.Value;

                    // Вывод стоп-кадра если проигрывание стоит на паузе
                    if (ctrl.State == PlayerState.Paused)
                    {
                        ctrl.player.Play();
                        ctrl.player.Pause();
                    }
                }
            }
        }

        #endregion

        #region События

        public static readonly RoutedEvent DoubleClickEvent = EventManager.RegisterRoutedEvent("DoubleClick", RoutingStrategy.Bubble, typeof(PlayerDoubleClickEventHandler), typeof(FileMediaPlayer));

        public event PlayerDoubleClickEventHandler DoubleClick
        {
            add { AddHandler(DoubleClickEvent, value); }
            remove { RemoveHandler(DoubleClickEvent, value); }
        }

        public static readonly RoutedEvent VideoOpenedEvent = EventManager.RegisterRoutedEvent("VideoOpened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FileMediaPlayer));

        public event RoutedEventHandler VideoOpened
        {
            add { AddHandler(VideoOpenedEvent, value); }
            remove { RemoveHandler(VideoOpenedEvent, value); }
        }

        #endregion

        #region Конструктор

        private readonly double _maxScaleRatio = 10.0d;

        public FileMediaPlayer()
        {
            InitializeComponent();

            _doubleClickTimer.Interval = TimeSpan.FromMilliseconds(GetDoubleClickTime());
            _doubleClickTimer.Tick += (s, e) => _doubleClickTimer.Stop();
        }

        public FileMediaPlayer(PlayerDataContext dataContext) : this()
        {
            DataContext = dataContext;
        }

        #endregion

        #region Обработчики

        [DllImport("user32.dll")]
        private static extern uint GetDoubleClickTime();

        private DispatcherTimer _doubleClickTimer = new DispatcherTimer();

        private Point _dragStartPoint;

        private Thickness _marginStart;

        private Point? _scaleCenter;

        private void Player_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(this);
            _marginStart = player.Margin;

            if (!_doubleClickTimer.IsEnabled)
            {
                _doubleClickTimer.Start();
            }
            else
            {
                // остановка воспроизведения
                State = PlayerState.Paused;

                // передача текущего момента воспроизведения
                var ctx = DataContext as PlayerDataContext;

                if (ctx != null)
                {
                    ctx.Position = player.Position;
                }

                // событие
                var args = new PlayerDoubleClickEventArgs((PlayerDataContext)DataContext);
                args.RoutedEvent = DoubleClickEvent;
                RaiseEvent(args);
            }
        }

        private void Player_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsScalingEnabled)
            {
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var currentPoint = e.GetPosition(this);

                var deltaX = currentPoint.X - _dragStartPoint.X;
                var deltaY = currentPoint.Y - _dragStartPoint.Y;

                var marginLeft = _marginStart.Left + deltaX;
                var marginRight = _marginStart.Right - deltaX;
                var marginTop = _marginStart.Top + deltaY;
                var marginBottom = _marginStart.Bottom - deltaY;

                if (marginLeft > 0)
                {
                    marginRight += marginLeft;
                    marginLeft = 0;
                }

                if (marginRight > 0)
                {
                    marginLeft += marginRight;
                    marginRight = 0;
                }

                if (marginTop > 0)
                {
                    marginBottom += marginTop;
                    marginTop = 0;
                }

                if (marginBottom > 0)
                {
                    marginTop += marginBottom;
                    marginBottom = 0;
                }

                player.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
            }
        }

        private void Slider_ScaleValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScalePlayer(1.0d + (e.NewValue / (sender as Slider).Maximum) * (_maxScaleRatio - 1.0d));
        }

        private void Player_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (!IsScalingEnabled)
            {
                return;
            }

            //_scaleCenter = e.GetPosition(this);

            if (e.Delta > 0)
            {
                sldScale.Value = Math.Min(sldScale.Value + (sldScale.Maximum - sldScale.Minimum) / 100.0d, sldScale.Maximum);
            }
            else
            {
                sldScale.Value = Math.Max(sldScale.Value - (sldScale.Maximum - sldScale.Minimum) / 100.0d, sldScale.Minimum);
            }
        }

        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            var args = new RoutedEventArgs(VideoOpenedEvent, this);
            RaiseEvent(args);
        }

        #endregion

        #region Открытые методы

        public int GetVideoWidth()
        {
            return player.NaturalVideoWidth;
        }

        public int GetVideoHeight()
        {
            return player.NaturalVideoHeight;
        }

        public TimeSpan? GetVideoDuration()
        {
            return player.NaturalDuration.HasTimeSpan ? player.NaturalDuration.TimeSpan : (TimeSpan?)null;
        }

        public TimeSpan GetVideCurrentPosition()
        {
            return player.Position;
        }

        public void SetVideoCurrentPosition(TimeSpan position)
        {
            player.Position = position;
        }

        #endregion

        #region ISnapshort
        public byte[] GetSnapshort(int dpi, PixelFormat pixelFormat)
        {
            var bmp = new RenderTargetBitmap(Convert.ToInt32(player.ActualWidth), Convert.ToInt32(player.ActualHeight), dpi, dpi, pixelFormat);
            bmp.Render(player);
            var frame = BitmapFrame.Create(bmp);
            var encoder = new BmpBitmapEncoder();
            MemoryStream stream = new MemoryStream();
            encoder.Frames.Add(frame);
            encoder.Save(stream);
            stream.Position = 0;
            return stream.ToArray();
        }

        #endregion ISnapshort

        #region Закрытые методы

        /// <summary>
        /// Масштабирование плера
        /// </summary>
        /// <param name="ratio">Коэффициент масштабирования</param>
        /// <param name="center">Центр масштабирования (если не задан - текущий центр) (НЕ ИСПОЛЬЗУЕТСЯ)</param>
        /// <param name="maxRatio">Максимальный коэффициент масштабирования</param>
        private void ScalePlayer(double ratio)
        {
            _scaleCenter = _scaleCenter ?? new Point(player.ActualWidth / 2.0d, player.ActualHeight / 2.0d);

            // размер ограничивающей области (плеера)
            var clipWidth = (double)pnlPlayer.ActualWidth;
            var clipHeight = (double)pnlPlayer.ActualHeight;

            // новые размеры, учитывая максимальный коэффициент масштабирования
            var newWidth = Math.Min(clipWidth * ratio, clipWidth * _maxScaleRatio);
            var newHeight = Math.Min(clipHeight * ratio, clipHeight * _maxScaleRatio);

            // вычисление полей
            var marginHorizontalTotal = Math.Abs(newWidth - clipWidth);
            var marginVerticalTotal = Math.Abs(newHeight - clipHeight);

            var marginLeft = (marginHorizontalTotal / 2.0) - (_scaleCenter.Value.X - clipWidth / 2.0);
            var marginRight = marginHorizontalTotal - marginLeft;

            var marginTop = (marginVerticalTotal / 2.0) - (_scaleCenter.Value.Y - clipHeight / 2.0);
            var marginBottom = marginVerticalTotal - marginTop;

            player.Margin = new Thickness(-1.0d * marginLeft, -1.0d * marginTop, -1.0d * marginRight, -1.0d * marginBottom);

            /*
            // размер ограничивающей области (плеера)
            var clipWidth = (double)pnlPlayer.ActualWidth;
            var clipHeight = (double)pnlPlayer.ActualHeight;

            // новые размеры, учитывая максимальный коэффициент масштабирования
            var newWidth = Math.Min(clipWidth * ratio, clipWidth * _maxScaleRatio);
            var newHeight = Math.Min(clipHeight * ratio, clipHeight * _maxScaleRatio);

            var marginLeft = _scaleCenter.Value.X * ratio - _scaleCenter.Value.X;
            var marginTop = _scaleCenter.Value.Y * ratio - _scaleCenter.Value.Y;

            // вычисление полей
            var marginHorizontalTotal = Math.Abs(newWidth - clipWidth);
            var marginVerticalTotal = Math.Abs(newHeight - clipHeight);

            var marginRight = marginHorizontalTotal - marginLeft;
            var marginBottom = marginVerticalTotal - marginTop;

            player.Margin = new Thickness(-1.0d * marginLeft, -1.0d * marginTop, -1.0d * marginRight, -1.0d * marginBottom);
            */
        }

        #endregion
    }
}