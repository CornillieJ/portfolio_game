using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Maps;

namespace Portfolio_Game_Core.Services;

public class GameService
{
    private int _topMargin = 40;
    private WindowCreator _windowCreator;
    public Player _playerOne { get; set; }
    public Vector2 ScreenSize { get; }
    public Map CurrentMap { get; set; }

    public GameService(int screenWidth, int screenHeight)
    {
        ScreenSize = new Vector2(screenWidth, screenHeight);
        _windowCreator = new WindowCreator(screenWidth,screenHeight);
        _playerOne = new Player(0, 0);
        CurrentMap = new FirstMap(screenWidth,screenHeight);
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
            var interactText = CurrentMap.Interactables[0].Interact();
            CurrentMap.Windows.Add( _windowCreator.GetTextWindow(interactText.Item1,interactText.Item2));
            CurrentMap.Interactables.RemoveAt(0);
        }
    }
}