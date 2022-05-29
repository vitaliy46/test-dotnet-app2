using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace aiPeopleTracker.Wpf.Controls.Helpers
{
    public static class ControlTemplateHelper
    {
        public static List<Control> GetChildren(DependencyObject parent)
        {
            var list = new List<Control>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Control)
                {
                    list.Add(child as Control);
                }

                list.AddRange(GetChildren(child));
            }

            return list;
        }
    }
}