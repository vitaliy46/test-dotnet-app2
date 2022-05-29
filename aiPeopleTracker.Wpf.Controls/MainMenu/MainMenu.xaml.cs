using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace aiPeopleTracker.Wpf.Controls.MainMenu
{
    public partial class MainMenu : UserControl
    {
        #region Свойства зависимости

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", 
            typeof(ObservableCollection<MainMenuItem>), 
            typeof(MainMenuItem), 
            new FrameworkPropertyMetadata(new ObservableCollection<MainMenuItem>()));

        public ObservableCollection<MainMenuItem> Items
        {
            get { return (ObservableCollection<MainMenuItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        #endregion

        #region Свойства

        /// <summary>Текущий выбранный элемент меню</summary>
        public MainMenuItem SelectedItem => Items?.FirstOrDefault(item => item.IsSelected);

        #endregion

        #region События

        public static readonly RoutedEvent SelectedEvent =
            EventManager.RegisterRoutedEvent("Selected", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(MainMenu));

        public event RoutedEventHandler Selected
        {
            add { AddHandler(SelectedEvent, value); }
            remove { RemoveHandler(SelectedEvent, value); }
        }

        #endregion

        #region Конструктор и данные

        private MainMenuItem _prevSelectedItem;

        public MainMenu()
        {
            InitializeComponent();

            this.Items.CollectionChanged += Items_CollectionChanged;
        }

        #endregion

        #region Открытые методы

        /// <summary>Восстановление предыдущего выбранного элемента меню</summary>
        public void RestoreSelectedItem()
        {
            if (this.Items != null)
            {
                foreach(var item in this.Items)
                {
                    item.IsSelected = false;
                }
            }

            if (_prevSelectedItem != null)
            {
                _prevSelectedItem.IsSelected = true;
            }
        }

        /// <summary>Сброс признака выбора пункта меню</summary>
        public void DropSelection()
        {
            if (this.Items != null)
            {
                foreach (var item in this.Items)
                {
                    item.IsSelected = false;
                }
            }
        }

        #endregion

        #region Обработчики

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    ((MainMenuItem)item).Selected -= MainMenuItem_Selected;
                }
            }

            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    ((MainMenuItem)item).Selected += MainMenuItem_Selected;
                }
            }
        }

        private void MainMenuItem_Selected(object sender, RoutedEventArgs e)
        {
            var selectedCurr = (MainMenuItem)sender;
            var selectedPrev = Items.FirstOrDefault(item => (item != selectedCurr) && item.IsSelected);

            _prevSelectedItem = selectedPrev;

            if (selectedCurr != selectedPrev)
            {
                if (selectedPrev != null)
                {
                    selectedPrev.IsSelected = false;
                }

                if (selectedCurr.IsSelected)
                {
                    RaiseEvent(new RoutedEventArgs(MainMenu.SelectedEvent));
                }
                else
                {
                    selectedCurr.IsSelected = true;
                }
            }
        }

        #endregion
    }
}
