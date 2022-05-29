using System;
using aiPeopleTracker.ViewModels;

namespace aiPeopleTracker.Views
{
    public partial class PlansView : IViewBase, IDisposable
    {
        #region Конструктор

        public event ViewOpenHandler ViewOpen;

        public PlansView()
        {
            InitializeComponent();
        }

        #endregion

        #region Реализация IViewBase

        public void SetEntity(object entity)
        {

        }

        public bool CanClose()
        {
            return true;
        }

        #endregion

        #region Реализация IDisposable

        public void Dispose()
        {
            //
        }

        public void InitializeView(ViewModelBase entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}