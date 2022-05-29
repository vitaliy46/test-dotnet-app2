using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace aiPeopleTracker.Wpf.Controls
{
    public partial class TimeLine : UserControl
    {
        #region Конструктор и данные

        public TimeLine()
        {
            InitializeComponent();
        }

        #endregion

        #region Свойства зависимости

        /// <summary>
        /// Дата начала отсчета
        /// </summary>
        public DateTime BaseDate
        {
            get { return (DateTime)GetValue(BaseDateProperty); }
            set { SetValue(BaseDateProperty, value); }
        }

        public static readonly DependencyProperty BaseDateProperty = DependencyProperty.Register("LeftDate", typeof(DateTime), typeof(TimeLine),
            new FrameworkPropertyMetadata(default(DateTime)));

        /// <summary>
        /// Длительность отображаемого периода
        /// </summary>
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(TimeLine),
            new FrameworkPropertyMetadata(default(TimeSpan), new PropertyChangedCallback(OnDurationChanged)));

        private static void OnDurationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as TimeLine;

            if (ctrl != null)
            {
                //// разрешение слайдера - сотые секунды
                ctrl.slider.Maximum = ((TimeSpan)e.NewValue).TotalSeconds * 100;

                // перерисовка отметок
                ctrl.DrawTimeLineTicks();
            }
        }

        /// <summary>
        /// Текущая дата таймлайна
        /// </summary>
        public DateTime? CurrentDate
        {
            get { return (DateTime?)GetValue(CurrentDateProperty); }
            set
            {
                SetValue(CurrentDateProperty, value);

                if (value != null)
                {
                    slider.Value = (value.Value - BaseDate).TotalSeconds * 100.0;
                }
                else
                {
                    slider.Value = slider.Minimum;
                }
            }
        }

        public static readonly DependencyProperty CurrentDateProperty = DependencyProperty.Register("CurrentDate", typeof(DateTime?), typeof(TimeLine),
            new FrameworkPropertyMetadata(default(DateTime?), new PropertyChangedCallback(OnCurrentDateChanged)));

        private static void OnCurrentDateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as TimeLine;

            if (ctrl != null)
            {
                ctrl.DrawThumbValue();
            }
        }

        /// <summary>
        /// Доступность тайм-лайна для манипулирования
        /// </summary>
        public bool IsTimeLineAvailable
        {
            get { return (bool)GetValue(IsTimeLineAvailableProperty); }
            set { SetValue(IsTimeLineAvailableProperty, value); }
        }

        public static readonly DependencyProperty IsTimeLineAvailableProperty = DependencyProperty.Register("IsTimeLineAvailable", typeof(bool), typeof(TimeLine),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnTimeLineAvailableChanged)));

        private static void OnTimeLineAvailableChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as TimeLine;

            if (ctrl != null)
            {
                ctrl.slider.IsEnabled = (bool)e.NewValue;

                ctrl.DrawThumbValue();
            }
        }

        #endregion

        #region Открытые методы

        // Возвращает отношение отступа ползунка слайдера от правого края таймлайна
        public double GetThumbRightMarginRatio()
        {
            // _thumbValueBorder задаем ширину 100, берем половину этого значения для вычисления отступа ползунка от правого края, чтобы его маркер не скрывался за краем таймлайна
            return 50 / canvas.ActualWidth;
        }

        #endregion

        #region События

        public static readonly RoutedEvent TimeLineValueChangedEvent = EventManager.RegisterRoutedEvent("TimeLineValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime?>), typeof(TimeLine));

        public event RoutedPropertyChangedEventHandler<DateTime?> TimeLineValueChanged
        {
            add { AddHandler(TimeLineValueChangedEvent, value); }
            remove { RemoveHandler(TimeLineValueChangedEvent, value); }
        }

        #endregion

        #region Закрытые методы

        private Brush _darkGrayColor = new SolidColorBrush(Colors.DarkGray);

        private double _startTicksMargin = 5.0d;
        private double _textBlocksMargin = 2.0d;
        private double _ticksWidth = 2.0d;

        private int _minTicksCount = 5; // минимальное кол-во отметок на шкале

        // создание текстового блока с отметкой
        private TextBlock CreateTimeLineTextBlock(string text)
        {
            var result = new TextBlock
            {
                Text = text,
                FontSize = 10,
            };

            result.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            result.Arrange(new Rect(result.DesiredSize));

            return result;
        }

        // создание линии отметки
        private Line CreateTickLine(double x1, double y1, double x2, double y2, double thickness, Brush stroke)
        {
            var result = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                StrokeThickness = thickness,
                Stroke = stroke
            };

            return result;
        }

        // добавление отметки с текстом
        private void AddTick(long tickTotalSeconds, DateTime time, Func<DateTime, string> getTickText)
        {
            var text = string.Empty;

            if (getTickText != null)
            {
                text = getTickText(time);
            }

            var textBlock = CreateTimeLineTextBlock(text);

            var tickX = canvas.ActualWidth * (tickTotalSeconds / Duration.TotalSeconds);

            // предотвращение обрезки временной метки
            if ((tickX + (textBlock.ActualWidth / 2.0) <= canvas.ActualWidth - _startTicksMargin)
                && (tickX - (textBlock.ActualWidth / 2.0) >= _startTicksMargin))
            {
                var y = canvas.ActualHeight - textBlock.ActualHeight * 2.5 - _textBlocksMargin;

                Canvas.SetLeft(textBlock, _startTicksMargin + tickX - (textBlock.ActualWidth / 2.0d));
                Canvas.SetTop(textBlock, y);
                canvas.Children.Add(textBlock);

                var lineTick = CreateTickLine(_startTicksMargin + tickX, 0, _startTicksMargin + tickX, y, 1.0, _darkGrayColor);
                canvas.Children.Add(lineTick);
            }
        }

        // вывод всех отметок таймлайна
        private void DrawTimeLineTicks()
        {
            try
            {
                canvas.Children.Clear();

                // стартовая отметка
                var textBlockStart = CreateTimeLineTextBlock($"{BaseDate.Hour.ToString().PadLeft(2, '0')}:{BaseDate.Minute.ToString().PadLeft(2, '0')}:{BaseDate.Second.ToString().PadLeft(2, '0')}");
                Canvas.SetLeft(textBlockStart, _textBlocksMargin);
                Canvas.SetTop(textBlockStart, canvas.Height - textBlockStart.ActualHeight - _textBlocksMargin);
                canvas.Children.Add(textBlockStart);

                var lineLeft = CreateTickLine(_startTicksMargin, 0, _startTicksMargin, canvas.ActualHeight - textBlockStart.ActualHeight - _textBlocksMargin, _ticksWidth, _darkGrayColor);
                canvas.Children.Add(lineLeft);

                var minutesDelimiter = 0;

                Func<DateTime, string> getTickText = null;

                var ticksRendered = false;

                // метки в формате {чч}:{мм}:{сс}
                getTickText = (d) => $"{d.Hour.ToString().PadLeft(2, '0')}:{d.Minute.ToString().PadLeft(2, '0')}:{d.Second.ToString().PadLeft(2, '0')}";

                // до 1 минуты
                if (Duration.TotalSeconds <= 60)
                {
                    var secondsDelimiter = 10;  // всегда каждые 10 секунд

                    for (var i = 0; i < Duration.TotalSeconds; i++)
                    {
                        var time = BaseDate.AddSeconds(i);

                        if (time.Second % secondsDelimiter == 0)
                        {
                            AddTick(i, time, getTickText);
                        }
                    }

                    ticksRendered = true;
                }
                // между минутой и часом
                else if (Duration.TotalMinutes <= 60)
                {
                    var itemsCount = 0;
                    minutesDelimiter = 20; // 20 / 2 = 10 минут

                    // не меньше 5 отметок на шкале
                    while (itemsCount < _minTicksCount)
                    {
                        minutesDelimiter /= 2;
                        itemsCount = (int)Duration.TotalSeconds / (minutesDelimiter * 60);
                    }
                }
                // между часом и сутками
                else if (Duration.TotalHours <= 24)
                {
                    var itemsCount = 0;
                    minutesDelimiter = 120; // 120 / 2 = 60 минут

                    // не меньше 5 отметок на шкале
                    while (itemsCount < _minTicksCount)
                    {
                        minutesDelimiter /= 2;
                        itemsCount = (int)Duration.TotalSeconds / (minutesDelimiter * 60);
                    }
                }
                // какой-то жуткий период большое суток
                else
                {
                    var itemsCount = 0;
                    var hoursDelimiter = 10; // 10 / 2 = 5 часов

                    // не меньше 5 отметок на шкале
                    while (itemsCount < _minTicksCount)
                    {
                        hoursDelimiter /= 2;
                        itemsCount = (int)Duration.TotalSeconds / (hoursDelimiter * 60 * 60);
                    }

                    // метки в формате {дд}.{ММ}.{гггг} {чч}:{мм}:{сс}
                    getTickText = (d) => $"{d.Day.ToString().PadLeft(2, '0')}.{d.Month.ToString().PadLeft(2, '0')}.{d.Year.ToString()} {d.Hour.ToString().PadLeft(2, '0'):d.Minute.ToString().PadLeft(2, '0')}:{d.Second.ToString().PadLeft(2, '0')}";

                    if (!ticksRendered)
                    {
                        for (var i = 0; i < Duration.TotalSeconds; i++)
                        {
                            var time = BaseDate.AddSeconds(i);

                            var hour = (time.Hour != 0) ? time.Hour : 24;

                            if (((hour % hoursDelimiter == 0) || ((hour + 24) % hoursDelimiter == 0)) && (time.Minute == 0))
                            {
                                AddTick(i, time, getTickText);
                                i += 60 * 60 * hoursDelimiter - 1;
                            }
                        }
                    }

                    ticksRendered = true;
                }

                // вывод отметок, если их еще нет (в случае короткого или очень длинного периода)
                if (!ticksRendered)
                {
                    for (var i = 0; i < Duration.TotalSeconds; i++)
                    {
                        var time = BaseDate.AddSeconds(i);

                        if (time.Minute % minutesDelimiter == 0)
                        {
                            AddTick(i, time, getTickText);
                            i += 60 - 1;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        // метка текущего момента, отображаемая под вертикальной меткой
        private Border _thumbValueBorder;
        private TextBlock _thumbValueTextBlock;

        // вывод метки текущего момента
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

                    canvas.Children.Add(_thumbValueBorder);
                }

                _thumbValueBorder.Visibility = slider.IsEnabled ? Visibility.Visible : Visibility.Collapsed;

                _thumbValueTextBlock.Text = CurrentDate.HasValue ? 
                    $"{CurrentDate.Value.Day.ToString().PadLeft(2, '0')}." + 
                    $"{CurrentDate.Value.Month.ToString().PadLeft(2, '0')}." + 
                    $"{CurrentDate.Value.Year.ToString()} " + 
                    $"{CurrentDate.Value.Hour.ToString().PadLeft(2, '0')}:" + 
                    $"{CurrentDate.Value.Minute.ToString().PadLeft(2, '0')}:" + 
                    $"{CurrentDate.Value.Second.ToString().PadLeft(2, '0')}" : string.Empty;

                var borderX = canvas.ActualWidth * (slider.Value / 100.0d / Duration.TotalSeconds) - (_thumbValueBorder.ActualWidth / 2.0d);

                Canvas.SetLeft(_thumbValueBorder, borderX);
                Canvas.SetTop(_thumbValueBorder, canvas.Height - _thumbValueBorder.ActualHeight - _textBlocksMargin);
            }
            catch
            {

            }
        }

        #endregion

        #region Обработчики контролов

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawTimeLineTicks();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // разрешение слайдера - сотые секунды
            this.CurrentDate = BaseDate.AddSeconds(slider.Value / 100.0d);

            var args = new RoutedPropertyChangedEventArgs<DateTime?>(this.BaseDate, this.CurrentDate);

            args.RoutedEvent = TimeLineValueChangedEvent;
            RaiseEvent(args);
        }

        #endregion
    }
}