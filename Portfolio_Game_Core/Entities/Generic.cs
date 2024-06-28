using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities;

public class Generic:GameObject,IVisible,IInteractable,IHasInventory
{
    public List<(string, string)> ResultTexts { get; set; }
    public List<(Vector2,PlayerState)> ResultMovePositions { get; set; }
    public List<int> ResultDelays { get; set; }
    public Texture2D Texture { get; set; } 
    public string GraphicText { get; set; }
    public ResultAction[] Interactions;
    public bool Interacted { get; set; }
    public (string, string) NoInteractionText { get; set; }
    public List<GameObject> ObjectAdditions { get; set; }
    public List<GameItem> Inventory { get; set; }
    
    public bool IsInteractable { get; set; }
    public bool IsJustOnce { get; set; }
    public Generic(int x, int y, int width, int height, string graphicText, string noInteractionText = "", ResultAction[] interactions = null, bool isInteractable = false, bool isJustOnce = false)
    {
        PositionX = x;
        PositionY = y;
        Width = width;
        Height = height;
        GraphicText = graphicText;
        Interactions = interactions;
        IsInteractable = isInteractable;
        IsJustOnce = isJustOnce;
        NoInteractionText = (graphicText,noInteractionText);
        CurrentSprite = new Rectangle(0,0,Width,Height);
        ResultTexts = new List<(string, string)>();
        ResultMovePositions = new List<(Vector2, PlayerState)>();
        ResultDelays = new List<int>();
        ObjectAdditions = new List<GameObject>();
        Inventory = new List<GameItem>();
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
        CurrentSprite = new Rectangle(Width,0,Width,Height);
    }

    public void ShowInventory()
    {
        throw new NotImplementedException();
    }
}