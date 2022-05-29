using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Wpf.Api;
using Unity;

namespace aiPeopleTracker.Wpf.Controls.Multitimeline
{
    /// <summary>
    /// Элемент управления который управляет просмотром мультитаймлайна
    /// Через него можно просмаотривать полный или фрагмент мультитаймлайна
    /// масштабируя и перемещаясь вдоль временной шаклы
    /// </summary>
    public partial class MultitimelineObjectControl : UserControl, IMultitimelineWindowControl
    {
        private IScaleComponent _scaleComponent { get; set; }

        #region public properties

        public IMultitimeline Multitimeline
        {
            get
            {
                return (IMultitimeline)GetValue(MultitimelineProperty);
            }
            set
            {
                SetValue(MultitimelineProperty, value);
            }
        }
        public static readonly DependencyProperty MultitimelineProperty = DependencyProperty.Register("Multitimeline", typeof(IMultitimeline), typeof(MultitimelineObjectControl)
            , new FrameworkPropertyMetadata(default(IMultitimeline)));

        /// <summary>
        /// Левая граница окна просмотра таймлайн
        /// </summary>
        public DateTime LeftDate
        {
            get { return (DateTime)GetValue(LeftDateProperty); }
            set { SetValue(LeftDateProperty, value); }
        }
        public static readonly DependencyProperty LeftDateProperty = DependencyProperty.Register("LeftDate", typeof(DateTime), typeof(MultitimelineObjectControl),
            new FrameworkPropertyMetadata(default(DateTime)));

        /// <summary>
        /// Правая граница окна просмотра таймлайна
        /// </summary>
        public DateTime RightDate
        {
            get { return (DateTime)GetValue(RightDateProperty); }
            set { SetValue(RightDateProperty, value); }
        }
        public static readonly DependencyProperty RightDateProperty = DependencyProperty.Register("RightDate", typeof(DateTime), typeof(MultitimelineObjectControl),
            new FrameworkPropertyMetadata(default(DateTime)));

        /// <summary>
        /// Длительность отображаемого периода
        /// </summary>
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(MultitimelineObjectControl),
            new FrameworkPropertyMetadata(default(TimeSpan), new PropertyChangedCallback(OnDurationChanged)));

