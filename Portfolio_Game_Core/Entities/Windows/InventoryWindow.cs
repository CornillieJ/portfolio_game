using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Portfolio_Game_Core.Entities;

public class InventoryWindow:Window,IVisible
{
    public Direction location { get; set; } = Direction.Right;
    private static int InventoryWindowWidth => 320;
    public static int InventoryWindowHeight => 400;
    public static Texture2D Texture { get; set; }
    public bool IsOpen { get; set; }
    public List<GameItem> Inventory { get; set; }
    public List<Text>? Description { get; set; }
    public string DescriptionText { get; set; }

    public InventoryWindow(int x, int y) : base(x, y, "Inventory")
    {
        TextMarginX = 54;
        TextMarginY = 270; 
        Inventory = new List<GameItem>();
        Width = InventoryWindowWidth;
        Height = InventoryWindowHeight; 
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(20, 0, Width, Height);
    }

    private void FillInventory()
    {
        foreach (var item in Inventory)
        {
            
        }
    }

    public void ShowDescription(GameItem item)
    {
        DescriptionText = item.Description;
        Description = GetText(225, 127, item.Description);
    }
    public void HideDescription()
    {
        Description = null;
    }
    public Texture2D GetStaticTexture()
    {
        return Texture;
    }


    public void SetStaticTexture(Texture2D texture)
    {
        Texture = texture;
    }
}