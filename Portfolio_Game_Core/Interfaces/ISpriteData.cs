

using Microsoft.Xna.Framework;

namespace Portfolio_Game_Core.Interfaces;

public interface ISpriteData
{
    int SpriteCount { get; set; }
    public Rectangle[] UpSprites { get; } 
    public Rectangle[] RightSprites { get; }
    public Rectangle[] DownSprites { get; }
    public Rectangle[] LeftSprites { get; }
}