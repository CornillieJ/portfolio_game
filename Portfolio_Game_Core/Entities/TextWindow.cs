using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class TextWindow:Window, IVisible
{
    public static int TextWindowHeight => 224;
    public static int TextWindowWidth => 720;
    public static Texture2D Texture { get; set; }
    private int _marginX = 48;
    private int _marginY = 24;
    private List<Text> _content;
    public IEnumerable<Text> Content => _content.AsReadOnly();

    public TextWindow(int x, int y, string title, string content) : base(x, y, title)
    {
        Width = TextWindowWidth;
        Height = TextWindowHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 0, Width, Height);
        _content = GetText(content);
    }

    private List<Text> GetText(string content)
    {
        List<Text> result = new();
        int positionX = _marginX;
        int positionY = _marginY;
        foreach (char c in content)
        {
            if (int.TryParse(c.ToString(), out int value))
                result.Add(new Number(positionX, positionY, c));
            else
                result.Add(new Letter(positionX,positionY,c));
            positionX += Text.TextWidth;
            if (positionX >= TextWindowWidth - _marginX)
            {
                positionX = _marginX;
                positionY += Text.TextHeight;
            }
        }
        return result;
    }
    public Texture2D GetStaticTexture()
    {
        return Texture;
    }

    public void SetStaticTexture(Texture2D texture)
    {
        Texture = texture;
    }
}