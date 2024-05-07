using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Graphical;

public class Carpet:GameObject
{
    public static int carpetWidth = 94;
    public static int carpetHeight = 94;
    public Carpet(int x, int y)
    {
        Width = carpetWidth;
        Height = carpetHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0,226,Width,Height);
    }
}