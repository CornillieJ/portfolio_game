using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Font;

public class Number:Text
{
    
    public static int NumberWidth { get; set; } = 16;
    public static int NumberHeight { get; set; } = 17;
    private int _numbersStart = 430;
    public static Texture2D Texture { get; set; }

    public Number(int x, int y, char number)
    {
        Width = NumberWidth;
        Height = NumberHeight;
        PositionX = x;
        PositionY = y;
        int index = int.Parse(number.ToString());
        CurrentSprite = new Rectangle(_numbersStart + index%3 * NumberWidth, index/3 * NumberHeight, Width, Height);
    }
}