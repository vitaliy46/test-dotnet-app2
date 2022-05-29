using System.Windows;

namespace aiPeopleTracker.Wpf.Controls.WatermarkSearchTextbox
{
    public delegate void TextSearchEventHandler(object sender, TextRoutedEventArgs e);

    public class TextRoutedEventArgs : RoutedEventArgs
    {
        private string _text;

        public string Text => _text;

        internal TextRoutedEventArgs(string text)
        {
            _text = text;
        }
    }
}