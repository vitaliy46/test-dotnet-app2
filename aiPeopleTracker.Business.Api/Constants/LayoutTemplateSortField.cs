using System.ComponentModel;

namespace aiPeopleTracker.Business.Api.Constants
{
    public enum LayoutTemplateSortField : int
    {
        [Description("наименованию")]
        Name = 1,

        [Description("частоте использования")]
        Usage = 2,

        [Description("дате создания")]
        CreateDate = 3,

        [Description("дате обновления")]
        UpdateDate = 4
    }
}