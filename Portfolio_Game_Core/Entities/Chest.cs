using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game_Core.Entities;

public class Chest:GameObject, IInteractable, IVisible
{
    private static Texture2D Texture { get; set; }
    private static int ChestWidth => 28;
    private static int ChestHeight => 27;
    private List<GameItem> _inventory;
    private bool _isOpen = false;
    public IEnumerable<GameItem> Inventory
    {
        get => _inventory.AsReadOnly();
    }
    public Chest(int x, int y)
    {
        Width = ChestWidth;
        Height = ChestHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 0, Width, Height);
        _inventory = new List<GameItem>();
    }

    public Chest(int x, int y, GameItem item) : this(x, y)
    {
        _inventory.Add(item);
    }
    public Texture2D GetStaticTexture()
    {
        return Texture;
    }
    public void SetStaticTexture(Texture2D texture)
    {
        Texture = texture;
    }
    public (string,string, GameItem) Interact()
    {
        if (_isOpen) return (null,null, null);
        _isOpen = true;
        CurrentSprite = new Rectangle(34, 0, Width, Height);
        return ("chest",TextData.ChestTexts[0],_inventory[0]);
    }

    public void ShowInventory()
    {
       //TODO 
    }
}