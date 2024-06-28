using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Items;

public class OuterWilds:Website,IVisible
{
    public static Texture2D Texture { get; set; }
    public OuterWilds(int inventoryNumber) : base(inventoryNumber)
    {
        ProgramPath =  @"Programs\OuterWilds\index.html";
        Description = TextData.ItemTexts["OuterWilds"]; 
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