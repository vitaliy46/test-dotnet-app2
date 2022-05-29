using System.Windows.Media;

namespace aiPeopleTracker.Wpf.Controls.Players
{
    public interface ISnapshort
    {
        byte[] GetSnapshort(int dpi, PixelFormat pixelFormat);
    }
}