using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace aiPeopleTracker.Wpf.Controls
{
    public partial class Slider : UserControl
    {
        #region Свойства зависимости

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(Slider), 
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMinimumChanged)));

        public double Minimum
        {
            get { return (double)this.slider.GetValue(System.Windows.Controls.Slider.MinimumProperty); }
            set { this.slider.SetValue(System.Windows.Controls.Slider.MinimumProperty, value); }
        }

        private static void OnMinimumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as Slider;
            if (ctrl != null)
            {
                ctrl.slider.Minimum = (double)e.NewValue;
            }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(Slider),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMaximumChanged)));

        public double Maximum
        {
            get { return (double)this.slider.GetValue(System.Windows.Controls.Slider.MaximumProperty); }
            set { this.slider.SetValue(System.Windows.Controls.Slider.MaximumProperty, value); }
        }

        private static void OnMaximumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as Slider;
            if (ctrl != null)
            {
                ctrl.slider.Maximum = (double)e.NewValue;
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(Slider),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnValueChanged)));

        public double Value
        {
            get { return (double)this.slider.GetValue(System.Windows.Controls.Slider.ValueProperty); }
            set { this.slider.SetValue(System.Windows.Controls.Slider.ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as Slider;
            if (ctrl != null)
            {
                ctrl.slider.Value = (double)e.NewValue;
            }
        }

        public static readonly DependencyProperty TickFrequencyProperty = DependencyProperty.Register("TickFrequency", typeof(double), typeof(Slider),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTickFrequencyChanged)));

        public double TickFrequency
        {
            get { return (double)this.slider.GetValue(System.Windows.Controls.Slider.TickFrequencyProperty); }
            set { this.slider.SetValue(System.Windows.Controls.Slider.TickFrequencyProperty, value); }
        }

        private static void OnTickFrequencyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = sender as Slider;
            if (ctrl != null)
            {
                ctrl.slider.TickFrequency = (double)e.NewValue;
            }
        }

        #endregion

        #region События

        public static readonly RoutedEvent SliderValueChangedEvent = EventManager.RegisterRoutedEvent("SliderValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(Slider));
        
        public event RoutedPropertyChangedEventHandler<double> SliderValueChanged
        {
            add { AddHandler(SliderValueChangedEvent, value); }
            remove { RemoveHandler(SliderValueChangedEvent, value); }
        }

        #endregion

        #region Конструктор

        public Slider()
        {
            InitializeComponent();
        }

        #endregion

        #region Обработчики событий

        private void Minus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            slider.Value -= slider.TickFrequency;
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            slider.Value += slider.TickFrequency;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var args = new RoutedPropertyChangedEventArgs<double>(e.OldValue, e.NewValue);

            args.RoutedEvent = SliderValueChangedEvent;
            RaiseEvent(args);
        }

        #endregion
    }
}