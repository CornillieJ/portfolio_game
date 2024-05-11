﻿using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class Window:GameObject
{
    private static int WindowWidth;
    private static int WindowHeight;
    public int TextMarginX = 48;
    public int TextMarginY = 48;
    public int TitleMargin = 10;
    private List<Text> _title;

    public IEnumerable<Text> Title
    {
    get => _title;
    set => _title = value.ToList();
    }

    public Window(int x, int y, string title)
    {
        Width = WindowWidth;
        Height = WindowHeight;
        PositionX = x;
        PositionY = y;
        _title = GetTitle(title);
        CurrentSprite = new Rectangle(0, 1, Width, Height);
    }
    public List<Text> GetTitle(string title)
    {
        List<Text> result = new();
        float textX = PositionX + TitleMargin; 
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
    public List<Text>? GetText(int textWindowWidth, int textWindowHeight,string content)
    {
        List<Text>? result = new();
        float textX = PositionX + TextMarginX;
        float textY = PositionY + TextMarginY;
        string[] words = content.Split(' ');
        foreach (string word in words)
        {
            if (textX + word.Length * Text.TextWidth >= PositionX + textWindowWidth - TextMarginX)
            {
                textX = PositionX + TextMarginX;
                textY += Text.TextHeight;
            }
            foreach (char c in word)
            {
                if (c == 'i') textX -= Text.TextWidth - Text.TextWidthSmall;
                if (int.TryParse(c.ToString(), out int value))
                    result.Add(new Number(textX, textY, c));
                else
                    result.Add(new Letter(textX, textY, c));
                textX += c=='i' ? Text.TextWidthSmall : Text.TextWidth;
            }
            result.Add(new Letter(textX, textY, ' ')); 
            textX += Text.TextWidth;
        }
        return result;
    }
}