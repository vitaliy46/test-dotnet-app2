using System.ComponentModel;

namespace aiPeopleTracker.Business.Api.Constants
{
    /// <summary>Состояние камеры</summary>
    public enum CameraState : int
    {
        [Description("Работает")]
        Active = 1,

        [Description("Не работает")]
        InActive = 2
    }
}