using Microsoft.Xna.Framework.Graphics;

namespace Portfolio_Game_Core.Interfaces;

public interface IVisible
{
    public Texture2D GetTexture();
    public void SetTexture(Texture2D texture);
}