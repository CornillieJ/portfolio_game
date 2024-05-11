using System.ComponentModel.Design;
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
    private int _topMargin = 35;
    private WindowCreator _windowCreator;
    public Player _playerOne { get; set; }
    public Vector2 ScreenSize { get; }
    public Map CurrentMap { get; set; }
    public InventoryWindow InventoryWindow { get; set; }

    public GameService(int screenWidth, int screenHeight)
    {
        ScreenSize = new Vector2(screenWidth, screenHeight);
        _windowCreator = new WindowCreator(screenWidth,screenHeight);
        _playerOne = new Player(0, 0);
        CurrentMap = new FirstMap(screenWidth,screenHeight);
        InventoryWindow = new InventoryWindow(screenWidth/2 + 50, screenHeight/2 - InventoryWindow.InventoryWindowHeight/2);
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
        if (movable is not Player player) return false;
        float speed = player.Speed * deltaTime;
        foreach (var gameObject in CurrentMap.Objects)
        {
            switch (direction)
            {
               case Direction.Right:
                   if (gameObject.Top <= player.Bottom 
                       && gameObject.Bottom >= player.Top + _topMargin
                       && gameObject.Left >= player.Right 
                       && player.Right + speed > gameObject.Left)
                       return false;
                   break;
               case Direction.Left:
                   if (gameObject.Top <= player.Bottom 
                       && gameObject.Bottom >= player.Top + _topMargin
                       && gameObject.Right <= player.Left 
                       && player.Left - speed < gameObject.Right)
                       return false;
                   break;
               case Direction.Up:
                   if (gameObject.Bottom <= player.Top + _topMargin
                       && gameObject.Bottom >= player.Top + _topMargin - speed 
                       && gameObject.Left <= player.Right 
                       && gameObject.Right >= player.Left)
                       return false;
                   break;
                  case Direction.Down:
                      if (gameObject.Top >= player.Bottom 
                          && gameObject.Top <= player.Bottom + speed 
                          && gameObject.Left <= player.Right 
                          && gameObject.Right >= player.Left)
                          return false;
                      break;
            }
        }

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
    public GameObject? GetObjectAtLocation(Vector2 location)
    {
        foreach (var gameObject in CurrentMap.Objects)
        {
            if (location.X >= gameObject.Left && location.X <= gameObject.Right && location.Y >= gameObject.Top && location.Y <= gameObject.Bottom)
                return gameObject;
        }

        return null;
    }

    public GameObject? GetObjectPlayerIsLookingAt()
    {
        Vector2 location = _playerOne.GetVisionCoordinate();
        return GetObjectAtLocation(location);
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
    public void InteractAll()
    {
        while(CurrentMap.Interactables.Any())
        {
            (string? title, string? content, GameItem? item) = CurrentMap.Interactables[0].Interact();
            if(title is not null && content is not null )
                CurrentMap.Windows.Add( _windowCreator.GetTextWindow(title,content));
            if (item is not null)
            {
                AddToInventory(item);
            }
            CurrentMap.Interactables.RemoveAt(0);
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
        item.InventoryNumber = InventoryWindow.Inventory.Count;
        item.PositionX = item.ItemPositionX + InventoryWindow.PositionX;
        item.PositionY = item.ItemPositionY + InventoryWindow.PositionY;
        InventoryWindow.Inventory.Add(item);
    }

    public void ChangeMapIfNecessary()
    {
        foreach (var exit in CurrentMap.MapExits.Keys)
        {
            if (Math.Abs(_playerOne.Middle.X - exit.X) < 25 && Math.Abs(_playerOne.Middle.Y - exit.Y) < 15)
            {
                var lastMap = CurrentMap;
                CurrentMap = CurrentMap.MapExits[exit].Item1??CurrentMap;
                CurrentMap.GetEntryLocation(lastMap.MapExits[exit].Item2);
                _playerOne.PositionX = CurrentMap.EntryLocation.X;
                _playerOne.PositionY = CurrentMap.EntryLocation.Y;
            }
            
        }
    }
}