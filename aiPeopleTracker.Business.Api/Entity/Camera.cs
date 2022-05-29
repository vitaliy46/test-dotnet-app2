using aiPeopleTracker.Business.Api.Constants;

namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>Камера</summary>
    public class Camera : EntityBase<int>
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }

        /// <summary>
        /// Какой-то номер камеры
        /// </summary>
        private string _number;
        public string Number
        {
            get { return _number; }
            set { SetField(ref _number, value); }
        }

        private CameraState _state;
        public CameraState State
        {
            get { return _state; }
            set { SetField(ref _state, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetField(ref _description, value); }
        }

        private CameraSettings _cameraSettings;
        public CameraSettings CameraSettings
        {
            get { return _cameraSettings; }
            set { SetField(ref _cameraSettings, value); }
        }

        public override string ToString()
        {
            return $"{Number}, {Name}";
        }
    }
}