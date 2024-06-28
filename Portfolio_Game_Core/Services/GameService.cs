using System.ComponentModel.Design;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Maps;

namespace Portfolio_Game_Core.Services;

public class GameService
{
    private int _topMargin = 30;
    private WindowCreator _windowCreator;
    private int _delayCounter;
    private int _delayTime;
    public Player _playerOne { get; set; }
    public Vector2 ScreenSize { get; }
    public Map CurrentMap { get; set; }
    public InventoryWindow InventoryWindow { get; set; }

    private Random _random = new Random();
    public GameService(int screenWidth, int screenHeight)
    {
        CurrentMap = MapService.Maps["house-entry"];
        ScreenSize = new Vector2(screenWidth, screenHeight);
        _windowCreator = new WindowCreator(screenWidth,screenHeight);
        _playerOne = new Player(0, 0);
        InventoryWindow = new InventoryWindow(screenWidth/2 + 50, screenHeight/2 - InventoryWindow.InventoryWindowHeight/2);
        foreach (var map in MapService.Maps.Values)
        {
            map.SeedNextMaps();
        }
    }

    public void AddObject(GameObject gameObject)
    {
        if (CurrentMap.Objects.Contains(gameObject)) return;
        CurrentMap.Objects.Add(gameObject);
    }
    public void RemoveObject(GameObject gameObject)
    { 
        if (CurrentMap.Objects.Contains(gameObject)) return; 
        CurrentMap.Objects.Remove(gameObject);
    }
    public void AddGraphicObject(GameObject gameObject)
    {
        if (CurrentMap.GraphicObjects.Contains(gameObject)) return;
        CurrentMap.GraphicObjects.Add(gameObject);
    }
    public void RemoveGraphicObject(GameObject gameObject)
    { 
        if (CurrentMap.GraphicObjects.Contains(gameObject)) return; 
        CurrentMap.GraphicObjects.Remove(gameObject);
    }
    public bool CanMove(IMovable movable, Direction direction, float deltaTime)
    {
        foreach (var gameObject in CurrentMap.Objects)
        {
            bool isFirstDirectionOk = CheckColission(movable, gameObject, direction, deltaTime);
            if (isFirstDirectionOk == false) return false;
        }
        if(movable is not Player)
            if(!CheckColission(movable, _playerOne, direction, deltaTime)) return false;
        return true;
    }
    public void ShiftWindow()
    {
        CurrentMap.Windows.RemoveAt(0);
    }
    public void RemoveWindow(Window window)
    {
        if (!CurrentMap.Windows.Contains(window)) return;
        CurrentMap.Windows.Remove(window);
    }
    public void AddWindow(Window window)
    {
        CurrentMap.Windows.Add(window);
    }
    public void AddTextWindow(string title, string content)
    {
        CurrentMap.Windows.Add( _windowCreator.GetTextWindow(title,content));
    }
    public GameObject[] GetObjectsAtLocation(Vector2 location)
    {
        int visionMarginX = _playerOne.PlayerState is PlayerState.Left or PlayerState.Right ? 20 : 0;
        int visionMarginY = _playerOne.PlayerState is PlayerState.Left or PlayerState.Right ? 0 : 20;
        List<GameObject> objectsAtLocation = new List<GameObject>();
        foreach (var gameObject in CurrentMap.Objects)
        {
            if (location.X+visionMarginX >= gameObject.InteractLeft && location.X - visionMarginX <= gameObject.InteractRight && location.Y +visionMarginY >= gameObject.InteractTop && location.Y-visionMarginY <= gameObject.InteractBottom)
                objectsAtLocation.Add(gameObject);
        }
        return objectsAtLocation.ToArray();
    }
    public GameObject[] GetObjectsPlayerIsLookingAt()
    {
        Vector2 location = _playerOne.GetVisionCoordinate();
        return GetObjectsAtLocation(location);
    }
    public void AddInteraction(IInteractable interactable)
    {
        CurrentMap.Interactables.Add(interactable);
    }
    public void RemoveInteraction(IInteractable interactable)
    {
        if(CurrentMap.Interactables.Contains(interactable))
            CurrentMap.Interactables.Remove(interactable);
    }
    public void InteractAll(ref int interactCounter, ref Vector2 initialPlayerPosition)
    {
        if (_delayCounter < _delayTime)
        {
            _delayCounter++;
            return;
        }
        _delayTime = 0;
        _delayCounter = 0;
        var resultActions = CurrentMap.Interactables[0].Interact();
        if (resultActions is null) return;
        var resultAction = resultActions[interactCounter];
        switch (resultAction)
        {
            case ResultAction.ShowText:
                string title = CurrentMap.Interactables[0].ResultTexts[0].Item1;
                string content = CurrentMap.Interactables[0].ResultTexts[0].Item2;
                CurrentMap.Interactables[0].ResultTexts.RemoveAt(0);
                CurrentMap.Windows.Add(CurrentMap.GetTextWindow(title,content));
                break;
            case ResultAction.AddToInventory:
                var hasInventory = (IHasInventory)CurrentMap.Interactables[0];
                AddToInventory(hasInventory.Inventory);
                break;
            case ResultAction.MovePlayer:
                initialPlayerPosition = new Vector2(_playerOne.PositionX, _playerOne.PositionY);
                _playerOne.PositionX = CurrentMap.Interactables[0].ResultMovePositions[0].Item1.X;
                _playerOne.PositionY = CurrentMap.Interactables[0].ResultMovePositions[0].Item1.Y;
                _playerOne.PlayerState = CurrentMap.Interactables[0].ResultMovePositions[0].Item2;
                _playerOne.TurnPlayer();
                if(resultActions.Count(a=>a == ResultAction.MovePlayer)>1)
                    CurrentMap.Interactables[0].ResultMovePositions.RemoveAt(0);
                break;
            case ResultAction.ResetPlayer:
                _playerOne.PositionX = initialPlayerPosition.X;
                _playerOne.PositionY = initialPlayerPosition.Y;
                break;
            case ResultAction.Delay:
                _delayTime = CurrentMap.Interactables[0].ResultDelays[0];
                CurrentMap.Interactables[0].ResultDelays.RemoveAt(0);
                break;
            case ResultAction.SwitchObjectState:
                CurrentMap.Interactables[0].SwitchState();
                break;
            case ResultAction.NoMoreInteraction:
                string endTitle = CurrentMap.Interactables[0].NoInteractionText.Item1;
                string endContent = CurrentMap.Interactables[0].NoInteractionText.Item2;
                if (!CurrentMap.Windows.Contains(_windowCreator.GetTextWindow(endTitle, endContent)))
                    CurrentMap.Windows.Add(_windowCreator.GetTextWindow(endTitle, endContent));
                break;
            case ResultAction.AddObject:
                foreach (var gameObject in CurrentMap.Interactables[0].ObjectAdditions)
                {
                    if (gameObject is TopGraphic topGraphic)
                    {
                        if(!CurrentMap.GraphicMiddleObjects.Contains(topGraphic))
                            CurrentMap.GraphicMiddleObjects.Add(topGraphic);
                    }
                    else
                        CurrentMap.Objects.Add(gameObject);
                }
                break;
            case ResultAction.RemoveObject:
                foreach (var gameObject in CurrentMap.Interactables[0].ObjectAdditions)
                {
                    if(gameObject is TopGraphic topGraphic)
                        // CurrentMap.GraphicTopObjects.Remove(topGraphic);
                        CurrentMap.GraphicMiddleObjects = CurrentMap.GraphicMiddleObjects.Where(o => o.GraphicText != topGraphic.GraphicText && Math.Abs(o.PositionX - topGraphic.PositionX) > 0.5).ToList();
                    else
                        CurrentMap.Objects.Remove(gameObject);
                }
                break;
            case ResultAction.Nothing:
            default:
                break; 
        }
        if (interactCounter + 1 >= resultActions.Length && CurrentMap.Interactables.Any())
        {
            if (CurrentMap.Interactables[0] is Generic generic)
            {
                if (generic.IsJustOnce)
                    generic.IsInteractable = false;
            }
            CurrentMap.Interactables.RemoveAt(0);
            interactCounter = 0;
        }
        else
        {
            interactCounter++;
        }
    }
    public void MoveInventoryOnPlayerPosition()
    {
        int difference = 0;
        if (_playerOne.PositionX > ScreenSize.X / 2 && InventoryWindow.location == Direction.Right)
        {
            difference = -(int)ScreenSize.X / 2;
            InventoryWindow.location = Direction.Left;
        }
        else if(_playerOne.PositionX < ScreenSize.X / 2 && InventoryWindow.location == Direction.Left)
        {
            difference = (int)ScreenSize.X / 2;
            InventoryWindow.location = Direction.Right;
        }
        InventoryWindow.PositionX += difference;
        InventoryWindow.Title = InventoryWindow.Title.Select(c => { c.PositionX += difference; return c; });
        InventoryWindow.Description = InventoryWindow.Description?.Select(c => { c.PositionX += difference; return c; }).ToList();
        InventoryWindow.Inventory = InventoryWindow.Inventory.Select(i => { i.PositionX += difference; return i; }).ToList();
    }
    public void AddToInventory(GameItem item)
    {
        item.UpdatePosition(InventoryWindow.Inventory.Count);
        item.PositionX = item.ItemPositionX + InventoryWindow.PositionX;
        item.PositionY = item.ItemPositionY + InventoryWindow.PositionY;
        InventoryWindow.Inventory.Add(item);
    }
    public void AddToInventory(IEnumerable<GameItem> items)
    {
        foreach (var gameItem in items)
        {
            AddToInventory(gameItem);
        }
    }
    public bool ChangeMapIfNecessary(PlayerState playerState)
    {
        int precisionX = playerState is PlayerState.Up or PlayerState.Down ? 30 : 15;
        int precisionY = playerState is PlayerState.Left or PlayerState.Right ? 30 : 15; 
        foreach (var exit in CurrentMap.MapExits.Keys)
        {
            if (Math.Abs(_playerOne.Middle.X - exit.X) < precisionX && Math.Abs(_playerOne.Middle.Y - exit.Y) < precisionY)
            {
                var lastMap = CurrentMap;
                CurrentMap = CurrentMap.MapExits[exit].Item1??CurrentMap;
                CurrentMap.GetEntryLocation(lastMap.MapExits[exit].Item2);
                _playerOne.PositionX = CurrentMap.EntryLocation.X;
                _playerOne.PositionY = CurrentMap.EntryLocation.Y;
                return true;
            }
        }

        return false;
    }

