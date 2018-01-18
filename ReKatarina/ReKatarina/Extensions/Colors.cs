using System.Drawing;

namespace ReKatarina.Extensions
{
    internal static class Colors
    {
        public static Color SetTransparency(this Color color, int a)
        {
            return Color.FromArgb(a, color.R, color.G, color.B);
        }
    }
}
