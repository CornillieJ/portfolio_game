﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Font;

public  class Text:GameObject,IVisible
{
    public static int TextWidth { get; set; } = 16;
    public static int TextHeight { get; set; } = 30;
    public static Texture2D Texture { get; set; }

    public Texture2D GetStaticTexture()
    {
        return Texture;
    }
    public void SetStaticTexture(Texture2D texture)
    {
        Texture = texture;
    }
}