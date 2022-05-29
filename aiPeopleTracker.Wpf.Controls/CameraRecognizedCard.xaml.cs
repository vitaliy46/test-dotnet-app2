using System;
using System.Windows;
using System.Windows.Controls;

namespace aiPeopleTracker.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for CameraCard.xaml
    /// </summary>
    public partial class CameraRecognizedCard : UserControl
    {
        #region properties

        //public static readonly DependencyProperty CameraNameProperty = DependencyProperty.Register("CameraName", typeof(string), typeof(TextBlock), new FrameworkPropertyMetadata(default(string)));

        //public static readonly DependencyProperty CameraLocationProperty = DependencyProperty.Register("CameraLocation", typeof(string), typeof(TextBlock), new FrameworkPropertyMetadata(default(string)));

        //public static readonly DependencyProperty DateTimeProperty = DependencyProperty.Register("CurrentDateTime", typeof(DateTime), typeof(TextBlock), new FrameworkPropertyMetadata(default(DateTime)));

        #endregion properties
        public CameraRecognizedCard()
        {
            InitializeComponent();
        }
    }
}
