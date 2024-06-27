using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Data;

public class CatSpriteData : ISpriteData
{
    public int SpriteCount { get; set; } = 3;
      private static int _catWidth { get; } = 32;
    private static int _catHeight { get; } = 32;
    private static int row0 = 0;
    private static int row1 = _catHeight;
    private static int row2 = _catHeight * 2;
    private static int row3 = _catHeight * 3;
    private static int column0 = 0;
    private static int column1 = _catWidth;
    private static int column2 = _catWidth * 2;
    private static int column3 = _catWidth * 3;



    private static Rectangle DownSprite0 = new(column0, row0, _catWidth, _catHeight);
    private static Rectangle DownSprite1 = new(column1, row0, _catWidth, _catHeight);
    private static Rectangle DownSprite2 = new(column2, row0, _catWidth, _catHeight);
    private static Rectangle RightSprite0 = new(column0, row2, _catWidth, _catHeight);
    private static Rectangle RightSprite1 = new(column1, row2, _catWidth, _catHeight);
    private static Rectangle RightSprite2 = new(column2, row2, _catWidth, _catHeight);
    private static Rectangle UpSprite0 = new(column0, row3, _catWidth, _catHeight);
    private static Rectangle UpSprite1 = new(column1, row3, _catWidth, _catHeight);
    private static Rectangle UpSprite2 = new(column2, row3, _catWidth, _catHeight);
    private static Rectangle LeftSprite0 = new(column0, row1, _catWidth, _catHeight);
    private static Rectangle LeftSprite1 = new(column1, row1, _catWidth, _catHeight);
    private static Rectangle LeftSprite2 = new(column2, row1, _catWidth, _catHeight);


    public Rectangle[] UpSprites { get; } =
    {
        UpSprite0, UpSprite1, UpSprite2
    };
    public Rectangle[] RightSprites { get; } =
    {
        RightSprite0, RightSprite1, RightSprite2
    };
    
    public Rectangle[] DownSprites { get; } =
    {
        DownSprite0, DownSprite1, DownSprite2
    };
    public Rectangle[] LeftSprites { get; } =
    {
        LeftSprite0, LeftSprite1, LeftSprite2
    };
}