    private bool CheckColission(IMovable movable, GameObject gameObject, Direction direction, float deltaTime)
    {
        float speed = movable.Speed * deltaTime;
        GameObject obj = (GameObject)movable;
        switch (direction)
        {
            case Direction.Right:
                if (gameObject.Top <= obj.Bottom 
                    && gameObject.Bottom >= obj.Top + _topMargin
                    && gameObject.Left >= obj.Right 
                    && obj.Right + speed > gameObject.Left)
                    return false;
                break;
            case Direction.Left:
                if (gameObject.Top <= obj.Bottom 
                    && gameObject.Bottom >= obj.Top + _topMargin
                    && gameObject.Right <= obj.Left 
                    && obj.Left - speed < gameObject.Right)
                    return false;
                break;
            case Direction.Up:
                if (gameObject.Bottom <= obj.Top + _topMargin
                    && gameObject.Bottom >= obj.Top + _topMargin - speed 
                    && gameObject.Left <= obj.Right 
                    && gameObject.Right >= obj.Left)
                    return false;
                break;
            case Direction.Down:
                if (gameObject.Top >= obj.Bottom 
                    && gameObject.Top <= obj.Bottom + speed 
                    && gameObject.Left <= obj.Right 
                    && gameObject.Right >= obj.Left)
                    return false;
                break;
        }

        return true;
    }

    public Direction GetRandomDirection()
    {
        return _random.Next(5) switch
        {
            0 => Direction.Up,
            1 => Direction.Down,
            2 => Direction.Left,
            3 => Direction.Right,
            _ => Direction.Neutral
        };
    }
}