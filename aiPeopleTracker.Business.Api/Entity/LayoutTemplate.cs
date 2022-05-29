using System.Collections.ObjectModel;

namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>Шаблон</summary>
    public class LayoutTemplate : EntityBase<int>
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }

        /// <summary>
        /// Количество камер по горизонтали
        /// </summary>
        private int _itemsX;
        public int ItemsX
        {
            get { return _itemsX; }
            set { SetField(ref _itemsX, value); }
        }

        /// <summary>
        /// Количество камер по вертикали в шаблоне
        /// </summary>
        private int _itemsY;
        public int ItemsY
        {
            get { return _itemsY; }
            set { SetField(ref _itemsY, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetField(ref _isActive, value); }
        }

        /// <summary>
        /// Показатель частоты использования шаблона
        /// </summary>
        private int _usageIndex;
        public int UsageIndex
        {
            get { return _usageIndex; }
            set { SetField(ref _usageIndex, value); }
        }

        /// <summary>
        /// Камеры связанные с шаблоном для вывода на нем изображений с 
        /// этих камер
        /// </summary>
        private ObservableCollection<LayoutTemplateCameraLink> _cameraLinks;
        public ObservableCollection<LayoutTemplateCameraLink> CameraLinks
        {
            get { return _cameraLinks; }
            set { SetField(ref _cameraLinks, value); }
        }

        public override string ToString()
        {
            return $"{Name}. {ItemsX}x{ItemsY}";
        }
    }
}