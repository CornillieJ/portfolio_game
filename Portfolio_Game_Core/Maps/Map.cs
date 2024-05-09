using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game_Core.Maps;

public class Map
{
    public List<GameObject> Objects{get; set;}
    public List<GameObject> GraphicObjects{get; set;}
    public List<GameObject> Floors{get; set;}
    public List<Window> Windows{get; set;}
    public List<IInteractable> Interactables{get; set;}

    public WindowCreator _windowCreator;
    public Vector2 ScreenSize { get; }

    public Map(int screenWidth, int screenHeight)
    {
        ScreenSize = new Vector2(screenWidth, screenHeight);
        Objects = new List<GameObject>();
        GraphicObjects = new List<GameObject>();
        Floors = new List<GameObject>();
        Windows = new List<Window>();
        Interactables = new List<IInteractable>();
        _windowCreator = new WindowCreator(screenWidth,screenHeight);
    }
}