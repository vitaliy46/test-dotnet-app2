using aiPeopleTracker.Views;
using aiPeopleTracker.Wpf.Controls.Players.Model;
using aiPeopleTracker.Wpf.Controls.WatermarkSearchTextbox;
using NLog;
using System;
using System.Windows;
using System.Windows.Controls;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using Unity;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.ViewModels;

namespace aiPeopleTracker
{
    public partial class MainWindow : Window
    {
        #region Конструкторы

        private readonly IUnityContainer _container;

        private readonly ILogger _logger;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IUnityContainer container) : this()
        {
            _container = container;

            _logger = _container.Resolve<ILogger>();

            var mainView = _container.Resolve<LayoutTemplatesListView>();

            //mainView.EntityViewOpen += MainWindow_ViewOpen;
            mainView.ViewOpen += MainWindow_ViewOpen;

            viewContainer.Children.Add(mainView);
        }

        #endregion

        #region Меню

        private void MainMenu_Selected(object sender, RoutedEventArgs e)
        {
            UIElement currentView = null;

            if ((viewContainer.Children != null) && (viewContainer.Children.Count > 0))
            {
                currentView = viewContainer.Children[0];
            }

            var viewChangeAllowed = true;

            if (currentView != null)
            {
                if (currentView is IViewBase)
                {
                    viewChangeAllowed = ((IViewBase)currentView).CanClose();
                }
            }

            if (viewChangeAllowed)
            {
                if (currentView is IDisposable)
                {
                    ((IDisposable)currentView).Dispose();
                }

                viewContainer.Children.Remove(currentView);

                var menuItem = mnuMain.SelectedItem;

                UserControl newView = null;

                switch (menuItem.Tag?.ToString())
                {
                    case "miMain":
                        {
                            newView = _container.Resolve<LayoutTemplatesListView>();
                            break;
                        };
                    case "miPlans":
                        {
                            newView = new PlansView();
                            break;
                        };
                    case "miPeople":
                        {
                            newView = new PeopleView();
                            break;
                        };
                    case "miSettings":
                        {
                            newView = new SettingsView();
                            break;
                        };
                    default: break;
                }

                if (newView is IViewBase)
                {
                    ((IViewBase)newView).ViewOpen += MainWindow_ViewOpen;
                }

                viewContainer.Children.Add(newView);
            }
            else
            {
                mnuMain.RestoreSelectedItem();
            }
        }

        /// <summary>
        /// Обработчик смены представления
        /// </summary>
        /// <param name="sender">Представление, которое следует закрыть</param>
        /// <param name="model">Модель, для которой требуется открыть новое представление</param>
        private void MainWindow_ViewOpen(IViewBase sender, ViewModelBase model)
        {
            if (sender.CanClose())
            {
                // сброс выбранного пункта меню
                mnuMain.DropSelection();

                // удаление представления с главной формы
                viewContainer.Children.Remove((UIElement) sender);

                if (sender is IDisposable)
                {
                    // освобождение ресурсов представления
                    ((IDisposable) sender).Dispose();
                }

                if (model is LayoutTemplateViewModel)
                {
                    var layoutTemplateView = _container.Resolve<LayoutTemplateView>();

                    layoutTemplateView.ViewOpen += MainWindow_ViewOpen;

                    layoutTemplateView.InitializeView(model);

                    viewContainer.Children.Add(layoutTemplateView);
                } else if (model is CameraViewModel)
                {
                    var cameraView = _container.Resolve<CameraView>();

                    cameraView.ViewOpen += MainWindow_ViewOpen;

                    cameraView.InitializeView(model);

                    viewContainer.Children.Add(cameraView);
                } else if (model is CameraRecognizedListViewModel)
                {
                    var cameraRecognizedListView = _container.Resolve<CameraRecognizedListView>();

                    cameraRecognizedListView.ViewOpen += MainWindow_ViewOpen;

                    cameraRecognizedListView.InitializeView(model);

                    viewContainer.Children.Add(cameraRecognizedListView);
                } else if (model is RecognizedPersonViewModel)
                {
                    var recognizedPersonView = _container.Resolve<RecognizedPersonView>();

                    recognizedPersonView.InitializeView(model as RecognizedPersonViewModel);

                    viewContainer.Children.Add(recognizedPersonView);
                } else if (model is RecognizedObjectViewModel)
                {
                    var recognizedObjectView = _container.Resolve<RecognizedObjectView>();

                    recognizedObjectView.InitializeView(model as RecognizedObjectViewModel);

                    viewContainer.Children.Add(recognizedObjectView);
                }
            }
        }

        #endregion

        #region События контролов

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        #endregion

        #region Поиск

        private void WatermarkSearchTextbox_SearchText(object sender, TextRoutedEventArgs e)
        {
            var args = e;
        }

        #endregion

        private void MainMenuItem_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
