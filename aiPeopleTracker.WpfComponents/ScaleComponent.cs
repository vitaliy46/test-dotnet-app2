using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using aiPeopleTracker.Wpf.Api;
using aiPeopleTracker.Wpf.Controls;
using Brush = System.Windows.Media.Brush;



namespace aiPeopleTracker.WpfComponents
{
    public class ScaleComponent : IScaleComponent
    {
        private Brush _darkGrayColor = new SolidColorBrush(Colors.DarkGray);

        private double _startTicksMargin = 5.0d;

        private double _textBlocksMargin = 2.0d;

        private double _ticksWidth = 2.0d;
        /// <summary>
        /// минимальное кол-во отметок на шкале
        /// </summary>
        private int _minTicksCount = 5;

        public ScaleScope ZoomOut(ScaleScope scaleScope)
        {
            throw new NotImplementedException();
        }

        public void DrawScale(IMultitimelineWindowControl multitimelineControl, Canvas canvas)
        {
            canvas.Children.Clear();

            // стартовая отметка
            var textBlockStart = CreateTimeLineTextBlock($"{multitimelineControl.LeftDate.Hour.ToString().PadLeft(2, '0')}:{multitimelineControl.LeftDate.Minute.ToString().PadLeft(2, '0')}:{multitimelineControl.LeftDate.Second.ToString().PadLeft(2, '0')}");
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
            if (multitimelineControl.Duration.TotalSeconds <= 60)
            {
                var secondsDelimiter = 10;  // всегда каждые 10 секунд

                for (var i = 0; i < multitimelineControl.Duration.TotalSeconds; i++)
                {
                    var time = multitimelineControl.LeftDate.AddSeconds(i);

                    if (time.Second % secondsDelimiter == 0)
                    {
                        AddTick(canvas, i, time, getTickText, multitimelineControl.Duration.TotalSeconds);
                    }
                }

                ticksRendered = true;
            }
            // между минутой и часом
            else if (multitimelineControl.Duration.TotalMinutes <= 60)
            {
                var itemsCount = 0;
                minutesDelimiter = 20; // 20 / 2 = 10 минут

                // не меньше 5 отметок на шкале
                while (itemsCount < _minTicksCount)
                {
                    minutesDelimiter /= 2;

                    if (minutesDelimiter == 1) break;

                    itemsCount = (int)multitimelineControl.Duration.TotalSeconds / (minutesDelimiter * 60);
                }
            }
            // между часом и сутками
            else if (multitimelineControl.Duration.TotalHours <= 24)
            {
                var itemsCount = 0;
                minutesDelimiter = 120; // 120 / 2 = 60 минут

                // не меньше 5 отметок на шкале
                while (itemsCount < _minTicksCount)
                {
                    minutesDelimiter /= 2;

                    if (minutesDelimiter == 1) break;

                    itemsCount = (int)multitimelineControl.Duration.TotalSeconds / (minutesDelimiter * 60);
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

                    if (hoursDelimiter == 1) break;

                    itemsCount = (int)multitimelineControl.Duration.TotalSeconds / (hoursDelimiter * 60 * 60);
                }

                // метки в формате {дд}.{ММ}.{гггг} {чч}:{мм}:{сс}
                getTickText = (d) => $"{d.Day.ToString().PadLeft(2, '0')}" +
                                     $".{d.Month.ToString().PadLeft(2, '0')}" +
                                     $".{d.Year.ToString()}" +
                                     $" {d.Hour.ToString().PadLeft(2, '0'):d.Minute.ToString().PadLeft(2, '0')}" +
                                     $":{d.Second.ToString().PadLeft(2, '0')}";

                if (!ticksRendered)
                {
                    for (var i = 0; i < multitimelineControl.Duration.TotalSeconds; i++)
                    {
                        var time = multitimelineControl.LeftDate.AddSeconds(i);

                        var hour = (time.Hour != 0) ? time.Hour : 24;

                        if (((hour % hoursDelimiter == 0) || ((hour + 24) % hoursDelimiter == 0))
                            && (time.Minute == 0))
                        {
                            AddTick(canvas, i, time, getTickText, multitimelineControl.Duration.TotalSeconds);
                            i += 60 * 60 * hoursDelimiter - 1;
                        }
                    }
                }

                ticksRendered = true;
            }

            // вывод отметок, если их еще нет (в случае короткого или очень длинного периода)
            if (!ticksRendered)
            {
                for (var i = 0; i < multitimelineControl.Duration.TotalSeconds; i++)
                {
                    var time = multitimelineControl.LeftDate.AddSeconds(i);

                    if (time.Minute % minutesDelimiter == 0)
                    {
                        AddTick(canvas, i, time, getTickText, multitimelineControl.Duration.TotalSeconds);
                        i += 60 - 1;
                    }
                }
            }
        }

        /// <summary>
        /// Cоздание текстового блока с отметкой
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
        private Line CreateTickLine(double x1, double y1,
                                    double x2, double y2,
                                    double thickness,Brush stroke)
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
        private void AddTick(Canvas canvas,long tickTotalSeconds,
                     DateTime time,Func<DateTime, string> getTickText,
                     double durationTotalSeconds)
        {
            var text = string.Empty;

            if (getTickText != null)
            {
                text = getTickText(time);
            }

            var textBlock = CreateTimeLineTextBlock(text);

            var tickX = canvas.ActualWidth * (tickTotalSeconds / durationTotalSeconds);

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
    }

}
