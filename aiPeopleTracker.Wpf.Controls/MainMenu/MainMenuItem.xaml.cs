using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace aiPeopleTracker.Wpf.Controls.MainMenu
{
    public partial class MainMenuItem : UserControl
    {
        #region Свойства зависимости

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MainMenuItem), new PropertyMetadata("Menu item title"));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(MainMenuItem), new PropertyMetadata(false));

        #endregion

        #region События

        public static readonly RoutedEvent SelectedEvent =
            EventManager.RegisterRoutedEvent("Selected", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(MainMenuItem));

        public event RoutedEventHandler Selected
        {
            add { AddHandler(SelectedEvent, value); }
            remove { RemoveHandler(SelectedEvent, value); }
        }

        #endregion

        public MainMenuItem()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private void ctrlMainMenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = !this.IsSelected;
            RaiseEvent(new RoutedEventArgs(MainMenuItem.SelectedEvent));
        }
    }
}