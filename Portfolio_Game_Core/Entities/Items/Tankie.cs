using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Entities.Items;

public class Tankie:Program,IVisible
{
    public static Texture2D Texture { get; set; }
    public Tankie(int inventoryNumber) : base(inventoryNumber)
    {
        Description =  "A tank game made on project day in Howest";
        CurrentSprite = new Rectangle(0, 0, ItemWidth, ItemHeight);
    }
    public Texture2D GetStaticTexture()
    {
        return Texture;
    }
    public void SetStaticTexture(Texture2D texture)
    {
        Texture = texture;
    }
}