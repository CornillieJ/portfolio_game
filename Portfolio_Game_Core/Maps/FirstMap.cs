﻿using Microsoft.Xna.Framework;
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
        IsTranslation = false;
        Width = 800;
        Height = 480;
        GetFloor();
        SeedGraphicObjects();
        SeedObjects();
        SeedStartText();
        SeedWalls();
        SeedMovables();
    }

    private void SeedMovables()
    {
        MovingNPC cat = new MovingNPC(100, 100, "cat", new CatSpriteData());
        cat.Inventory.Add(new BongoCat(0));
        cat.ObjectAdditions.Add(cat);
        foreach (string text in TextData.ObjectsTexts["Cat"])
        {
            cat.ResultTexts.Add(("Cat", text));
        } 
        cat.Interactions = new[]
            { ResultAction.ShowText, ResultAction.ShowText,ResultAction.ShowText, ResultAction.AddToInventory, ResultAction.RemoveObject };
        Objects.Add(cat);
    }

    public override void SeedNextMaps()
    {
        MapExits.Add(new Vector2(Width/2,5),( MapService.Maps["garden"],Direction.Up));
        MapExits.Add(new Vector2(Width/2,Height-5),( MapService.Maps["garden"],Direction.Down));
        MapExits.Add(new Vector2(Width-5,Height/2),(MapService.Maps["bathroom"],Direction.Right));
    }

    protected override void SeedObjects()
    {
        var chestTankie = new Chest(50, 50, new Tankie(0));
        chestTankie.ResultTexts.Add(("chest", TextData.ChestTexts[0]));
        Objects.Add(chestTankie);
        ResultAction[] chairActions = { ResultAction.MovePlayer,ResultAction.AddObject};
        Generic[] chairs =
        {
            new(545, 58, 32, 54, "chairLeft","",chairActions,true),
            new(545, 95, 32, 54, "chairLeft","",chairActions,true),
            new(660, 58, 32, 54, "chairRight","",chairActions,true),
            new(660, 95, 32, 54, "chairRight","",chairActions,true)
        };
        Generic table = new(570,60,96,93,"table");
        Generic tableForBoundaries = new(585,70,60,60,"table");
        foreach (var chair in chairs)
        { 
            // chair.ObjectAdditions.Add(new TopGraphic(570,50,96,93,"table")); 
            // chair.ObjectAdditions.Add(new TopGraphic(630,92,28,28,"chessboard"));
            if(chair.GraphicText == "chairLeft")
                chair.ResultMovePositions.Add((new Vector2(chair.PositionX,chair.PositionY-10),PlayerState.Right));
            else
                chair.ResultMovePositions.Add((new Vector2(chair.PositionX,chair.PositionY-10),PlayerState.Left));
            Objects.Add(chair);
            chair.VisionYMarginOverride = 20;
        }
        Objects.Add(tableForBoundaries);
        Objects.Add(table);
        Generic chessBoard = new(630,102,28,28,"chessboard","",
            new[]
            {
                ResultAction.ShowText,
                ResultAction.ShowText,
                ResultAction.ShowText,
                ResultAction.AddToInventory,
                ResultAction.RemoveObject,
                ResultAction.RemoveObject,
            }, true);
        foreach (string text in TextData.ObjectsTexts["Chessboard"])
        {
            chessBoard.ResultTexts.Add(("Chessboard", text));
        }
        chessBoard.ObjectAdditions.Add(chessBoard);
        chessBoard.ObjectAdditions.Add(new TopGraphic(630,102,28,28,"chessboard"));
        chessBoard.Inventory.Add(new Chess(0));
        GraphicMiddleObjects.Add(new TopGraphic(570,60,96,93,"table"));
        GraphicMiddleObjects.Add(new TopGraphic(630,102,28,28,"chessboard"));
        Objects.Add(chessBoard);
    }

    protected override void GetFloor()
    {
        Floor = new Floor(0,0);
    }

    protected override void SeedGraphicObjects()
    {
       // GraphicObjects.Add(new Carpet((int)(Width/2 - Carpet.carpetWidth/2),(int)(Height/2 - Carpet.carpetHeight/2)));
    }

    protected override void SeedStartText()
    {
        Windows.AddRange(_windowCreator.GetTextWindows(new List<string>{ "Welcome"} , TextData.WelcomeTexts));
    }

    protected override void SeedWalls()
    {
        int height = Wall.WallHeight;
        var walls = MapHelper.GetWallsSurroundingMap(new Vector2(Width,Height));
        walls = walls.Where(w=>Math.Abs(w.Middle.Y - Height/2) > 5 && Math.Abs(w.Middle.X - Width/2) > 5 || w.Left <20).ToList();
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
            new Wall(250, (int)(Height - height), Direction.Neutral),
            new Wall(250, (int)(Height - height * 2), Direction.Right,Direction.Neutral),
            new Wall(250, (int)(Height - height * 3), Direction.Right,Direction.Neutral),
            new Wall(250, (int)(Height - height * 4), Direction.Right,Direction.Neutral),
            new Wall(250, (int)(Height - height * 5), Direction.Right,Direction.Up),
        });
        walls.AddRange(new List<Wall>()
        {
            new Wall((int)(Width - 250), (int)(Height - height), Direction.Neutral),
            new Wall((int)(Width - 250), (int)(Height - height * 2), Direction.Right,Direction.Neutral),
            new Wall((int)(Width - 250), (int)(Height - height * 3), Direction.Right,Direction.Neutral),
            new Wall((int)(Width - 250), (int)(Height - height * 4), Direction.Right,Direction.Neutral),
            new Wall((int)(Width - 250), (int)(Height - height * 5), Direction.Right,Direction.Up),
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
                EntryLocation = new Vector2((int)(Width / 2 - (float)Player.PlayerWidth/2), Height - 80);
                break;
            case Direction.Down:
                EntryLocation = new Vector2((int)(Width / 2 - (float)Player.PlayerWidth/2), 10);
                break;
            case Direction.Left:
                EntryLocation = new Vector2(Width - 35, (int)(Height / 2 - 10 - (float)Player.PlayerHeight/2));
                break;
            case Direction.Neutral:
                EntryLocation = new Vector2((int)(Width / 2 - 10 - (float)Player.PlayerWidth/2), (int)(Height / 2 - (float)Player.PlayerHeight/2));
                break;
        } 
    }
}