using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Graphical;

public class Carpet:GameObject, IVisible
{
    public static Texture2D Texture { get; set; }
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
    public Texture2D GetTexture()
    {
        return Texture;
    }
    public void SetTexture(Texture2D texture)
    {
        Texture = texture;
    }
}