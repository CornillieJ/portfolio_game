using Portfolio_Game_Core.Entities.Base;

namespace Portfolio_Game_Core.Interfaces;

public interface IHasInventory
{
        public List<GameItem> Inventory { get; }
        
        public void ShowInventory();
}