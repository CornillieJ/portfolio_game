using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Helpers;
using Portfolio_Game_Core.Interfaces;
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
        SeedTopGraphics();
    }


    public override  void SeedNextMaps()
    {
        MapExits.Add(new Vector2(927,670),( MapService.Maps["house-entry"],Direction.Down));
        MapExits.Add(new Vector2(928,926),(MapService.Maps["house-entry"],Direction.Up));
    }

    protected override void SeedObjects()
    {
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
        int wallWidth = 15;
        Vector2 fenceOrigin = new(671, 440);
        Vector2 fenceSize = new(522,240);
        int gapMargin = 30;
        Objects.Add(new InvisibleWall(fenceOrigin.X,fenceOrigin.Y,fenceSize.X,wallWidth));
        Objects.Add(new InvisibleWall(fenceOrigin.X,fenceOrigin.Y+fenceSize.Y-wallWidth,fenceSize.X/2-gapMargin/2,wallWidth));
        Objects.Add(new InvisibleWall(fenceOrigin.X + fenceSize.X/2+gapMargin,fenceOrigin.Y+fenceSize.Y-wallWidth,fenceSize.X/2-gapMargin/2,wallWidth));
        Objects.Add(new InvisibleWall(fenceOrigin.X,fenceOrigin.Y,wallWidth,fenceSize.Y));
        Objects.Add(new InvisibleWall(fenceOrigin.X+fenceSize.X - wallWidth,fenceOrigin.Y,wallWidth,fenceSize.Y));
        Vector2 houseOrigin = new(740, 690);
        Vector2 houseSize = new(380,235);
        Objects.AddRange(MapHelper.GetSurroundingInvisibleWalls(houseOrigin,houseSize,20));
    }
    
    private void SeedTopGraphics()
    {
        GraphicTopObjects.Add( new TopGraphic(723,681,419,163,"houseroof"));
    }
    public override void GetEntryLocation(Direction entryDirection)
    {
        switch (entryDirection)
        {
            case Direction.Down:
                EntryLocation = new Vector2(920,930);
                break;
            case Direction.Up:
                EntryLocation = new Vector2(920 , 600);
                break;
        } 
    }
}