using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Helpers;
using Portfolio_Game_Core.Interfaces;
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
        MapExits.Add(new Vector2(10,Height/2),( MapService.Maps["house-entry"],Direction.Left));
    }

    protected override void GetFloor()
    {
    }

    protected override void SeedObjects()
    {
        var bath = new Generic(30, 65, 97, 67, "bath",TextData.ObjectEndTexts["Bath"], () =>
        {
            return new[]
            {
                ResultAction.ShowText,
                ResultAction.MovePlayer,
                ResultAction.AddObject,
                ResultAction.Delay,
                ResultAction.ShowText,
                ResultAction.Delay,
                ResultAction.ShowText,
                ResultAction.Delay,
                ResultAction.ShowText,
                ResultAction.Delay,
                ResultAction.RemoveObject,
                ResultAction.ResetPlayer,
                ResultAction.ShowText,
                ResultAction.ShowText,
                ResultAction.AddToInventory,
                ResultAction.SwitchObjectState,
            };
        });
        foreach (string text in TextData.ObjectsTexts["Bath"])
        {
            bath.ResultTexts.Add(("Bath", text));
        }
        bath.ResultMovePositions.Add((new Vector2(75,60),PlayerState.Left));
        bath.ResultDelays.AddRange(new[] { 100, 100, 100, 100 });
        bath.ObjectAdditions.Add(new TopGraphic(30,65,97,67,"bathpiece"));
        bath.Inventory.Add(new OuterWilds(0));
        Objects.Add(bath);
        
    }
    protected override void SeedGraphicObjects()
    {
       // GraphicObjects.Add(new Carpet((int)(new Vector2(Width,Height).X/2 - Carpet.carpetWidth/2),(int)(Height/2 - Carpet.carpetHeight/2)));
    }

    protected override void SeedStartText()
    {
    }

    protected override void SeedWalls()
    {
        var walls = MapHelper.MakeMapSmaller(new Vector2(Width,Height), 2,15,2,1);
       walls = walls.Where(w=>Math.Abs(w.Middle.Y - Height/2) > 5 || w.Left > 150).ToList();
       Objects.AddRange(walls);

    }

    public override void GetEntryLocation(Direction entryDirection)
    {
    EntryLocation = new Vector2(15, (int)(Height / 2 - 10 - (float)Player.PlayerHeight/2));
    }

}