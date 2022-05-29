using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aiPeopleTracker.Wpf.Api
{
    /// <summary>
    /// Позволяет обобщить два окна просмотра на разных вьюхах
    /// </summary>
    public interface IMultitimelineWindowControl
    {
        DateTime LeftDate { get; }

        DateTime RightDate { get; }

        TimeSpan Duration { get; }
    }
}
