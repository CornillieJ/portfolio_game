
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
    public float HitBoxMargin { get; protected set; } = 5;
    
    public float VisionXMarginOverride { get; set; } = 0;
    public float VisionYMarginOverride { get; set; } = 0;

    public float Left => PositionX + HitBoxMargin;
    public float Right => PositionX + Width - HitBoxMargin;
    public float InteractRight => PositionX + Width - HitBoxMargin - VisionXMarginOverride;
    public float InteractLeft => PositionX + HitBoxMargin + VisionXMarginOverride;
    public float Top => PositionY + HitBoxMargin;
    public float Bottom => PositionY + Height- HitBoxMargin;
    public float InteractBottom => PositionY + Height - HitBoxMargin - VisionYMarginOverride;
    public double InteractTop => PositionY + HitBoxMargin + VisionYMarginOverride;

    public Vector2 Middle => new(PositionX+Width/2, PositionY + Height/2);
}