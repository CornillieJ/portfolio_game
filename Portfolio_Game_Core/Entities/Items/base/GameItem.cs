using Microsoft.Xna.Framework;

namespace Portfolio_Game_Core.Entities.Base;

public class GameItem
{
    private const int WindowMarginX = 40;
    private const int WindowMarginY = 22;
    private const int ItemMarginX = 10;
    private const int ItemMarginY = 8;
    
    public string Description { get; set; }
    public int ItemWidth { get; set; } = 30;
    public int ItemHeight { get; set; } = 35;
    public int InventoryNumber { get; set; }
    public float ItemPositionY { get; set; }
    public float ItemPositionX { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float Right => PositionX + ItemWidth;
    public float Left => PositionX;
    public float Top => PositionY;
    public float Bottom => PositionY + ItemHeight;
    public Rectangle CurrentSprite { get; protected set; }
    public GameItem(int inventoryNumber)
    {
        UpdatePosition(inventoryNumber);
    }
    public void UpdatePosition(int inventoryNumber)
    {
        InventoryNumber = inventoryNumber;
        ItemPositionX =WindowMarginX + ItemMarginX +(inventoryNumber % 5) * (ItemMarginX + ItemWidth);
        ItemPositionY =WindowMarginY + ItemMarginY + (inventoryNumber/5) * (ItemMarginY + ItemWidth);
    }

}