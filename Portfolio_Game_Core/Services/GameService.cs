using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Services;

public class GameService
{
    private List<Player> _players;
    private List<IGameObject> _objects;
    
    public IEnumerable<Player> Players
    {
        get => _players.AsReadOnly();
    }
    public IEnumerable<IGameObject> Objects
    {
        get => _objects.AsReadOnly();
    }
    
    public GameService()
    {
        _players = new List<Player>();
        _objects = new List<IGameObject>();
    }

    public void AddPlayer(Player player)
    {
        if (_players.Contains(player)) return;
       _players.Add(player);
    }
    public void RemovePlayer(Player player)
    {
        if (!_players.Contains(player)) return; 
        _players.Add(player);
    }
    public void AddObject(IGameObject gameObject)
    {
        if (_objects.Contains(gameObject)) return;
       _objects.Add(gameObject);
    }
    public void RemoveObject(IGameObject gameObject)
    { 
        if (_objects.Contains(gameObject)) return; 
        _objects.Add(gameObject);
    }
}