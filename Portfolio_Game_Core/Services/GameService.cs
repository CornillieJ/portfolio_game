﻿using Microsoft.Xna.Framework;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Services;

public class GameService
{
    private List<GameObject> _objects;
    public Player _playerOne { get; set; }
    private int step;
    public IEnumerable<GameObject> Objects
    {
        get => _objects.AsReadOnly();
    }
    
    public GameService()
    {
        _playerOne = new Player(0, 0);
        _objects = new List<GameObject>();
    }
    public void AddObject(GameObject gameObject)
    {
        if (_objects.Contains(gameObject)) return;
       _objects.Add(gameObject);
    }
    public void RemoveObject(GameObject gameObject)
    { 
        if (_objects.Contains(gameObject)) return; 
        _objects.Add(gameObject);
    }

    public bool CanMove(IMovable movable, Direction direction)
    {
        if (movable is not Player player) return false;
        foreach (var gameObject in _objects)
        {
            switch (direction)
            {
               case Direction.Right:
                   if (gameObject.Top <= player.Bottom 
                       && gameObject.Bottom >= player.Top
                       && gameObject.Left >= player.Right 
                       && player.Right + player.Speed > gameObject.Left)
                       return false;
                   break;
               case Direction.Left:
                   if (gameObject.Top <= player.Bottom 
                       && gameObject.Bottom >= player.Top
                       && gameObject.Right <= player.Left 
                       && player.Left - player.Speed < gameObject.Right)
                       return false;
                   break;
               case Direction.Up:
                   if (gameObject.Bottom <= player.Top 
                       && gameObject.Bottom >= player.Top - player.Speed 
                       && gameObject.Left <= player.Right 
                       && gameObject.Right >= player.Left)
                       return false;
                   break;
                  case Direction.Down:
                      if (gameObject.Top >= player.Bottom 
                          && gameObject.Top <= player.Bottom + player.Speed 
                          && gameObject.Left <= player.Right 
                          && gameObject.Right >= player.Left)
                          return false;
                      break;
            }
        }

        return true;
    }
}