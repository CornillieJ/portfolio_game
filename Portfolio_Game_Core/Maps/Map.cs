using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game_Core.Maps;

public abstract class Map
{
    public int Width { get; set; }
    public int Height { get; set; }
    
    public Dictionary<Vector2, (Map?,Direction)> MapExits = new();
    public List<GameObject> Objects { get; set; }
    public List<GameObject> GraphicObjects { get; set; }
    public List<IMovable> MovableObjects { get; set; }
    public List<TopGraphic> GraphicMiddleObjects { get; set; }
    public List<TopGraphic> GraphicTopObjects { get; set; }
    public List<Window> Windows { get; set; }
    public List<IInteractable> Interactables { get; set; }

    protected WindowCreator _windowCreator;
    protected Vector2 ScreenSize { get; set; }
    public Floor Floor { get; set; }
    public Vector2 EntryLocation { get; set; }
    public Texture2D Texture { get; set; }
    public bool IsTranslation { get; set; } = true;

    public Map(float screenWidth, float screenHeight, Direction entryDirection = Direction.Neutral)
    {
        ScreenSize = new Vector2(screenWidth, screenHeight);
        Objects = new List<GameObject>();
        GraphicObjects = new List<GameObject>();
        GraphicMiddleObjects = new List<TopGraphic>();
        GraphicTopObjects = new List<TopGraphic>();
        Windows = new List<Window>();
        Interactables = new List<IInteractable>();
        MovableObjects = new List<IMovable>();
        _windowCreator = new WindowCreator(screenWidth, screenHeight);
    }

    public Window GetTextWindow(string title, string content)
    {
       return _windowCreator.GetTextWindow(title, content);
    }
    public abstract void GetEntryLocation(Direction entryDirection);

    public abstract void SeedNextMaps();
    protected abstract void GetFloor();
    protected abstract void SeedGraphicObjects();
    protected abstract void SeedObjects();
    protected abstract void SeedStartText();
    protected abstract void SeedWalls();
}