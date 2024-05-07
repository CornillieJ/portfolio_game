using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Graphical;

public class Floor:GameObject
{
    public static int floorWidth = 32;
    public static int floorHeight = 32;
    public Floor(int x, int y)
    {
        Width = floorWidth;
        Height = floorHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0,Height,Width,Height);
    }
}