        /// <summary>
        /// Текущая позиция ползунка таймлайна
        /// Это свойство используется как посредник между Tracker и miltitimelineControl.Value
        /// для связывания положений двуэ тих элементов при программном и ручном 
        /// управлении того и другого
        /// </summary>
        public DateTime SliderValue
        {
            get { return (DateTime)GetValue(SliderValueProperty); }
            set
            {
                SetValue(SliderValueProperty, value);
            }
        }
        public static readonly DependencyProperty SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(DateTime), typeof(MultitimelineObjectControl),
            new FrameworkPropertyMetadata(default(DateTime), new PropertyChangedCallback((s, e) => (s as MultitimelineObjectControl)?.DrawThumbValue())));

        /// <summary>
        /// Доступность тайм-лайна для манипулирования
        /// </summary>
        public bool IsTimeLineAvailable
        {
            get { return (bool)GetValue(IsTimeLineAvailableProperty); }
            set { SetValue(IsTimeLineAvailableProperty, value); }
        }
        public static readonly DependencyProperty IsTimeLineAvailableProperty = DependencyProperty.Register("IsTimeLineAvailable", typeof(bool), typeof(MultitimelineObjectControl),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnTimeLineAvailableChanged)));

        #endregion public properties

        public MultitimelineObjectControl(IScaleComponent scaleComponent)
        {
            InitializeComponent();

            _scaleComponent = scaleComponent ?? throw new ArgumentNullException(nameof(scaleComponent));

            DrawThumbValue();
        }

        public void Initialize(IMultitimeline multitimeline)
        {
            Multitimeline = multitimeline;

            LeftDate = multitimeline.LeftEdge;
            RightDate = multitimeline.RightEdge;
            Duration = RightDate - LeftDate;

            //настройка элемента управления
            miltitimelineControl.Minimum = LeftDate.Ticks;
            miltitimelineControl.Maximum = RightDate.Ticks;
            miltitimelineControl.Value = miltitimelineControl.Minimum;

            DrawTimeLines();
            //Нанесение шкалы
            _scaleComponent.DrawScale(this, canvas);

            IsTimeLineAvailable = true;
        }

        // !!! временно закомментировал чтобы не мешало отладке
        private static void OnDurationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //var ctrl = sender as MultitimelineObjectControl;

            //if (ctrl != null)
            //{
            //    // разрешение слайдера - сотые секунды
            //    ctrl.miltitimelineControl.Maximum = ((TimeSpan)e.NewValue).TotalSeconds * 100;

            //    // перерисовка отметок
            //    ctrl.DrawTimeLineTicks();
            //}
        }

        private static void OnTimeLineAvailableChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as MultitimelineObjectControl;

            if (ctrl != null)
            {
                ctrl.miltitimelineControl.IsEnabled = (bool)e.NewValue;

                ctrl.DrawThumbValue();
            }
        }

        #region private

        /// <summary>
        /// Метод добавляет отрезки распознанных видеоклипов на таймлайн
        /// </summary>
        private void DrawTimeLines()
        {
            TimeLineGrid.RowDefinitions.Clear();
            TimeLineGrid.Children.Clear();

            TimeLineGrid.Width = this.Width;

            var borderBackgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("SliderThumb.Static.Background").ToString()));

            var groupedVideoClips = Multitimeline.VideoClips.GroupBy(c => c.Camera.Id).OrderBy(x => x.Key);

            int i = 0;
            foreach (var group in groupedVideoClips)
            {
                TimeLineGrid.RowDefinitions.Add(new RowDefinition());

                var videoClips = group.ToList();

                foreach (var item in videoClips)
                {
                    var offset = item.BeginTime - Multitimeline.LeftEdge;

                    var border = new Border
                    {
                        //CornerRadius = new CornerRadius(7.0d),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Width = TimeLineGrid.Width * ((double)item.Duration.Ticks / (double)Multitimeline.Duration.Ticks),
                        Height = 12.0d,
                        Margin = new Thickness(TimeLineGrid.Width * ((double)offset.Ticks / (double)Multitimeline.Duration.Ticks), 1, 0, 1),
                        BorderBrush = borderBackgroundBrush,
                        Background = borderBackgroundBrush
                    };


                    var textBlock = new TextBlock
                    {
                        Foreground = new SolidColorBrush(Colors.White),
                        Background = borderBackgroundBrush,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontSize = 10.0d,
                        Margin = new Thickness(0, 0, 0, 1.0d)
                    };

                    textBlock.Text = $"{item.BeginTime.Minute}:{item.BeginTime.Second}";

                    border.Child = textBlock;

                    border.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    border.Arrange(new Rect(border.DesiredSize));

                    border.SetValue(Grid.RowProperty, i);

                    TimeLineGrid.Children.Add(border);
                }

                i++;
            }
        }

        // метка текущего момента, отображаемая под вертикальной меткой
        private Border _thumbValueBorder;
        private TextBlock _thumbValueTextBlock;
        private double _textBlocksMargin = 2.0d;

        /// <summary>
        /// Вывод метки в позиции ползунка
        /// </summary>
        private void DrawThumbValue()
        {
            try
            {
                if (_thumbValueBorder == null)
                {
                    var backgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FindResource("SliderThumb.Static.Background").ToString()));

                    _thumbValueBorder = new Border
                    {
                        CornerRadius = new CornerRadius(7.0d),
                        Width = 100,
                        Margin = new Thickness(0, 0, 0, 0),
                        BorderBrush = backgroundBrush,
                        Background = backgroundBrush
                    };

                    _thumbValueTextBlock = new TextBlock
                    {
                        Foreground = new SolidColorBrush(Colors.White),
                        Background = backgroundBrush,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontSize = 10.0d,
                        Margin = new Thickness(0, 0, 0, 1.0d)
                    };

                    _thumbValueBorder.Child = _thumbValueTextBlock;

                    _thumbValueBorder.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    _thumbValueBorder.Arrange(new Rect(_thumbValueBorder.DesiredSize));

                    canvasThumbValue.Children.Add(_thumbValueBorder);
                }

                _thumbValueBorder.Visibility = miltitimelineControl.IsEnabled ? Visibility.Visible : Visibility.Collapsed;

                _thumbValueTextBlock.Text = $"{SliderValue.Day}.{SliderValue.Month}.{SliderValue.Year} {SliderValue.Hour}:{SliderValue.Minute}:{SliderValue.Second}";

                var borderX = -(_thumbValueBorder.ActualWidth / 2.0d);

                if (Multitimeline != null)
                {
                    borderX = canvasThumbValue.ActualWidth * ((double)(SliderValue - Multitimeline.LeftEdge).Ticks / (double)Multitimeline.Duration.Ticks) - (_thumbValueBorder.ActualWidth / 2.0d);
                }

                Canvas.SetLeft(_thumbValueBorder, borderX);
                Canvas.SetTop(_thumbValueBorder, canvas.Height - _thumbValueBorder.ActualHeight - _textBlocksMargin);
            }
            catch
            {

            }
        }


        #endregion private

        #region controlls handlers

        public delegate void ThumbPositionChangedHandler(DateTime position);

        /// <summary>
        /// Событие, изменение  положения ползунка мультитаймлайна
        /// </summary>
        public event ThumbPositionChangedHandler ThumbPositionChanged;

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Multitimeline != null)
            {
                this.SliderValue = new DateTime((long)miltitimelineControl.Value);
                ThumbPositionChanged?.Invoke(SliderValue);
            }
        }
        
        /// <summary>
        /// Рассчитывается положение полунка в относительных величинах
        /// для расположения его на экране  
        /// </summary>
        /// <param name="position"></param>
        public void PositionChangeHandler(DateTime position)
        {
            //TODO Если бы процесс перемещения курсора работал в отдельном потоке
            // то тут была бы ошибка. Соответсвенно правильно было бы вызывать 
            // ниже закомментированным кодом.
            // После перевода процесса  перемещения в отдельный поток заменить этот код на 
            // закомменитрованный
            SliderValue = position;
            miltitimelineControl.Value = position.Ticks;

            //Обращение из  фонового потока к UI компонентам
            //Dispatcher.Invoke(() =>
            //{
            //    SliderValue = position;
            //    miltitimelineControl.Value = position.Ticks;
            //});
        }

        public void PlayableVideoClipsCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        #endregion events
    }
}
