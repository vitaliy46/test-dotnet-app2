using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace aiPeopleTracker.Wpf.Controls.WatermarkSearchTextbox
{
    public partial class WatermarkSearchTextbox : UserControl
    {
        #region Свойства зависимости

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(WatermarkSearchTextbox), new PropertyMetadata("Введите ваш запрос"));

        #endregion

        #region События

        public static readonly RoutedEvent SearchTextEvent = EventManager.RegisterRoutedEvent("SearchText", RoutingStrategy.Bubble, typeof(TextSearchEventHandler), typeof(WatermarkSearchTextbox));

        public event TextSearchEventHandler SearchText
        {
            add { AddHandler(SearchTextEvent, value); }
            remove { RemoveHandler(SearchTextEvent, value); }
        }

        #endregion

        #region Конструктор

        public WatermarkSearchTextbox()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        #endregion

        #region Обработчики

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseSearchTextEvent();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RaiseSearchTextEvent();
            }
        }

        private void RaiseSearchTextEvent()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var args = new TextRoutedEventArgs(txtSearch.Text);
                args.RoutedEvent = SearchTextEvent;
                RaiseEvent(args);

                txtSearch.Clear();
            }
        }

        #endregion
    }
}
