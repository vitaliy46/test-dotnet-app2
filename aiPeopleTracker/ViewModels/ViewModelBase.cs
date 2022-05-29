using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.ViewModels
{
    public delegate void EntitySelectedHandler(object entity);

    public class ViewModelBase : NotifyPropertyChanged
    {
        /// <summary>Событие выбора сущности</summary>
        event EntitySelectedHandler EntitySelected;
    }
}