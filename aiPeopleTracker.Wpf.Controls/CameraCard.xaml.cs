using System;
using System.Windows;
using System.Windows.Controls;

namespace aiPeopleTracker.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for CameraCard.xaml
    /// </summary>
    public partial class CameraCard : UserControl
    {
        #region properties

        public static readonly DependencyProperty CameraNameProperty = DependencyProperty.Register("CameraName", typeof(string), typeof(TextBlock),
           new FrameworkPropertyMetadata(default(string)));

        public string CameraName
        {
            get { return (string)this.TbName.GetValue(System.Windows.Controls.TextBlock.TextProperty); }
            set { this.TbName.SetValue(System.Windows.Controls.TextBlock.TextProperty, value); }
        }

        public static readonly DependencyProperty CameraLocationProperty = DependencyProperty.Register("CameraLocation", typeof(string), typeof(TextBlock),
            new FrameworkPropertyMetadata(default(string)));

        public string CameraLocation
        {
            get { return (string)this.TbLocation.GetValue(System.Windows.Controls.TextBlock.TextProperty); }
            set { this.TbLocation.SetValue(System.Windows.Controls.TextBlock.TextProperty, value); }
        }

        public static readonly DependencyProperty DateTimeProperty = DependencyProperty.Register("CurrentDateTime", typeof(DateTime), typeof(TextBlock),
            new FrameworkPropertyMetadata(default(DateTime)));

        public DateTime CurrentDateTime
        {
            get
            {
                DateTime res = DateTime.Now;
                DateTime.TryParse((string)this.TbLocation.GetValue(System.Windows.Controls.TextBlock.TextProperty), out res);
                return res;
            }
            set { this.TbLocation.SetValue(System.Windows.Controls.TextBlock.TextProperty, value.ToString()); }
        }

        #endregion properties
        public CameraCard()
        {
            InitializeComponent();
        }
    }
}
