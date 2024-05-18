using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class Wall:GameObject, IVisible
{
    public static Texture2D Texture { get; set; }
    public static int WallWidth => 32;
    public static int WallHeight => 32;
    public Wall(int x, int y, Direction direction)
    {
        Width = WallWidth;
        Height = WallHeight;
        PositionX = x;
        PositionY = y;
        GetRectangleDirection(direction);
    }
    public Wall(int x, int y, Direction direction, Direction directionEnd)
    {
        Width = WallWidth;
        Height = WallHeight;
        PositionX = x;
        PositionY = y;
         GetThinRectangleDirection(direction, directionEnd);
    }
    private void GetThinRectangleDirection(Direction direction, Direction endDirection)
    {
        //down and up are vertical walls, left and right are horizontal (direction = position of the wall)
        CurrentSprite = (direction,endDirection) switch
        {
            (Direction.Down,Direction.Neutral) or (Direction.Up,Direction.Neutral) 
                => new Rectangle(256, 32, Width, Height),
            (Direction.Right,Direction.Neutral) or (Direction.Left,Direction.Neutral) 
                => new Rectangle(256, 0, Width, Height),
            (Direction.Down,Direction.Left) or (Direction.Up,Direction.Left) 
                => new Rectangle(192, 32, Width, Height),
            (Direction.Down,Direction.Right) or (Direction.Up,Direction.Right) 
                => new Rectangle(224, 32, Width, Height),
            (Direction.Right,Direction.Up) or (Direction.Left,Direction.Up) 
                => new Rectangle(192, 0, Width, Height),
            (Direction.Right,Direction.Down) or (Direction.Left,Direction.Down) 
                => new Rectangle(256, 0, Width, Height),
            _ => new Rectangle(224, 0, Width, Height)
        };
    }

    private void GetRectangleDirection(Direction direction)
    {
        CurrentSprite = direction switch
        {
            Direction.Down => new Rectangle(64, 64, Width, Height),
            Direction.Up => new Rectangle(64, 0, Width, Height),
            Direction.Right => new Rectangle(96, 32, Width, Height),
            Direction.Left => new Rectangle(32, 32, Width, Height),
            Direction.DownLeft => new Rectangle(32, 64, Width, Height),
            Direction.DownRight => new Rectangle(96, 64, Width, Height),
            Direction.UpLeft => new Rectangle(32, 0, Width, Height),
            Direction.UpRight => new Rectangle(96, 0, Width, Height),
            _ => new Rectangle(64, 32, Width, Height)
        };
    }

    public Texture2D GetTexture()
    {
        return Texture;
    }

    public void SetTexture(Texture2D texture)
    {
        Texture = texture;
    }
}