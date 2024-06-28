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
        MapExits.Add(new Vector2(930,668),( MapService.Maps["house-entry"],Direction.Down));
        MapExits.Add(new Vector2(900,668),( MapService.Maps["house-entry"],Direction.Down));
        MapExits.Add(new Vector2(950,668),( MapService.Maps["house-entry"],Direction.Down));
        MapExits.Add(new Vector2(928,910),(MapService.Maps["house-entry"],Direction.Up));
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
        int gapMarginLeft = 40;
        int gapMarginRight = 20;
        Objects.Add(new InvisibleWall(fenceOrigin.X,fenceOrigin.Y,fenceSize.X,wallWidth));
        Objects.Add(new InvisibleWall(fenceOrigin.X,fenceOrigin.Y+fenceSize.Y-wallWidth,fenceSize.X/2-gapMarginLeft,wallWidth));
        Objects.Add(new InvisibleWall(fenceOrigin.X + fenceSize.X/2 + gapMarginRight,fenceOrigin.Y+fenceSize.Y-wallWidth,fenceSize.X/2-gapMarginRight,wallWidth));
        Objects.Add(new InvisibleWall(fenceOrigin.X,fenceOrigin.Y,wallWidth,fenceSize.Y));
        Objects.Add(new InvisibleWall(fenceOrigin.X+fenceSize.X - wallWidth,fenceOrigin.Y,wallWidth,fenceSize.Y));
        Vector2 houseOrigin = new(740, 688);
        Vector2 houseSize = new(380,237);
        Objects.AddRange(MapHelper.GetSurroundingInvisibleWalls(houseOrigin,houseSize,20));
        //TODO: Remove this invisible wall and expand garden
        Objects.AddRange(MapHelper.GetSurroundingInvisibleWalls(new (508,380),new(864,740),20));
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
                EntryLocation = new Vector2(918,928);
                break;
            case Direction.Up:
                EntryLocation = new Vector2(920 , 600);
                break;
        } 
    }
}