using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game_Core.Entities;

public class Chest:GameObject, IInteractable, IVisible, IHasInventory
{
    private static Texture2D Texture { get; set; }
    private static int ChestWidth => 28;
    private static int ChestHeight => 27;
    private bool _isOpen = false;
    public List<GameItem> Inventory { get; set; }
    public List<(string, string)> ResultTexts { get; set; }
    public List<(Vector2, PlayerState)> ResultMovePositions { get; set; }
    public List<int> ResultDelays { get; set; }
    public (string, string) NoInteractionText { get; set; }
    public List<GameObject> ObjectAdditions { get; set; }

    public Chest(int x, int y)
    {
        Width = ChestWidth;
        Height = ChestHeight;
        PositionX = x;
        PositionY = y;
        CurrentSprite = new Rectangle(0, 0, Width, Height);
        Inventory = new List<GameItem>();
        ResultTexts = new List<(string, string)>();
        ResultMovePositions = new List<(Vector2, PlayerState)>();
        ResultDelays = new List<int>();
        ObjectAdditions = new List<GameObject>();
    }

    public Chest(int x, int y, GameItem item) : this(x, y)
    {
        Inventory.Add(item);
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
        if (_isOpen) return new []{ResultAction.Nothing};
        return new []
        {
            ResultAction.ShowText,
            ResultAction.AddToInventory,
            ResultAction.SwitchObjectState
        };
    }
    
    public void ShowInventory()
    {
       //TODO 
    }
    
    public void SwitchState()
    {
        _isOpen = true;
        CurrentSprite = new Rectangle(34, 0, Width, Height);
    }
}
