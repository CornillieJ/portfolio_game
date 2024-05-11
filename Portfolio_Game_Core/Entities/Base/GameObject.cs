
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Portfolio_Game_Core.Interfaces;
public abstract class GameObject
{
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public int Width { get; internal set; }
    public int Height { get; internal set; }
    public Rectangle CurrentSprite { get; protected set; }
    public float Right => PositionX + Width;
    public float Left => PositionX;
    public float Top => PositionY;
    public float Bottom => PositionY + Height;
    public Vector2 Middle => new(PositionX+Width/2, PositionY + Height/2);
    
}