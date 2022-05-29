namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>Привязка камеры к шаблону</summary>
    public class LayoutTemplateCameraLink : EntityBase<int>
    {
        private int _layoutTemplateId;
        public int LayoutTemplateId
        {
            get { return _layoutTemplateId; }
            set { SetField(ref _layoutTemplateId, value); }
        }

        private int _cameraId;
        public int CameraId
        {
            get { return _cameraId; }
            set { SetField(ref _cameraId, value); }
        }

        private Camera _camera;
        public Camera Camera
        {
            get { return _camera; }
            set { SetField(ref _camera, value); }
        }
        
        /// <summary>
        /// Позиция камеры в сетке шаблона по горизонтали относительно левой стороны
        /// </summary>
        private int _x;
        public int X
        {
            get { return _x; }
            set { SetField(ref _x, value); }
        }

        /// <summary>
        /// Позиция камеры в сетке шаблона по вертикали относительно верха
        /// </summary>
        private int _y;
        public int Y
        {
            get { return _y; }
            set { SetField(ref _y, value); }
        }
    }
}