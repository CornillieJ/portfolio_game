using Microsoft.Xna.Framework.Graphics;

namespace Portfolio_Game_Core.Interfaces;

public interface IVisible
{
    public Texture2D GetStaticTexture();
    public void SetStaticTexture(Texture2D texture);
}