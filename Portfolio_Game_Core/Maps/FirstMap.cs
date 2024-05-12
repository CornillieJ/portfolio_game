using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Helpers;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game_Core.Maps;

public class FirstMap:Map
{
    public FirstMap(float screenWidth, float screenHeight) : base(screenWidth, screenHeight)
    {
        Width = 800;
        Height = 480;
        GetFloor();
        SeedGraphicObjects();
        SeedObjects();
        SeedStartText();
        SeedWalls();
    }

    public override void SeedNextMaps()
    {
        MapExits.Add(new Vector2(ScreenSize.X/2,5),( MapService.Maps["garden"],Direction.Up));
        MapExits.Add(new Vector2(ScreenSize.X/2,ScreenSize.Y-5),( MapService.Maps["garden"],Direction.Down));
        MapExits.Add(new Vector2(ScreenSize.X,ScreenSize.Y/2),(MapService.Maps["bathroom"],Direction.Right));
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
        Windows.AddRange(_windowCreator.GetTextWindows(new List<string>{ "Welcome"} , TextData.WelcomeTexts));
    }

    protected override void SeedWalls()
    {
        int height = Wall.WallHeight;
        var walls = MapHelper.GetWallsSurroundingMap(ScreenSize);
        walls = walls.Where(w=>Math.Abs(w.Middle.Y - ScreenSize.Y/2) > 5 && Math.Abs(w.Middle.X - ScreenSize.X/2) > 5 || w.Left <20).ToList();
        walls.AddRange(new List<Wall>()
        {
            new Wall(180, 0, Direction.Neutral),
            new Wall(180, 0 + height, Direction.Right,Direction.Neutral),
            new Wall(180, 0 + height * 1, Direction.Right,Direction.Neutral),
            new Wall(180, 0 + height * 2, Direction.Right,Direction.Neutral),
            new Wall(180, 0 + height * 3, Direction.Right,Direction.Neutral),
            new Wall(180, 0 + height * 4, Direction.Right,Direction.Down),
        });
        walls.AddRange(new List<Wall>()
        {
            new Wall(250, (int)(ScreenSize.Y - height), Direction.Neutral),
            new Wall(250, (int)(ScreenSize.Y - height * 2), Direction.Right,Direction.Neutral),
            new Wall(250, (int)(ScreenSize.Y - height * 3), Direction.Right,Direction.Neutral),
            new Wall(250, (int)(ScreenSize.Y - height * 4), Direction.Right,Direction.Neutral),
            new Wall(250, (int)(ScreenSize.Y - height * 5), Direction.Right,Direction.Up),
        });
        walls.AddRange(new List<Wall>()
        {
            new Wall((int)(ScreenSize.X - 250), (int)(ScreenSize.Y - height), Direction.Neutral),
            new Wall((int)(ScreenSize.X - 250), (int)(ScreenSize.Y - height * 2), Direction.Right,Direction.Neutral),
            new Wall((int)(ScreenSize.X - 250), (int)(ScreenSize.Y - height * 3), Direction.Right,Direction.Neutral),
            new Wall((int)(ScreenSize.X - 250), (int)(ScreenSize.Y - height * 4), Direction.Right,Direction.Neutral),
            new Wall((int)(ScreenSize.X - 250), (int)(ScreenSize.Y - height * 5), Direction.Right,Direction.Up),
        });
        Objects.AddRange(walls);
        Objects.Add(new WindowInWall(Wall.WallWidth*3,0));
        Objects.Add(new WindowInWall(Wall.WallWidth*10,0));
        Objects.Add(new WindowInWall(Wall.WallWidth*17,0));
    }

    public override void GetEntryLocation(Direction entryDirection)
    {
        switch (entryDirection)
        {
            case Direction.Up:
                EntryLocation = new Vector2((int)(ScreenSize.X / 2 - (float)Player.PlayerWidth/2), ScreenSize.Y - 25);
                break;
            case Direction.Down:
                EntryLocation = new Vector2((int)(ScreenSize.X / 2 - (float)Player.PlayerWidth/2), 10);
                break;
            case Direction.Left:
                EntryLocation = new Vector2(ScreenSize.X - 25, (int)(ScreenSize.Y / 2 - (float)Player.PlayerHeight/2));
                break;
            case Direction.Neutral:
                EntryLocation = new Vector2((int)(ScreenSize.X / 2 - (float)Player.PlayerWidth/2), (int)(ScreenSize.Y / 2 - (float)Player.PlayerHeight/2));
                break;
        } 
    }
}