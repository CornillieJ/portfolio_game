
using Microsoft.Xna.Framework.Graphics;

namespace Portfolio_Game_Core.Interfaces;
public interface IGameObject
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public Texture2D PlayerTexture { get; }
}