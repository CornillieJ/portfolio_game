using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core;

public class Player: IGameObject
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public Texture2D PlayerTexture { get; }

    public Player(Texture2D playerTexture, int x, int y)
    {
        PositionX = x;
        PositionY = y;
        PlayerTexture = playerTexture;
    }

}