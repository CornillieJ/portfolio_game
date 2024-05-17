using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Portfolio_Game_Core;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Base;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Entities.Items;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Maps;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game;

public class Game1 : Game
{
    float _zoomFactor = 1.0f;
    private float _lastScrollWheelValue = 0;
    private GameService _gameService;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private bool _isActionPressed = false;
    private bool _isInventoryPressed = false;
    private bool _leftClicked = false;
    private bool _rightClicked = false;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        MapService.SeedMaps(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _gameService = new GameService(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Player player = _gameService._playerOne;
        player.PositionX = _graphics.PreferredBackBufferWidth / 2 - (player.Width / 2);
        player.PositionY = _graphics.PreferredBackBufferHeight / 2 - (player.Height / 2);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        foreach (var gameServiceObject in _gameService.CurrentMap.Objects)
        {
            if (gameServiceObject is not IVisible iVisible) continue;
            iVisible.SetStaticTexture(Content.Load<Texture2D>("objects"));
        }

        foreach (var graphicObject in _gameService.CurrentMap.GraphicObjects)
        {
            if (graphicObject is not IVisible iVisible) continue;
            iVisible.SetStaticTexture(Content.Load<Texture2D>("inner"));
        }

        Player.Texture = Content.Load<Texture2D>("character");
        // FloorTile.Texture = Content.Load<Texture2D>("inner");
        
        MapService.Maps["house-entry"].Texture = Content.Load<Texture2D>("houseentry");
        MapService.Maps["bathroom"].Texture = Content.Load<Texture2D>("houseentry");
        MapService.Maps["garden"].Texture = Content.Load<Texture2D>("overworldtemp");
        Wall.Texture = Content.Load<Texture2D>("inner");
        WindowInWall.Texture = Content.Load<Texture2D>("windowinwallsmall");
        TextWindow.Texture = Content.Load<Texture2D>("window200");
        InventoryWindow.Texture = Content.Load<Texture2D>("inventorywindow");
        Tankie.Texture = Content.Load<Texture2D>("tank");
        Text.Texture = Content.Load<Texture2D>("font2");
        foreach (TopGraphic graphicTopObject in _gameService.CurrentMap.GraphicTopObjects)
        {
           graphicTopObject.Texture = Content.Load<Texture2D>(graphicTopObject.GraphicText);
        }
    }
    protected override void Update(GameTime gameTime)
    {
        var kstate = Keyboard.GetState();
        var mstate = Mouse.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            kstate.IsKeyDown(Keys.Escape))
            Exit();
        if (!kstate.IsKeyDown(Keys.Space) && !kstate.IsKeyDown(Keys.Enter))
        {
            _isActionPressed = false;
        }

        if (_gameService.CurrentMap.Windows.OfType<TextWindow>().Any())
        {
            if (_isActionPressed) return;
            if (kstate.IsKeyDown(Keys.Space) || kstate.IsKeyDown(Keys.Enter))
            {
                _isActionPressed = true;
                _gameService.ShiftWindow();
            }

            base.Update(gameTime);
            return;
        }

        //Only check input up to here if there are open text windows (no walking allowed if open)
        MovePlayerOnInput(gameTime, kstate);
        if ((kstate.IsKeyDown(Keys.Space) || kstate.IsKeyDown(Keys.Enter)) && _isActionPressed == false)
        {
            _isActionPressed = true;
            var gameObjectInView = _gameService.GetObjectPlayerIsLookingAt();
            if (gameObjectInView is IInteractable interactable)
                _gameService.AddInteraction(interactable);
        }

        if ((kstate.IsKeyDown(Keys.I) || kstate.IsKeyDown(Keys.Tab)) && _isInventoryPressed == false)
        {
            _isInventoryPressed = true;
            _gameService.InventoryWindow.IsOpen = !_gameService.InventoryWindow.IsOpen;
        }

        if (!kstate.IsKeyDown(Keys.I) && !kstate.IsKeyDown(Keys.Tab))
        {
            _isInventoryPressed = false;
        }

        if (mstate.LeftButton == ButtonState.Pressed && _leftClicked == false)
        {
            if (GetItemUnderMouse(mstate) is GameItem item)
                _gameService.AddTextWindow("Description", item.Description);
        }

        if (mstate.RightButton == ButtonState.Pressed && _rightClicked == false)
        {
            if (GetItemUnderMouse(mstate) is ProgramItem program)
                RunProgram(program.ProgramPath);
        }

        if (kstate.IsKeyDown(Keys.LeftControl) && mstate.ScrollWheelValue > _lastScrollWheelValue)
        {
            _zoomFactor += _zoomFactor >= 2F ? 0 : 0.1F;
        }

        if (kstate.IsKeyDown(Keys.LeftControl) && mstate.ScrollWheelValue < _lastScrollWheelValue)
        {
            _zoomFactor -= _zoomFactor <= 1F ? 0 : 0.1F;
        }

        _lastScrollWheelValue = mstate.ScrollWheelValue;
        _leftClicked = mstate.LeftButton == ButtonState.Pressed;
        _rightClicked = mstate.RightButton == ButtonState.Pressed;
        _gameService.InteractAll();
        _gameService.MoveInventoryOnPlayerPosition();
        base.Update(gameTime);
    }

