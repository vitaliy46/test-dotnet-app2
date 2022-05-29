using System.ComponentModel;

namespace aiPeopleTracker.Core.Constants
{
    /// <summary>
    /// Направления сортировки
    /// </summary>
    public enum SortDirection : int
    {
        [Description("по возрастанию")]
        Asc = 1,

        [Description("по убыванию")]
        Desc = 2
    }
}