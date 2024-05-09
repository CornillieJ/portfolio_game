using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class Chest:GameObject, IInteractable, IVisible
{
    public static Texture2D Texture { get; set; }
    private static int ChestWidth => 30;
    private static int ChestHeight => 25;
    private List<GameObject> _inventory;
    public IEnumerable<GameObject> Inventory
    {
        get => _inventory.AsReadOnly();
    }
    public Chest(int x, int y)
    {
        Width = ChestWidth;
        Height = ChestHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 1, Width, Height);
        _inventory = new List<GameObject>();
    }
    public Texture2D GetStaticTexture()
    {
        return Texture;
    }
    public void SetStaticTexture(Texture2D texture)
    {
        Texture = texture;
    }
    public void Interact()
    {
        
    }

    public void ShowInventory()
    {
        
    }
}