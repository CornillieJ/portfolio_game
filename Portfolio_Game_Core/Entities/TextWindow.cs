using Microsoft.Xna.Framework;

namespace Portfolio_Game_Core.Entities;

public class TextWindow:Window
{
    private static int TextWindowWidth => 480;
    private static int TextWindowHeight => 144;
    public TextWindow(int x, int y, string title) : base(x, y, title)
    {
        Width = TextWindowWidth;
        Height = TextWindowHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 96, Width, Height);
    }
}