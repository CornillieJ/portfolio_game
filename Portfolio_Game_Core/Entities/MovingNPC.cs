using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class MovingNPC: GameObject, IMovable, IVisible, IInteractable, IHasInventory
{
    
    public bool Interacted { get; set; }
    public bool IsInteractable { get; set; } = true;
    public ResultAction[] Interactions { get; set; }
    public string GraphicText { get; set; }
    private ISpriteData NPCSpriteData { get; set; }
    public static float WalkSpeed = 75;
    public int maxSpriteCount;
    public int MoveDelay { get; set; } = 50;
    public int DelayCount { get; set; }
    public static float RunSpeed = 250;
    public static int NPCWidth { get; set; } = 32;
    public static int NPCHeight { get; set; } = 64;
    public  Texture2D Texture { get; set; }
    public float Speed { get; set; } = WalkSpeed;
    public int WalkDuration { get; set; } = 10;
    private int _spriteChangeSpeed = 5;
    private int _spriteNumber;
    public int _currentWalkDuration = 0;
    public bool isWalking= false;
    public List<GameItem> Inventory { get; set; }
    public void ShowInventory()
    {
    }

    public PlayerState PlayerState { get; set; }
    public Direction Direction { get; set; }

    public MovingNPC(string graphicText,ISpriteData spriteData)
    {
        NPCSpriteData = spriteData;
        Height = NPCHeight;
        Width = NPCWidth;
        PlayerState = PlayerState.Neutral;
        Direction = Direction.Down;
        CurrentSprite = NPCSpriteData.DownSprites[0];
        _spriteNumber = 0;
        GraphicText = graphicText;
        maxSpriteCount = spriteData.SpriteCount;
        ResultTexts = new List<(string, string)>();
        ResultMovePositions = new List<(Vector2, PlayerState)>();
        ResultDelays = new List<int>();
        ObjectAdditions = new List<GameObject>();
        Inventory = new List<GameItem>();
    }
    public MovingNPC(int x, int y,string graphicText, ISpriteData spriteData) : this(graphicText,spriteData)
    {
        PositionX = x;
        PositionY = y;
    }
    public MovingNPC(Texture2D texture, int x, int y,string graphicText, ISpriteData spriteData) : this(x, y,graphicText, spriteData)
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
        if (_spriteNumber == maxSpriteCount * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = NPCSpriteData.RightSprites[_spriteNumber / _spriteChangeSpeed];
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
        if (_spriteNumber == maxSpriteCount * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = NPCSpriteData.LeftSprites[_spriteNumber / _spriteChangeSpeed];
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
        if (_spriteNumber == maxSpriteCount * _spriteChangeSpeed)
            _spriteNumber = 0;
        CurrentSprite = NPCSpriteData.UpSprites[_spriteNumber / _spriteChangeSpeed];
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
        if (_spriteNumber == maxSpriteCount * _spriteChangeSpeed) _spriteNumber = 0;
        CurrentSprite = NPCSpriteData.DownSprites[_spriteNumber / _spriteChangeSpeed];
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

    public void TurnNPC()
    {
        switch (PlayerState)
        {
            default:
            case PlayerState.Neutral:
            case PlayerState.Down:
                CurrentSprite = NPCSpriteData.DownSprites[0];
                break;
            case PlayerState.Up:
                CurrentSprite = NPCSpriteData.UpSprites[0];
                break;
            case PlayerState.Right:
                CurrentSprite = NPCSpriteData.RightSprites[0];
                break;
            case PlayerState.Left:
                CurrentSprite = NPCSpriteData.LeftSprites[0];
                break;
        }
    }

    public void Move(Direction direction,float deltaTime, bool canMove)
    {
        if (!isWalking)
        {
            DelayCount++;
            if (DelayCount < MoveDelay) return;
            DelayCount = 0;
        }
        isWalking = true;
        switch (direction)
        {
            case Direction.Up:
                GoUp(deltaTime,canMove);
                break;
            case Direction.Down:
                GoDown(deltaTime,canMove);
                break;
            case Direction.Left:
                GoLeft(deltaTime,canMove);
                break;
            case Direction.Right:
                GoRight(deltaTime,canMove);
                break;
            default:
                Direction = Direction.Neutral;
                CurrentSprite = NPCSpriteData.DownSprites[0];
                break;
        }
        _currentWalkDuration++;
        if (_currentWalkDuration < WalkDuration) return;
        _currentWalkDuration = 0;
        isWalking = false;
    }
    public Texture2D GetTexture()
    {
        return Texture;
    }

    public void SetTexture(Texture2D texture)
    {
        Texture = texture;
    }

    public ResultAction[] Interact()
    {
        if (!IsInteractable || Interactions is null) return new[] { ResultAction.Nothing };
        return !Interacted ? Interactions : new []{ResultAction.NoMoreInteraction };
    }
    public void SwitchState()
    {
        Interacted = true;
    }

    public List<(string, string)> ResultTexts { get; set; }
    public List<(Vector2, PlayerState)> ResultMovePositions { get; set; }
    public List<int> ResultDelays { get; set; }
    public List<GameObject> ObjectAdditions { get; set; }
    public (string, string) NoInteractionText { get; set; }
}