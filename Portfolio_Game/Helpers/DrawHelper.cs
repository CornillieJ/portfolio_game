using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Maps;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game.Helpers;

public class DrawHelper
{
    private const int FlickerTime = 80;
    private int _flickerCounter = 0;
    private GameService _gameService;
    private SpriteBatch _spriteBatch;

    public DrawHelper(GameService gameService, SpriteBatch spriteBatch) 
    {
        _gameService = gameService;
        _spriteBatch = spriteBatch;
    }
    public void DrawGameObjects()
    {
        foreach (var gameObject in _gameService.CurrentMap.Objects)
        {
            DrawObject(gameObject);
        }
    }

    public void DrawGraphicObjects()
    {
        foreach (var graphicObject in _gameService.CurrentMap.GraphicObjects)
        {
            DrawObject(graphicObject);
        }
    }
    public void DrawMap(Map map)
    {
        _spriteBatch.Draw(map.Texture
            , new Vector2(0, 0)
            , Color.White);
    }

    public void DrawWindows()
    {
        var windows = _gameService.CurrentMap.Windows.ToArray();
        for (int i = windows.Length - 1; i >= 0; i--)
        {
            DrawObject(windows[i]);
            if (windows[i] is TextWindow textWindow)
            {
                DrawObjects(textWindow.Title);
                DrawObjects(textWindow.Content);
                if (_flickerCounter >= FlickerTime)
                    DrawObjects(textWindow.Next);
                if (_flickerCounter >= FlickerTime * 2)
                    _flickerCounter = 0;
                _flickerCounter++;
            }
        }

        if (windows.Any()) return;
        if (!_gameService.InventoryWindow.IsOpen) return;
        DrawObject(_gameService.InventoryWindow);
        DrawObjects(_gameService.InventoryWindow.Title);
        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (var item in _gameService.InventoryWindow.Inventory)
        {
            DrawObject(item);
        }
    }

    public void DrawText(IEnumerable<GameObject> gameObjects, float scale)
    {
        foreach (var gameObject in gameObjects)
        {
            DrawObjectWithScale(gameObject, scale);
        }
    }

    public void DrawObjects(IEnumerable<GameObject> gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            DrawObject(gameObject);
        }
    }

    public void DrawPlayer(Texture2D head)
    {
        DrawObject(_gameService._playerOne);
        DrawHead(head);
    }
    public void DrawObject(GameObject gameObject)
    {
        if (gameObject is not IVisible visible) return;
        _spriteBatch.Draw(visible.GetTexture()
            , new Vector2(gameObject.PositionX, gameObject.PositionY)
            , gameObject.CurrentSprite
            , Color.White);
    }
    public void DrawHead(Texture2D head)
    {
        GameObject gameObject = _gameService._playerOne;
        _spriteBatch.Draw(head
            , new Vector2(gameObject.PositionX, gameObject.PositionY)
            , gameObject.CurrentSprite
            , Color.White);
    }

    public void DrawObjectWithScale(GameObject gameObject, float scale)
    {
        if (gameObject is not IVisible visible) return;
        _spriteBatch.Draw(
            visible.GetTexture(),
            new Vector2(gameObject.PositionX, gameObject.PositionY),
            null,
            Color.White,
            0f,
            new Vector2(0, 0),
            scale,
            SpriteEffects.None,
            0f
        );
    }

    public void DrawObject(GameItem gameItem)
    {
        if (gameItem is not IVisible visible) return;
        _spriteBatch.Draw(visible.GetTexture()
            , new Vector2(gameItem.PositionX, gameItem.PositionY)
            , gameItem.CurrentSprite
            , Color.White);
    } 
}