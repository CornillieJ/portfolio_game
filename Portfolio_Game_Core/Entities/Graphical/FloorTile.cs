using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Graphical;

public class FloorTile : GameObject, IVisible
{
    public static Texture2D Texture { get; set; }
    public static int FloorWidth => 32;
    public static int FloorHeight => 32;

    public FloorTile(int x, int y)
    {
        Width = FloorWidth;
        Height = FloorHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0,Height,Width,Height);
    }
    public Texture2D GetStaticTexture()
    {
        return Texture;
    }
    public void SetStaticTexture(Texture2D texture)
    {
        Texture = texture;
    }
}