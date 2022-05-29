namespace aiPeopleTracker.Dal.Api.Dto
{
    /// <summary>Камера</summary>
    public class CameraDto : DtoBase<int>
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public int State { get; set; }

        public string Description { get; set; }

        public virtual CameraSettingsDto CameraSettings { get; set; }
    }
}