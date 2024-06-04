﻿using System;
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
using Portfolio_Game.Helpers;

namespace Portfolio_Game;

public class Game1 : Game
{
    float _zoomFactor = 1.0f;
    float _resolutionScaleFactor = 1.0f;
    private Texture2D _debugTexture; 
    private float _lastScrollWheelValue = 0;
    private GameService _gameService;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private bool _isActionPressed = false;
    private bool _isFullscreenPressed = false;
    private bool _isInventoryPressed = false;
    private bool _leftClicked = false;
    private bool _rightClicked = false;
    private int _interactCounter = 0;
    private Vector2 _initialPlayerPosition = new(0, 0);
    private DrawHelper _drawHelper;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);        
        // _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        _resolutionScaleFactor = GraphicsDevice.Viewport.Width / 800F;
        //ScaleWindows();
        _zoomFactor = _resolutionScaleFactor;
        _zoomFactor = Math.Min(_zoomFactor,2);
        _zoomFactor = Math.Max(_zoomFactor, 0.9F);
        MapService.SeedMaps(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _gameService = new GameService(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        Player player = _gameService._playerOne;
        player.PositionX = _gameService.CurrentMap.Width / 2 - (player.Width / 2);
        player.PositionY = _gameService.CurrentMap.Height / 2 - (player.Height / 2);
        base.Initialize();
    }
    private void ScaleWindows()
    {
        WindowCreator.WindowMargin = (int)(WindowCreator.WindowMargin * _resolutionScaleFactor);
        TextWindow.TextWindowWidth = (int)(TextWindow.TextWindowWidth * _resolutionScaleFactor);
        Portfolio_Game_Core.Entities.Window.LineMarginY = (int)(Portfolio_Game_Core.Entities.Window.LineMarginY * _resolutionScaleFactor);
        Portfolio_Game_Core.Entities.Window.TitleMarginX = (int)(Portfolio_Game_Core.Entities.Window.TitleMarginX * _resolutionScaleFactor);
        Portfolio_Game_Core.Entities.Window.TitleMarginY = (int)(Portfolio_Game_Core.Entities.Window.TitleMarginY * _resolutionScaleFactor);
        InventoryWindow.InventoryWindowHeight = (int)(InventoryWindow.InventoryWindowHeight * _resolutionScaleFactor);
        InventoryWindow.InventoryWindowWidth = (int)(InventoryWindow.InventoryWindowWidth * _resolutionScaleFactor);
        // TextWindow.TextWindowHeight = (int)(TextWindow.TextWindowHeight * _resolutionScaleFactor);
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _drawHelper = new DrawHelper(_gameService,_spriteBatch);
        _debugTexture = new Texture2D(GraphicsDevice, 1, 1);
        _debugTexture.SetData(new[] { Color.Red });
        foreach (var gameServiceObject in _gameService.CurrentMap.Objects)
        {
            if (gameServiceObject is not IVisible iVisible) continue;
            if (gameServiceObject is Generic generic)
            {
                generic.Texture = Content.Load<Texture2D>(generic.GraphicText);
            }
            else
            {
                iVisible.SetTexture(Content.Load<Texture2D>("objects"));
            }
        }
        foreach (var graphicObject in _gameService.CurrentMap.GraphicObjects)
        {
            if (graphicObject is not IVisible iVisible) continue;
            iVisible.SetTexture(Content.Load<Texture2D>("inner"));
        }
        foreach (TopGraphic graphicTopObject in _gameService.CurrentMap.GraphicTopObjects)
        {
            graphicTopObject.Texture = Content.Load<Texture2D>(graphicTopObject.GraphicText);
        }
        Player.Texture = Content.Load<Texture2D>("character");
        MapService.Maps["house-entry"].Texture = Content.Load<Texture2D>("houseentry");
        MapService.Maps["bathroom"].Texture = Content.Load<Texture2D>("houseentry");
        MapService.Maps["garden"].Texture = Content.Load<Texture2D>("overworldtemp");
        Wall.Texture = Content.Load<Texture2D>("inner");
        WindowInWall.Texture = Content.Load<Texture2D>("windowinwallsmall");
        TextWindow.Texture = Content.Load<Texture2D>("window200");
        InventoryWindow.Texture = Content.Load<Texture2D>("inventorywindow");
        Tankie.Texture = Content.Load<Texture2D>("tank");
        OuterWilds.Texture = Content.Load<Texture2D>("outerwilds");
        Text.Texture = Content.Load<Texture2D>("font2");
 
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
        if (!kstate.IsKeyDown(Keys.LeftAlt) && !kstate.IsKeyDown(Keys.Enter))
        {
            _isFullscreenPressed = false;
        }
        if (kstate.IsKeyDown(Keys.LeftAlt) && kstate.IsKeyDown(Keys.Enter) && _isFullscreenPressed == false)
        {
            
            _isFullscreenPressed = true;
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
        if (_gameService.CurrentMap.Interactables.Any())
        {
            _gameService.InteractAll(ref _interactCounter, ref _initialPlayerPosition);
            return;
        }
        _interactCounter = 0;
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
            var item = GetItemUnderMouse(mstate); 
            if ( item is ProgramItem program)
                RunProgram(program.ProgramPath);
            if (item is Website website)
                OpenWebsite(website.ProgramPath);
        }

        if (kstate.IsKeyDown(Keys.LeftControl) && mstate.ScrollWheelValue > _lastScrollWheelValue)
        {
            _zoomFactor += _zoomFactor >= 2F ? 0 : 0.1F;
        }

        if (kstate.IsKeyDown(Keys.LeftControl) && mstate.ScrollWheelValue < _lastScrollWheelValue)
        {
            _zoomFactor -= _zoomFactor <= 0.9F ? 0 : 0.1F;
        }

        _lastScrollWheelValue = mstate.ScrollWheelValue;
        _leftClicked = mstate.LeftButton == ButtonState.Pressed;
        _rightClicked = mstate.RightButton == ButtonState.Pressed;
        _gameService.MoveInventoryOnPlayerPosition();
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        float translateX = GraphicsDevice.Viewport.Width / 2 - (_gameService._playerOne.Middle.X * _zoomFactor);
        float translateY = GraphicsDevice.Viewport.Height / 2 - (_gameService._playerOne.Middle.Y * _zoomFactor);
        translateX = Math.Min(translateX, 0);
        translateX = Math.Max(translateX, -(_gameService.CurrentMap.Width * _zoomFactor - GraphicsDevice.Viewport.Width));
        translateY = Math.Min(translateY, 0);
        translateY = Math.Max(translateY,-(_gameService.CurrentMap.Height * _zoomFactor - GraphicsDevice.Viewport.Height));
        if(!_gameService.CurrentMap.IsTranslation)
        {
            translateX = GraphicsDevice.Viewport.Width / 2 - (_gameService.CurrentMap.Width/2 * _zoomFactor);
            translateY = GraphicsDevice.Viewport.Height / 2 - (_gameService.CurrentMap.Height/2 * _zoomFactor);
        }
        Matrix zoomMatrix = Matrix.CreateScale(_zoomFactor) * Matrix.CreateTranslation(translateX, translateY, 0);
        Matrix windowMatrix = Matrix.CreateScale(_resolutionScaleFactor, 1, 1);
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(transformMatrix: zoomMatrix);
        _drawHelper.DrawMap(_gameService.CurrentMap);
        _drawHelper.DrawGraphicObjects();
        _drawHelper.DrawGameObjects();
        _drawHelper.DrawObject(_gameService._playerOne);
        DrawTopGraphicObjects();
        // ShowExits();
        _spriteBatch.End();
        _spriteBatch.Begin();
        _drawHelper.DrawWindows();
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    public void DrawTopGraphicObjects()
    {
        foreach (var graphicTopObject in _gameService.CurrentMap.GraphicTopObjects)
        {
            DrawObject(graphicTopObject);
        }
    }
    private void DrawObject(TopGraphic topGraphic)
    {
        topGraphic.Texture ??= Content.Load<Texture2D>(topGraphic.GraphicText);
        _spriteBatch.Draw(topGraphic.Texture
            , new Vector2(topGraphic.PositionX, topGraphic.PositionY)
            , topGraphic.CurrentSprite
            , Color.White);
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
    private void OpenWebsite(string programPath)
    {
        try
        {
            Process.Start(@"cmd.exe ", @"/c " + programPath);
        }
        catch (Exception ex)
        {
            _gameService.AddTextWindow("Error", string.Concat("Could not start program: ", ex.Message.AsSpan(0, 20)));
        }
    }
    private void ShowExits()
    {
        foreach (var exit in _gameService.CurrentMap.MapExits.Keys)
        {
            _spriteBatch.Draw(_debugTexture, new Rectangle((int)exit.X-5, (int)exit.Y-5, 10, 10), Color.Red);
        }
    }    
}