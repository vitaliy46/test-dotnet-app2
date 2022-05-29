using System;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Api.Entity
{
    /// <summary>
    /// Базовый объект предметной области
    /// </summary>
    public abstract  class EntityBase<TKey> : NotifyPropertyChanged
    {
        private TKey _id;
        public TKey Id
        {
            get { return _id; }
            set { SetField(ref _id, value); }
        }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { SetField(ref _createDate, value); }
        }

        private DateTime? _updateDate;
        public DateTime? UpdateDate
        {
            get { return _updateDate; }
            set { SetField(ref _updateDate, value); }
        }    
    }
}