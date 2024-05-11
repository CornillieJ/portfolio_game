
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
    public float Right => PositionX + Width - HitBoxMargin;
    public float HitBoxMargin { get; protected set; } = 5;

    public float Left => PositionX + HitBoxMargin;
    public float Top => PositionY + HitBoxMargin;
    public float Bottom => PositionY + Height- HitBoxMargin;
    public Vector2 Middle => new(PositionX+Width/2, PositionY + Height/2);
    
}