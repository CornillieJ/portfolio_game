using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class WindowInWall:GameObject, IVisible
{
    public static Texture2D Texture { get; set; }
    public static int WindowWidth => 31;
    public static int WindowHeight => 52;
    public WindowInWall(int x, int y)
    {
        Width = WindowWidth;
        Height = WindowHeight/2;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 0, WindowWidth, WindowHeight);
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