using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core;

public class Player: GameObject, IMovable
{
    public float Speed { get; set; } = 150;
    private int _spriteChangeSpeed = 10;
    private int _spriteNumber = 0;
    public int Width { get; } = 32;
    public int Height { get; } = 64;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int Right => PositionX + Width;
    public int Left => PositionX;
    public int Top => PositionY;
    public int Bottom => PositionY + Height;
    public Texture2D PlayerTexture { get; set; }
    public PlayerState PlayerState { get; set; }
    public Direction Direction { get; set; }
    public Rectangle currentSprite { get; private set; }

    public Player()
    {
        PlayerState = PlayerState.Neutral;
        Direction = Direction.Down;
        currentSprite = SpriteData.DownSprites[0];
        _spriteNumber = 0;
    }
    public Player(int x, int y): this()
    {
        PositionX = x;
        PositionY = y;
    }
    public Player(Texture2D playerTexture, int x, int y): this(x,y)
    {
        PlayerTexture = playerTexture;
    }

    public void GoRight(float deltaTime)
    {
        PositionX += (int) (Speed * deltaTime);
        if (Direction == Direction.Right)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        currentSprite = SpriteData.RightSprites[_spriteNumber/_spriteChangeSpeed];
        Direction = Direction.Right;
    }

    public void GoLeft(float deltaTime)
    {
        PositionX -= (int) (Speed * deltaTime);
        if (Direction == Direction.Left)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        currentSprite = SpriteData.LeftSprites[_spriteNumber/_spriteChangeSpeed];
        Direction = Direction.Left;
    }

    public void GoUp(float deltaTime)
    {
        PositionY -= (int) (Speed * deltaTime);
        if (Direction == Direction.Up)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        currentSprite = SpriteData.UpSprites[_spriteNumber/_spriteChangeSpeed];
        Direction = Direction.Up;
    }

    public void GoDown(float deltaTime)
    {
        PositionY += (int) (Speed * deltaTime);
        if (Direction == Direction.Down)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        currentSprite = SpriteData.DownSprites[_spriteNumber/_spriteChangeSpeed];
        Direction = Direction.Down;
    }
}