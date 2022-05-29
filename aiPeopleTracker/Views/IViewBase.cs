using aiPeopleTracker.ViewModels;

namespace aiPeopleTracker.Views
{
    public delegate void ViewOpenHandler(IViewBase sender, ViewModelBase model);

    public interface IViewBase
    {
        event ViewOpenHandler ViewOpen;

        void InitializeView(ViewModelBase entity);

        bool CanClose();
    }
}