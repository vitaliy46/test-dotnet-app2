﻿using System;
using System.Windows.Data;
using System.Windows;

namespace aiPeopleTracker.Wpf.Controls.WatermarkSearchTextbox
{
    public class TextInputToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((values[0] is bool) && (values[1] is bool))
            {
                var hasText = !(bool)values[0];
                var hasFocus = (bool)values[1];

                if (hasFocus || hasText)
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
