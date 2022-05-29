using aiPeopleTracker.Wpf.Controls.Players.Model;
using System.Windows;

namespace aiPeopleTracker.Wpf.Controls.Players
{
    public delegate void PlayerDoubleClickEventHandler(object sender, PlayerDoubleClickEventArgs e);

    public class PlayerDoubleClickEventArgs : RoutedEventArgs
    {
        private PlayerDataContext _data;

        public PlayerDataContext Data => _data;

        internal PlayerDoubleClickEventArgs(PlayerDataContext data)
        {
            _data = data;
        }
    }
}