    private GameItem GetItemUnderMouse(MouseState mstate)
    {
        if (_gameService.InventoryWindow.IsOpen)
        {
            foreach (var item in _gameService.InventoryWindow.Inventory)
            {
                if (mstate.X >= item.Left && mstate.X <= item.Right
                                          && mstate.Y >= item.Top && mstate.Y <= item.Bottom)
                {
                    return item;
                }
            }
        }

        return null;
    }

    protected override void Draw(GameTime gameTime)
    {
        float translateX = GraphicsDevice.Viewport.Width / 2 - (_gameService._playerOne.Middle.X * _zoomFactor);
        float translateY = GraphicsDevice.Viewport.Height / 2 - (_gameService._playerOne.Middle.Y * _zoomFactor);
        translateX = Math.Min(translateX, 0);
        translateX = Math.Max(translateX,
            -(_gameService.CurrentMap.Width * _zoomFactor - GraphicsDevice.Viewport.Width));
        translateY = Math.Min(translateY, 0);
        translateY = Math.Max(translateY,-(_gameService.CurrentMap.Height * _zoomFactor - GraphicsDevice.Viewport.Height));
        Matrix zoomMatrix;
        zoomMatrix = Matrix.CreateScale(_zoomFactor) * Matrix.CreateTranslation(translateX, translateY, 0);
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(transformMatrix: zoomMatrix);
        DrawMap(_gameService.CurrentMap);
        DrawGraphicObjects();
        DrawGameObjects();
        DrawObject(_gameService._playerOne);
        DrawTopGraphicObjects();
        _spriteBatch.End();
        _spriteBatch.Begin();
        DrawWindows();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawGameObjects()
    {
        foreach (var gameObject in _gameService.CurrentMap.Objects)
        {
            DrawObject(gameObject);
        }
    }

    private void DrawGraphicObjects()
    {
        foreach (var graphicObject in _gameService.CurrentMap.GraphicObjects)
        {
            DrawObject(graphicObject);
        }
    }
    private void DrawTopGraphicObjects()
    {
        foreach (var graphicTopObject in _gameService.CurrentMap.GraphicTopObjects)
        {
            DrawObject(graphicTopObject);
        }
    }
    private void DrawMap(Map map)
    {
        _spriteBatch.Draw(map.Texture
            , new Vector2(0, 0)
            , Color.White);
    }

    private void DrawWindows()
    {
        var windows = _gameService.CurrentMap.Windows.ToArray();
        for (int i = windows.Length - 1; i >= 0; i--)
        {
            DrawObject(windows[i]);
            if (windows[i] is TextWindow textWindow)
            {
                DrawObjects(textWindow.Title);
                DrawObjects(textWindow.Content);
            }
        }

        if (windows.Any()) return;
        if (!_gameService.InventoryWindow.IsOpen) return;
        DrawObject(_gameService.InventoryWindow);
        DrawObjects(_gameService.InventoryWindow.Title);
        DrawInventory();
    }

    private void DrawInventory()
    {
        foreach (var item in _gameService.InventoryWindow.Inventory)
        {
            DrawObject(item);
        }
    }

    private void DrawText(IEnumerable<GameObject> gameObjects, float scale)
    {
        foreach (var gameObject in gameObjects)
        {
            DrawObjectWithScale(gameObject, scale);
        }
    }

    private void DrawObjects(IEnumerable<GameObject> gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            DrawObject(gameObject);
        }
    }
    private void DrawObject(TopGraphic topGraphic)
    {
        _spriteBatch.Draw(topGraphic.Texture
            , new Vector2(topGraphic.PositionX, topGraphic.PositionY)
            , topGraphic.CurrentSprite
            , Color.White);
    }
    private void DrawObject(GameObject gameObject)
    {
        if (gameObject is not IVisible visible) return;
        _spriteBatch.Draw(visible.GetStaticTexture()
            , new Vector2(gameObject.PositionX, gameObject.PositionY)
            , gameObject.CurrentSprite
            , Color.White);
    }

