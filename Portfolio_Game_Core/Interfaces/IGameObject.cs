
using Microsoft.Xna.Framework.Graphics;

namespace Portfolio_Game_Core.Interfaces;
public abstract class GameObject
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public Texture2D PlayerTexture { get; set; }
    
    public int Width { get; }
    public int Height { get; }
    public int Right => PositionX + Width;
    public int Left => PositionX;
    public int Top => PositionY;
    public int Bottom => PositionY + Height;
    
}