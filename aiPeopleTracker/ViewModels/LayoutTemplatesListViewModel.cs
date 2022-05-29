using aiPeopleTracker.Business.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Filters;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Core.Common;
using aiPeopleTracker.Core.Constants;

namespace aiPeopleTracker.ViewModels
{
    public class LayoutTemplatesListViewModel : ViewModelBase
    {
        #region Конструктор

        public event EntitySelectedHandler EntitySelected;

        private readonly ILayoutTemplateCrudService  _layoutTemplateCrudService;

        private readonly ICameraAppService  _cameraAppService;

        private readonly ICameraCrudService _cameraCrudService;

        public LayoutTemplatesListViewModel(ILayoutTemplateCrudService layoutTemplateCrudService,
                                            ICameraAppService cameraAppService, 
                                            ICameraCrudService cameraCrudService)
        {
            _layoutTemplateCrudService = layoutTemplateCrudService ?? throw new ArgumentNullException(nameof(layoutTemplateCrudService));
            _cameraAppService = cameraAppService ?? throw new ArgumentNullException(nameof(cameraAppService));
            _cameraCrudService = cameraCrudService ?? throw new ArgumentNullException(nameof(cameraCrudService));

            SortFields = EnumsHelper.MakeEnumItemsList<LayoutTemplateSortField>();
            LayoutTemplates = _layoutTemplateCrudService.GetList(new LayoutTemplateFilter());
            SelectedSortField = SortFields.First().Id;

            СamerasByStates = _cameraAppService.GetCamerasCountByStates();
            InactiveCameras = _cameraCrudService.GetList(new CameraFilter {State = CameraState.InActive});
        }

        #endregion

        #region Свойства

        #region Шаблоны

        private ObservableCollection<ListItemSimple> _sortFields;

        /// <summary>Поля для сортировки шаблонов</summary>
        public ObservableCollection<ListItemSimple> SortFields
        {
            get { return _sortFields; }
            set { SetField(ref _sortFields, value); }
        }

        private int _selectedSortField;

        /// <summary>Выбранное поле для сортировки шаблонов</summary>
        public int SelectedSortField
        {
            get { return _selectedSortField; }
            set
            {
                var prevValue = _selectedSortField;

                SetField(ref _selectedSortField, value);

                // произошли изменения, сортируем коллекцию
                if (prevValue != value)
                {
                    SortLayouts(_selectedSortField);
                }
            }
        }

        private SortableObservableCollection<LayoutTemplate> _layoutTemplates;

        /// <summary>Шаблоны</summary>
        public SortableObservableCollection<LayoutTemplate> LayoutTemplates
        {
            get { return _layoutTemplates; }
            set { SetField(ref _layoutTemplates, value); }
        }

        private LayoutTemplate _selectedTemplate;

        /// <summary>Выбранный шаблон</summary>
        public LayoutTemplate SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                SetField(ref _selectedTemplate, value);

                if (_selectedTemplate != null)
                {
                    EntitySelected?.Invoke(_selectedTemplate);
                }
            }
        }

        #endregion

        #region Камеры

        private CamerasByStates _camerasByStates;

        /// <summary>Количества камер по состояниям</summary>
        public CamerasByStates СamerasByStates
        {
            get { return _camerasByStates; }
            set { SetField(ref _camerasByStates, value); }
        }

        private SortableObservableCollection<Camera> _inactiveCameras;

        /// <summary>Неактивные камеры</summary>
        public SortableObservableCollection<Camera> InactiveCameras
        {
            get { return _inactiveCameras; }
            set { SetField(ref _inactiveCameras, value); }
        }

        #endregion

        #endregion

        #region Методы

        // сортировка шаблонов
        private void SortLayouts(int sortField)
        {
            var enumValue = (LayoutTemplateSortField)sortField;

            switch (enumValue)
            {
                case LayoutTemplateSortField.Name:
                    {
                        LayoutTemplates.Sort(l => l.Name);
                        break;
                    }
                case LayoutTemplateSortField.Usage:
                    {
                        LayoutTemplates.Sort(l => l.UsageIndex, SortDirection.Desc);
                        break;
                    }
                case LayoutTemplateSortField.CreateDate:
                    {
                        LayoutTemplates.Sort(l => l.CreateDate);
                        break;
                    }
                case LayoutTemplateSortField.UpdateDate:
                    {
                        LayoutTemplates.Sort(l => l.UpdateDate);
                        break;
                    }
            }
        }

        #endregion
    }
}