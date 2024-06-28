using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class InvisibleWall:GameObject
{
    public InvisibleWall(int x, int y, int width = 32, int height = 32)
    {
        PositionX = x;
        PositionY = y;
        Width = width;
        Height = height;
    }
    public InvisibleWall(float x, float y, float width = 32, float height = 32)
    {
        PositionX = x;
        PositionY = y;
        Width = (int)width;
        Height = (int)height;
    }
}