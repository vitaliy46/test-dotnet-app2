using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aiPeopleTracker.Business.Api.Constants
{
    ///Масштабные коэф-ты для таймлайна
    public enum TimeLineScales
    {
        [Description("x0.1")]
        x0_1 = -9,
        [Description("x0.2")]
        x0_2 = -8,
        [Description("x0.3")]
        x0_3 = -7,
        [Description("x0.4")]
        x0_4 = -6,
        [Description("x0.5")]
        x0_5 = -5,
        [Description("x0.6")]
        x0_6 = -4,
        [Description("x0.7")]
        x0_7 = -3,
        [Description("x0.8")]
        x0_8 = -2,
        [Description("x0.9")]
        x0_9 = -1,
        [Description("x1")]
        x1 = 1,
        [Description("x1.2")]
        x1_2 = 2,
        [Description("x1.3")]
        x1_3 = 3,
        [Description("x1.4")]
        x1_4 = 4,
        [Description("x1.5")]
        x1_5 = 5,
        [Description("x1.6")]
        x1_6 = 6,
        [Description("x1.7")]
        x1_7 = 7,
        [Description("x1.8")]
        x1_8 = 8,
    }
}