    private void DrawObjectWithScale(GameObject gameObject, float scale)
    {
        if (gameObject is not IVisible visible) return;
        _spriteBatch.Draw(
            visible.GetStaticTexture(),
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

    private void DrawObject(GameItem gameItem)
    {
        if (gameItem is not IVisible visible) return;
        _spriteBatch.Draw(visible.GetStaticTexture()
            , new Vector2(gameItem.PositionX, gameItem.PositionY)
            , gameItem.CurrentSprite
            , Color.White);
    }

    private void MovePlayerOnInput(GameTime gameTime, KeyboardState kstate)
    {
        PlayerState newPlayerState = PlayerState.Neutral;
        if (kstate.IsKeyDown(Keys.W))
        {
            newPlayerState = PlayerState.Up;
            bool canMove = _gameService.CanMove(_gameService._playerOne, Direction.Up,
                (float)gameTime.ElapsedGameTime.TotalSeconds);
            Direction secondDirection = Direction.Neutral;
            if (kstate.IsKeyDown(Keys.A) && _gameService.CanMove(_gameService._playerOne, Direction.Left,
                    (float)gameTime.ElapsedGameTime.TotalSeconds)) secondDirection = Direction.Left;
            if (kstate.IsKeyDown(Keys.D) && _gameService.CanMove(_gameService._playerOne, Direction.Right,
                    (float)gameTime.ElapsedGameTime.TotalSeconds)) secondDirection = Direction.Right;
            _gameService._playerOne.GoUp((float)gameTime.ElapsedGameTime.TotalSeconds, canMove, secondDirection);
        }
        else if (kstate.IsKeyDown(Keys.S))
        {
            newPlayerState = PlayerState.Down;
            bool canMove = _gameService.CanMove(_gameService._playerOne, Direction.Down,
                (float)gameTime.ElapsedGameTime.TotalSeconds);
            Direction secondDirection = Direction.Neutral;
            if (kstate.IsKeyDown(Keys.A) && _gameService.CanMove(_gameService._playerOne, Direction.Left,
                    (float)gameTime.ElapsedGameTime.TotalSeconds)) secondDirection = Direction.Left;
            if (kstate.IsKeyDown(Keys.D) && _gameService.CanMove(_gameService._playerOne, Direction.Right,
                    (float)gameTime.ElapsedGameTime.TotalSeconds)) secondDirection = Direction.Right;
            _gameService._playerOne.GoDown((float)gameTime.ElapsedGameTime.TotalSeconds, canMove, secondDirection);
        }
        else if (kstate.IsKeyDown(Keys.A))
        {
            newPlayerState = PlayerState.Left;
            bool canMove = _gameService.CanMove(_gameService._playerOne, Direction.Left,
                (float)gameTime.ElapsedGameTime.TotalSeconds);
            _gameService._playerOne.GoLeft((float)gameTime.ElapsedGameTime.TotalSeconds, canMove);
        }
        else if (kstate.IsKeyDown(Keys.D))
        {
            newPlayerState = PlayerState.Right;
            bool canMove = _gameService.CanMove(_gameService._playerOne, Direction.Right,
                (float)gameTime.ElapsedGameTime.TotalSeconds);
            _gameService._playerOne.GoRight((float)gameTime.ElapsedGameTime.TotalSeconds, canMove);
        }

        _gameService._playerOne.PlayerState = newPlayerState;
        if (newPlayerState != PlayerState.Neutral)
        {
            bool isNewMap = _gameService.ChangeMapIfNecessary(newPlayerState);
            if(isNewMap) LoadContent();
        }
    }

    private void RunProgram(string programPath)
    {
        try
        {
            Process.Start(programPath);
        }
        catch (Exception ex)
        {
            _gameService.AddTextWindow("Error", string.Concat("Could not start program: ", ex.Message.AsSpan(0, 20)));
        }
    }
}