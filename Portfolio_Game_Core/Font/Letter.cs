using Microsoft.Xna.Framework;

namespace Portfolio_Game_Core.Font;

public class Letter:Text
{
    public static int LetterWidth { get; set; } = 16;
    public static int LetterHeight { get; set; } = 32;
    public static char[] _chars = {
        'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 
        'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 
        'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 
        'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z', '.', ',', '!', '|',
        '?', '|', '#', '_', '-', '|', ':', ';', '\'', '"', ' '
    };

    public Letter(int x, int y, char letter) 
    {
        Width = LetterWidth;
        Height = LetterHeight;
        PositionX = x;
        PositionY = y;
        int index = Array.FindIndex(_chars, c => c == letter);
        CurrentSprite = new Rectangle(1+ index%26 * LetterWidth, index/26 * LetterHeight, Width, Height);
    }
}