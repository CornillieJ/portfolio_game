using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Graphical;

public class TopGraphic:GameObject
{
    public Texture2D Texture { get; set; } 
    public string GraphicText { get; set; }
    public TopGraphic(int x, int y, int width, int height, string graphicText)
    {
        PositionX = x;
        PositionY = y;
        Width = width;
        Height = height;
        GraphicText = graphicText;
        CurrentSprite = new Rectangle(0,0,Width,Height);
    }
}