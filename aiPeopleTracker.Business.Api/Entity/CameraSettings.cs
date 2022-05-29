namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>Настройки камеры</summary>
    public class CameraSettings : EntityBase<int>
    {
        private int _cameraId;
        public int CameraId
        {
            get { return _cameraId; }
            set { SetField(ref _cameraId, value); }
        }

        
        private string _sourceAddress;
        /// <summary>
        /// Источник видеосигнала камеры
        /// </summary>
        public string SourceAddress
        {
            get { return _sourceAddress; }
            set { SetField(ref _sourceAddress, value); }
        }

        
        private bool _isActive;
        /// <summary>
        /// Указывает на доступность камеры 
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { SetField(ref _isActive, value); }
        }

        
        private FloorPlan _floorPlan;
        /// <summary>
        /// Привязка камеры к поэтажному плану
        /// </summary>
        public FloorPlan FloorPlan
        {
            get { return _floorPlan; }
            set { SetField(ref _floorPlan, value); }
        }

        private int _floorPlanId;
        public int FloorPlanId
        {
            get { return _floorPlanId; }
            set { SetField(ref _floorPlanId, value); }
        }

        
        private int _x;
        /// <summary>
        /// Позиция камеры на поэтажном плане по горизонтали относительно левой стороны
        /// Указывется при настройке поэтажного плана пользователем системы.
        /// </summary>
        public int X
        {
            get { return _x; }
            set { SetField(ref _x, value); }
        }

        
        private int _y;
        /// <summary>
        /// Позиция камеры на поэтажном плане по вертикали относительно верха
        /// Указывется при настройке поэтажного плана пользователем системы.
        /// </summary>
        public int Y
        {
            get { return _y; }
            set { SetField(ref _y, value); }
        }
    }
}