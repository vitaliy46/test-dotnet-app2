namespace aiPeopleTracker.Dal.Api.Dto
{
    /// <summary>Настройки камеры</summary>
    public class CameraSettingsDto : DtoBase<int>
    {
        public int CameraId { get; set; }
        public virtual  CameraDto Camera { get; set; }

        public int? FloorPlanId { get; set; }
        public virtual FloorPlanDto FloorPlan { get; set; }

        public string SourceAddress { get; set; }

        public int? X { get; set; }

        public int? Y { get; set; }

        public bool IsActive { get; set; }
    }
}