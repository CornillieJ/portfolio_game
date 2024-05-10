using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Helpers;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Maps;

public class FirstMap:Map
{
    public FirstMap(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
    {
        GetFloor();
        SeedGraphicObjects();
        SeedObjects();
        SeedStartText();
        SeedWalls();
    }

    private void SeedObjects()
    {
        Objects.Add(new Chest(50,50, new Tankie(0)));
    }

    private void GetFloor()
    {
        int width = Floor.FloorWidth; 
        int height = Floor.FloorHeight; 
        for (int i = 0; i < ScreenSize.Y; i++)
        {
            for (int j = 0; j < ScreenSize.X; j++)
            {
                Floors.Add(new Floor(j*width,i*height));
            }
        }
    }

    private void SeedGraphicObjects()
    {
        GraphicObjects.Add(new Carpet((int)(ScreenSize.X/2 - Carpet.carpetWidth/2),(int)(ScreenSize.Y/2 - Carpet.carpetHeight/2)));
    }

    private void SeedStartText()
    {
        Windows.AddRange(_windowCreator.GetTextWindows(new List<string>{ "Welcome"} , TextData.WelcomeTexts));
    }

    private void SeedWalls()
    {
        int height = Wall.WallHeight;
        var walls = MapHelper.GetWallsSurroundingMap(ScreenSize);
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
    }
}