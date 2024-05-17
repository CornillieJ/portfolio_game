using System.Net.Mime;
using System.Threading.Tasks.Dataflow;
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
    private List<Text>? _content;
    public IEnumerable<Text> Content => _content.AsReadOnly();
    public List<Text> Next { get; set; }
    public TextWindow(float x, float y, string title, string content) : base(x, y, title)
    {
        TextMarginX = 48;
        TextMarginY = 48;
        Width = TextWindowWidth;
        Height = TextWindowHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 0, Width, Height);
        _content = GetText(TextWindowWidth,TextWindowHeight,content);
        Next = GetNextText("SPACE >>");
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