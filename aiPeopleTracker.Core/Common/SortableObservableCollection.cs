using System;
using System.Collections.ObjectModel;
using System.Linq;
using aiPeopleTracker.Core.Constants;

namespace aiPeopleTracker.Core.Common
{
    public class SortableObservableCollection<T> : ObservableCollection<T>
    {
        public void Sort<TKey>(Func<T, TKey> sortFunc, SortDirection sortDirection = SortDirection.Asc)
        {
            var sortedList = (sortDirection == SortDirection.Asc ? 
                Items.OrderBy(sortFunc) : 
                Items.OrderByDescending(sortFunc)).ToList();

            for (int i = 0; i < sortedList.Count; ++i)
            {
                var actualItemIndex = Items.IndexOf(sortedList[i]);

                if (actualItemIndex != i)
                {
                    Move(actualItemIndex, i);
                }
            }
        }

        
    }
}