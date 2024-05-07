﻿using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class Chest:GameObject
{
    public Chest(int x, int y)
    {
        Width = 30;
        Height = 25;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 1, Width, Height);
    }
}