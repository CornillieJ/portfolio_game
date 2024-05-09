using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Data;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Services;

public class GameService
{
    private int _topMargin = 40;
    private List<GameObject> _objects;
    private List<GameObject> _graphicObjects;
    private List<GameObject> _floors;
    private List<Window> _windows;

    private WindowCreator _windowCreator;
    public Player _playerOne { get; set; }
    public Vector2 ScreenSize { get; }
    public IEnumerable<GameObject> Objects => _objects.AsReadOnly();
    public IEnumerable<GameObject> GraphicObjects => _graphicObjects.AsReadOnly();
    public IEnumerable<GameObject> Floors => _floors.AsReadOnly();
    public IEnumerable<Window> Windows => _windows.AsReadOnly();
    public GameService(int screenWidth, int screenHeight)
    {
        _windowCreator = new WindowCreator(screenWidth,screenHeight);
        _playerOne = new Player(0, 0);
        _objects = new List<GameObject>();
        _graphicObjects = new List<GameObject>();
        _floors = new List<GameObject>();
        _windows = new List<Window>();
        ScreenSize = new Vector2(screenWidth,screenHeight);
        GetFloor();
        SeedGraphicObjects();
        SeedStartText();
    }

    public void AddObject(GameObject gameObject)
    {
        if (_objects.Contains(gameObject)) return;
       _objects.Add(gameObject);
    }
    public void RemoveObject(GameObject gameObject)
    { 
        if (_objects.Contains(gameObject)) return; 
        _objects.Remove(gameObject);
    }
    public void AddGraphicObject(GameObject gameObject)
    {
        if (_graphicObjects.Contains(gameObject)) return;
        _graphicObjects.Add(gameObject);
    }
    public void RemoveGraphicObject(GameObject gameObject)
    { 
        if (_graphicObjects.Contains(gameObject)) return; 
        _graphicObjects.Remove(gameObject);
    }
    public bool CanMove(IMovable movable, Direction direction, float deltaTime)
    {
        if (movable is not Player player) return false;
        float speed = player.Speed * deltaTime;
        foreach (var gameObject in _objects)
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

    private void GetFloor()
    {
        int width = Floor.FloorWidth; 
        int height = Floor.FloorHeight; 
        for (int i = 0; i < ScreenSize.Y; i++)
        {
            for (int j = 0; j < ScreenSize.X; j++)
            {
                _floors.Add(new Floor(j*width,i*height));
            }
        }
    }

    private void SeedGraphicObjects()
    {
        _graphicObjects.Add(new Carpet((int)(ScreenSize.X/2 - Carpet.carpetWidth/2),(int)(ScreenSize.Y/2 - Carpet.carpetHeight/2)));
    }

    private void SeedStartText()
    {
        _windows.AddRange(_windowCreator.GetTextWindows(new List<string>{ "Welcome"} , TextData.WelcomeTexts));
    }
    public void ShiftWindow()
    {
        _windows.RemoveAt(0);
    }
    public void RemoveWindow(Window window)
    {
        if (!_windows.Contains(window)) return;
        _windows.Remove(window);
    }

    public void AddWindow(Window window)
    {
        _windows.Add(window);
    }
}