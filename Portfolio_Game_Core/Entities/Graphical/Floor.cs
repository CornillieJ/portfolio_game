using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Graphical;

public class Floor : GameObject, IVisible
{
    public static Texture2D Texture { get; set; }
    public static int FloorWidth => 800;
    public static int FloorHeight => 480;

    public Floor(int x, int y)
    {
        Width = FloorWidth;
        Height = FloorHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0,0,Width,Height);
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