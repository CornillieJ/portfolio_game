
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Portfolio_Game_Core.Interfaces;
public abstract class GameObject
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int Width { get; internal set; }
    public int Height { get; internal set; }
    public Rectangle CurrentSprite { get; protected set; }
    public int Right => PositionX + Width;
    public int Left => PositionX;
    public int Top => PositionY;
    public int Bottom => PositionY + Height;
    
}