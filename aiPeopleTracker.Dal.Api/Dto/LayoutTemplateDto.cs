namespace aiPeopleTracker.Dal.Api.Dto
{
    /// <summary>Шаблон</summary>
    public class LayoutTemplateDto : DtoBase<int>
    {
        public string Name { get; set; }

        public int ItemsX { get; set; }

        public int ItemsY { get; set; }

        public bool IsActive { get; set; }

        public int UsageIndex { get; set; }

        //public virtual IList<LayoutTemplateCameraLinkDto> CameraLinks { get; set; }
    }
}