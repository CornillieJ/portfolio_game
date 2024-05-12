using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Helpers;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game_Core.Maps;

public class Garden:Map
{
    public Garden(float screenWidth, float screenHeight) : base(screenWidth, screenHeight)
    {
        Width = 1600;
        Height = 1600;
        GetFloor();
        SeedGraphicObjects();
        SeedObjects();
        SeedStartText();
        SeedWalls();
    }

    public override  void SeedNextMaps()
    {
        MapExits.Add(new Vector2(927,670),( MapService.Maps["house-entry"],Direction.Down));
        MapExits.Add(new Vector2(928,926),(MapService.Maps["house-entry"],Direction.Up));
    }

    protected override void SeedObjects()
    {
        Objects.Add(new Chest(50,50, new Tankie(0)));
    }

    protected override void GetFloor()
    {
        Floor = new Floor(0,0);
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
    }
    public override void GetEntryLocation(Direction entryDirection)
    {
        switch (entryDirection)
        {
            case Direction.Down:
                EntryLocation = new Vector2(900,940);
                break;
            case Direction.Up:
                EntryLocation = new Vector2(900 , 620);
                break;
        } 
    }
}