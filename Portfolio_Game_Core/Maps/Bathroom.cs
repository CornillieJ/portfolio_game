using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Helpers;

namespace Portfolio_Game_Core.Maps;

public class Bathroom:Map
{
    public Bathroom(float screenWidth, float screenHeight) : base(screenWidth, screenHeight)
    {
        SeedGraphicObjects();
        SeedObjects();
        SeedStartText();
        SeedWalls();
    }
     private void SeedNextMaps()
    {
    }

    private void SeedObjects()
    {
    }
    private void SeedGraphicObjects()
    {
       // GraphicObjects.Add(new Carpet((int)(ScreenSize.X/2 - Carpet.carpetWidth/2),(int)(ScreenSize.Y/2 - Carpet.carpetHeight/2)));
    }

    private void SeedStartText()
    {
    }

    private void SeedWalls()
    {
        var walls = MapHelper.MakeMapSmaller(ScreenSize, 2,15,2,1);
       walls = walls.Where(w=>Math.Abs(w.Middle.Y - ScreenSize.Y/2) > 5 || w.Left > 150).ToList();
       Objects.AddRange(walls);

    }

    public override void GetEntryLocation(Direction entryDirection)
    {
        EntryLocation = new Vector2(15, (int)(ScreenSize.Y / 2 - (float)Player.PlayerHeight/2));
    }
}