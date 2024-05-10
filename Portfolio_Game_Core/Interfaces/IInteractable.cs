using Portfolio_Game_Core.Entities.Base;

namespace Portfolio_Game_Core.Interfaces;

public interface IInteractable
{
    //public void Interact();
    public (string, string, GameItem) Interact();
}