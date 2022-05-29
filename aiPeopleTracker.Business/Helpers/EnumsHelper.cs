using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Helpers
{
    public static class EnumsHelper
    {
        public static string GetDescription(this object value)
        {
            var f = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])f.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static ObservableCollection<ListItemSimple> MakeEnumItemsList<T>() where T : struct, IConvertible
        {
            var result = new ObservableCollection<ListItemSimple>();

            var values = Enum.GetValues(typeof(T));

            foreach (var value in values)
            {
                result.Add(new ListItemSimple
                {
                    Id = (int)value,
                    Name = value.GetDescription()
                });
            }

            return result;
        }

    }
}
