using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Helpers;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game_Core.Maps;

public class Bathroom:Map
{
    public Bathroom(float screenWidth, float screenHeight) : base(screenWidth, screenHeight)
    {
        Width = 800;
        Height = 480;
        SeedGraphicObjects();
        SeedObjects();
        SeedStartText();
        SeedWalls();
    }
     public override void SeedNextMaps()
    {
        MapExits.Add(new Vector2(10,ScreenSize.Y/2),( MapService.Maps["house-entry"],Direction.Left));
    }

    protected override void GetFloor()
    {
    }

    protected override void SeedObjects()
    {
    }
    protected override void SeedGraphicObjects()
    {
       // GraphicObjects.Add(new Carpet((int)(ScreenSize.X/2 - Carpet.carpetWidth/2),(int)(ScreenSize.Y/2 - Carpet.carpetHeight/2)));
    }

    protected override void SeedStartText()
    {
    }

    protected override void SeedWalls()
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