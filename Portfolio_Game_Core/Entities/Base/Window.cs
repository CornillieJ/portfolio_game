using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class Window:GameObject
{
    private static int WindowWidth;
    private static int WindowHeight;
    public string Title { get; }

    public Window(int x, int y, string title)
    {
        Width = WindowWidth;
        Height = WindowHeight;
        PositionX = x;
        PositionY = y;
        Title = title;
        CurrentSprite = new Rectangle(0, 1, Width, Height);
    }
}