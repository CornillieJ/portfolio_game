using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Helpers;

namespace Portfolio_Game_Core.Maps;

public class Garden:Map
{
    public Garden(float screenWidth, float screenHeight) : base(screenWidth, screenHeight)
    {
        SeedNextMaps();
        GetFloor();
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
        Objects.Add(new Chest(50,50, new Tankie(0)));
    }

    private void GetFloor()
    {
        Floor = new Floor(0,0);
        // int width = FloorTile.FloorWidth; 
        // int height = FloorTile.FloorHeight; 
        // for (int i = 0; i < ScreenSize.Y; i++)
        // {
        //     for (int j = 0; j < ScreenSize.X; j++)
        //     {
        //         Floors.Add(new FloorTile(j*width,i*height));
        //     }
        // }
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
        EntryLocation = new Vector2((int)(ScreenSize.X / 2 - (float)Player.PlayerWidth/2), (int)(ScreenSize.Y / 2 - (float)Player.PlayerHeight/2));
    }
}