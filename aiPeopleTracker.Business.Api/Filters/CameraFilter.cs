using aiPeopleTracker.Business.Api.Constants;

namespace aiPeopleTracker.Business.Api.Filters
{
    public class CameraFilter : FilterBase
    {
        public int? LayoutTemplateId { get; set; }

        public CameraState? State { get; set; }
    }
}
