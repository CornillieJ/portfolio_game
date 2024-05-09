using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Graphical;

public class Floor : GameObject
{
    public static int FloorWidth => 32;
    public static int FloorHeight => 32;

    public Floor(int x, int y)
    {
        Width = FloorWidth;
        Height = FloorHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0,Height,Width,Height);
    }
}