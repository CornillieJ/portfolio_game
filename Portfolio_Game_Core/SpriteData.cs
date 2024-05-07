using Microsoft.Xna.Framework;

namespace Portfolio_Game_Core;

public static class SpriteData
{

    private static int _playerWidth { get; } = 32;
    private static int _playerHeight { get; } = 64;
    private static int row0 = 0;
    private static int row1 = _playerHeight;
    private static int row2 = _playerHeight * 2;
    private static int row3 = _playerHeight * 3;
    private static int column0 = 0;
    private static int column1 = _playerWidth;
    private static int column2 = _playerWidth * 2;
    private static int column3 = _playerWidth * 3;



    private static Rectangle DownSprite0 = new(column0, row0, _playerWidth, _playerHeight);
    private static Rectangle DownSprite1 = new(column1, row0, _playerWidth, _playerHeight);
    private static Rectangle DownSprite2 = new(column2, row0, _playerWidth, _playerHeight);
    private static Rectangle DownSprite3 = new(column3, row0, _playerWidth, _playerHeight);
    private static Rectangle RightSprite0 = new(column0, row1, _playerWidth, _playerHeight);
    private static Rectangle RightSprite1 = new(column1, row1, _playerWidth, _playerHeight);
    private static Rectangle RightSprite2 = new(column2, row1, _playerWidth, _playerHeight);
    private static Rectangle RightSprite3 = new(column3, row1, _playerWidth, _playerHeight);
    private static Rectangle UpSprite0 = new(column0, row2, _playerWidth, _playerHeight);
    private static Rectangle UpSprite1 = new(column1, row2, _playerWidth, _playerHeight);
    private static Rectangle UpSprite2 = new(column2, row2, _playerWidth, _playerHeight);
    private static Rectangle UpSprite3 = new(column3, row2, _playerWidth, _playerHeight);
    private static Rectangle LeftSprite0 = new(column0, row3, _playerWidth, _playerHeight);
    private static Rectangle LeftSprite1 = new(column1, row3, _playerWidth, _playerHeight);
    private static Rectangle LeftSprite2 = new(column2, row3, _playerWidth, _playerHeight);
    private static Rectangle LeftSprite3 = new(column3, row3, _playerWidth, _playerHeight);

    public static Rectangle[] UpSprites { get; } =
    {
        UpSprite0, UpSprite1, UpSprite2, UpSprite3
    };
    public static Rectangle[] RightSprites { get; } =
    {
        RightSprite0, RightSprite1, RightSprite2, RightSprite3
    };
    
    public static Rectangle[] DownSprites { get; } =
    {
        DownSprite0, DownSprite1, DownSprite2, DownSprite3
    };
    public static Rectangle[] LeftSprites { get; } =
    {
        LeftSprite0, LeftSprite1, LeftSprite2, LeftSprite3
    };
}