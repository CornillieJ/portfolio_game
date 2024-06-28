using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core;

public class Player : GameObject, IMovable, IVisible
{
    // private ISpriteData PlayerSpriteData { get; set; } = new PlayerSpriteData();
    private ISpriteData PlayerSpriteData { get; set; } = new PlayerSpriteData();
    public static float WalkSpeed = 150;
    public static float RunSpeed = 250;
    public static int PlayerWidth { get; set; } = 32;
    public static int PlayerHeight { get; set; } = 64;
    public static Texture2D Texture { get; set; }
    public float Speed { get; set; } = WalkSpeed;
    private int _spriteChangeSpeed = 10;
    private int _spriteNumber;
    public PlayerState PlayerState { get; set; }
    public Direction Direction { get; set; }

    public Player()
    {
        Height = PlayerHeight;
        Width = PlayerWidth;
        PlayerState = PlayerState.Neutral;
        Direction = Direction.Down;
        CurrentSprite = PlayerSpriteData.DownSprites[0];
        _spriteNumber = 0;
    }

    public Player(int x, int y) : this()
    {
        PositionX = x;
        PositionY = y;
    }

    public Player(Texture2D texture, int x, int y) : this(x, y)
    {
        Texture = texture;
    }

    public Texture2D GetTexture()
    {
        return Texture;
    }

    public void SetTexture(Texture2D texture)
    {
        Texture = texture;
    }

    public void GoRight(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral)
    {
        if (canMove)
        {
            switch (secondDirection)
            {
                case Direction.Up:
                    PositionX += (float)(Speed * 0.71 * deltaTime);
                    PositionY -= (float)(Speed * 0.71 * deltaTime);
                    break;
                case Direction.Down:
                    PositionX += (float)(Speed * 0.71 * deltaTime);
                    PositionY += (float)(Speed * 0.71 * deltaTime);
                    break;
                default:
                    PositionX += (Speed * deltaTime);
                    break;
            }
        }
        if (Direction == Direction.Right)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = PlayerSpriteData.RightSprites[_spriteNumber / _spriteChangeSpeed];
        Direction = Direction.Right;
    }

    public void GoLeft(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral)
    {
        if (canMove)
        {
            switch (secondDirection)
            {
                case Direction.Up:
                    PositionX -= (float)(Speed * 0.71 * deltaTime);
                    PositionY -= (float)(Speed * 0.71 * deltaTime);
                    break;
                case Direction.Down:
                    PositionX -= (float)(Speed * 0.71 * deltaTime);
                    PositionY += (float)(Speed * 0.71 * deltaTime);
                    break;
                default:
                    PositionX -= (Speed * deltaTime);
                    break;
            }
        }
        if (Direction == Direction.Left)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = PlayerSpriteData.LeftSprites[_spriteNumber / _spriteChangeSpeed];
        Direction = Direction.Left;
    }

    public void GoUp(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral)
    {
        if (canMove && secondDirection == Direction.Neutral)
            PositionY -= (Speed * deltaTime);
        else if (canMove)
            PositionY -= (float)(Speed * 0.71 * deltaTime);
        switch (secondDirection)
        {
            case Direction.Left:
                PositionX -= (float)(Speed * 0.71 * deltaTime);
                break;
            case Direction.Right:
                PositionX += (float)(Speed * 0.71 * deltaTime);
                break;
        }
        if (Direction == Direction.Up)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed)
            _spriteNumber = 0;
        CurrentSprite = PlayerSpriteData.UpSprites[_spriteNumber / _spriteChangeSpeed];
        Direction = Direction.Up;
    }

    public void GoDown(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral)
    {
        if (canMove && secondDirection == Direction.Neutral)
            PositionY += (Speed * deltaTime);
        else if (canMove)
            PositionY += (float)(Speed * 0.71 * deltaTime);
        switch (secondDirection)
        {
            case Direction.Left:
                PositionX -= (float)(Speed * 0.71 * deltaTime);
                break;
            case Direction.Right:
                PositionX += (float)(Speed * 0.71 * deltaTime);
                break;
        }

        if (Direction == Direction.Down)
            _spriteNumber++;
        else
            _spriteNumber = 0;
        if (_spriteNumber == 4 * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = PlayerSpriteData.DownSprites[_spriteNumber / _spriteChangeSpeed];
        Direction = Direction.Down;
    }

    public Vector2 GetVisionCoordinate(int distance = 5)
    {
        switch (Direction)
        {
            case Direction.Down:
                return new Vector2(PositionX + Width / 2, Middle.Y + distance);
            case Direction.Up:
                return new Vector2(PositionX + Width / 2, Middle.Y - distance);
            case Direction.Right:
                return new Vector2(Right + distance, PositionY + Height / 2);
            case Direction.Left:
                return new Vector2(Left - distance, PositionY + Height / 2);
            default:
                return new Vector2(0, 0);
        }
    }

    public void TurnPlayer()
    {
        switch (PlayerState)
        {
            default:
            case PlayerState.Neutral:
            case PlayerState.Down:
                CurrentSprite = PlayerSpriteData.DownSprites[0];
                break;
            case PlayerState.Up:
                CurrentSprite = PlayerSpriteData.UpSprites[0];
                break;
            case PlayerState.Right:
                CurrentSprite = PlayerSpriteData.RightSprites[0];
                break;
            case PlayerState.Left:
                CurrentSprite = PlayerSpriteData.LeftSprites[0];
                break;
        }
    }
}