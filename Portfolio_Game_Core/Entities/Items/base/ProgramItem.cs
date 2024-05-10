using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Items;

public class ProgramItem:GameItem
{
    public string ProgramPath { get; protected set; }
    public ProgramItem(int inventoryNumber) : base(inventoryNumber)
    {
    }
}