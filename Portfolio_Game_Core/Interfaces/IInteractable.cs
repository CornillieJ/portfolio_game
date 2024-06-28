using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Entities.Base;

namespace Portfolio_Game_Core.Interfaces;

public interface IInteractable
{
    //public void Interact();
    public ResultAction[] Interact();
    public void SwitchState();
    public List<(string, string)> ResultTexts { get; set; }
    public List<(Vector2, PlayerState)> ResultMovePositions { get; set; }
    public List<int> ResultDelays { get; set; }
    public List<GameObject> ObjectAdditions { get; set; }
    public (string, string) NoInteractionText { get; set; }

}