using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class TextWindow:Window, IVisible
{
    public static int TextWindowHeight => 200;
    public static int TextWindowWidth => 720;
    public static Texture2D Texture { get; set; }
    private int _marginX = 48;
    private int _marginTitle = 10;
    private int _marginY = 48;
    public List<Text> _content;
    public List<Text> _title;
    public IEnumerable<Text> Content => _content.AsReadOnly();
    public IEnumerable<Text> Title => _title.AsReadOnly();

    public TextWindow(int x, int y, string title, string content) : base(x, y, title)
    {
        Width = TextWindowWidth;
        Height = TextWindowHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 0, Width, Height);
        _content = GetText(content);
        _title = GetTitle(title);
    }

    private List<Text> GetText(string content)
    {
        List<Text> result = new();
        int textX = PositionX + _marginX;
        int textY = PositionY + _marginY;
        string[] words = content.Split(' ');
        foreach (string word in words)
        {
            if (textX + word.Length * Text.TextWidth >= PositionX + TextWindowWidth - _marginX)
            {
                textX = PositionX + _marginX;
                textY += Text.TextHeight;
            }
            foreach (char c in word)
            {
                if (int.TryParse(c.ToString(), out int value))
                    result.Add(new Number(textX, textY, c));
                else
                    result.Add(new Letter(textX, textY, c));
                textX += Text.TextWidth;
            }
            result.Add(new Letter(textX, textY, ' ')); 
            textX += Text.TextWidth;
        }
        return result;
    }

    public List<Text> GetTitle(string title)
    {
        List<Text> result = new();
        int textX = PositionX + _marginTitle; 
        foreach (char c in title)
        {
            if (int.TryParse(c.ToString(), out int value))
                result.Add(new Number(textX, PositionY, c));
            else
                result.Add(new Letter(textX, PositionY, c));
            textX += Text.TextWidth;
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