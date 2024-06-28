using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Items;

public class BongoCat:ProgramItem,IVisible
{
    public static Texture2D Texture { get; set; }
    public BongoCat(int inventoryNumber) : base(inventoryNumber)
    {
        ProgramPath =  @"Programs\KerSplash\KeySplash.exe";
        Description = TextData.ItemTexts["KerSplash"]; 
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