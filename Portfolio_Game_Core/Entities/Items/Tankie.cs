using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Items;

public class Tankie:ProgramItem,IVisible
{
    public static Texture2D Texture { get; set; }
    public Tankie(int inventoryNumber) : base(inventoryNumber)
    {
        ProgramPath =  @"Programs\Tankie\Tankie.exe";
        Description = TextData.ItemTexts["Tankie"]; 
        CurrentSprite = new Rectangle(0, 0, ItemWidth, ItemHeight);
    }
    public Texture2D GetTexture()
    {
        return Texture;
    }
    public void SetTexture(Texture2D texture)
    {
        Texture = texture;
    }
}