using System;
using System.Windows;
using System.Windows.Controls;

namespace aiPeopleTracker.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for FloorControl.xaml
    /// </summary>
    public partial class FloorControl : UserControl
    {
        #region properties

        public static readonly DependencyProperty FloorImgProperty = DependencyProperty.Register("FloorImg", typeof(string), typeof(TextBlock),
          new FrameworkPropertyMetadata(default(string)));

        public byte[] FloorImg
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion properties

        public FloorControl()
        {
            InitializeComponent();
        }
    }
}
