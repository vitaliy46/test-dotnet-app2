namespace aiPeopleTracker.Dal.Api.Dto
{
    public class LayoutTemplateCameraLinkDto : DtoBase<int>
    {
        public int LayoutTemplateId { get; set; }

        public int CameraId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}