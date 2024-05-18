using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Entities.Items;

namespace Portfolio_Game_Core.Entities.Items;

public class Website:GameItem
{
    public string ProgramPath { get; protected set; }
    public Website(int inventoryNumber) : base(inventoryNumber)
    {
    }
}