using aiPeopleTracker.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Startup;
using NLog;
using Unity;

namespace aiPeopleTracker.Views
{
    /// <summary>
    /// Класс для отображения списка шаблонов сетки видеопотоков с камер
    /// </summary>
    public partial class LayoutTemplatesListView : UserControl, IViewBase, IDisposable
    {
        #region Конструктор и данные

        public event ViewOpenHandler ViewOpen;

        private LayoutTemplatesListViewModel _model;

        public LayoutTemplatesListView(LayoutTemplatesListViewModel model)
        {
            InitializeComponent();

            DataContext = _model = model;

            _model.EntitySelected += Model_EntitySelected;
        }

        #endregion

        #region События контролов

        // выбран экземпляр сущности
        private void Model_EntitySelected(object entity)
        {
            var layoutTemplate = entity as LayoutTemplate;

            // выбран шаблон - необходимо вывести его представление
            if (layoutTemplate != null)
            {
                //EntityViewOpen?.Invoke(this, layoutTemplate);

                // Временно сделана инициализация UnityContainer
                var mapperConfig = MapperConfig.Create();
                var container = UnityConfig.Create(mapperConfig);

                var layoutTemplateCrudService = (ILayoutTemplateCrudService)container.Resolve(typeof(ILayoutTemplateCrudService));

                var layoutTemplateViewModel = new LayoutTemplateViewModel(layoutTemplateCrudService);
                layoutTemplateViewModel.LayoutTemplate = layoutTemplate;

                ViewOpen?.Invoke(this, layoutTemplateViewModel);
            }
        }

        private void btnInactiveCameras_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        #endregion

        #region Реализация IViewBase

        public void InitializeView(ViewModelBase model)
        {
        }

        public bool CanClose()
        {
            return true;
        }

        #endregion

        #region Реализация IDisposable

        public void Dispose()
        {
            //
        }

        #endregion
    }
}