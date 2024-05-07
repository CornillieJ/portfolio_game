﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core;

public class Player: GameObject, IMovable
{
    public float Speed { get; set; } = 150;
    private int _spriteChangeSpeed = 10;
    private int _spriteNumber;
    public PlayerState PlayerState { get; set; }
    public Direction Direction { get; set; }

    public Player()
    {
        Height = 64;
        Width = 32;
        PlayerState = PlayerState.Neutral;
        Direction = Direction.Down;
        CurrentSprite = SpriteData.DownSprites[0];
        _spriteNumber = 0;
    }
    public Player(int x, int y): this()
    {
        PositionX = x;
        PositionY = y;
    }
    public Player(Texture2D texture, int x, int y): this(x,y)
    {
        Texture = texture;
    }

    public void GoRight(float deltaTime, bool canMove)
    {
        if(canMove)
            PositionX += (int) (Speed * deltaTime);
        if (Direction == Direction.Right)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = SpriteData.RightSprites[_spriteNumber/_spriteChangeSpeed];
        Direction = Direction.Right;
    }

    public void GoLeft(float deltaTime, bool canMove)
    {
        if(canMove)
            PositionX -= (int) (Speed * deltaTime);
        if (Direction == Direction.Left)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = SpriteData.LeftSprites[_spriteNumber/_spriteChangeSpeed];
        Direction = Direction.Left;
    }

    public void GoUp(float deltaTime, bool canMove)
    {
        if(canMove)
            PositionY -= (int) (Speed * deltaTime);
        if (Direction == Direction.Up)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = SpriteData.UpSprites[_spriteNumber/_spriteChangeSpeed];
        Direction = Direction.Up;
    }

    public void GoDown(float deltaTime, bool canMove)
    {
        if(canMove)
            PositionY += (int) (Speed * deltaTime);
        if (Direction == Direction.Down)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = SpriteData.DownSprites[_spriteNumber/_spriteChangeSpeed];
        Direction = Direction.Down;
    }
}
