using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Portfolio_Game_Core;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Entities.Graphical;
using Portfolio_Game_Core.Font;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game;

public class Game1 : Game
{
    private GameService _gameService;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private bool _isActionPressed = false;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _gameService = new GameService(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Player player = _gameService._playerOne; 
        player.PositionX = _graphics.PreferredBackBufferWidth / 2 - (player.Width/2);
        player.PositionY = _graphics.PreferredBackBufferHeight / 2 - (player.Height/2);
        _gameService.AddObject(new Chest(50,50));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Player.Texture = Content.Load<Texture2D>("character");
        Floor.Texture = Content.Load<Texture2D>("inner");
        TextWindow.Texture = Content.Load<Texture2D>("window200");
        Text.Texture = Content.Load<Texture2D>("font");
        foreach (var gameServiceObject in _gameService.Objects)
        {
            if(gameServiceObject is IVisible iVisible)
                iVisible.SetStaticTexture(Content.Load<Texture2D>("objects"));
        }
        foreach (var graphicObject in _gameService.GraphicObjects)
        {
            if(graphicObject is IVisible iVisible)
                iVisible.SetStaticTexture(Content.Load<Texture2D>("inner"));
        }
    }

    protected override void Update(GameTime gameTime)
    {
        Player player = _gameService._playerOne;
        var kstate = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            kstate.IsKeyDown(Keys.Escape))
            Exit();
        if (!kstate.IsKeyDown(Keys.Space) && !kstate.IsKeyDown(Keys.Enter))
        {
            _isActionPressed = false;
        }
        //Only check input up to here if there are open text windows (no walking)
        if (_gameService.Windows.OfType<TextWindow>().Any())
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
        PlayerState newPlayerState = PlayerState.Neutral;
        if (kstate.IsKeyDown(Keys.W))
        {
            newPlayerState = PlayerState.Up;
            bool canMove = _gameService.CanMove(player, Direction.Up, (float)gameTime.ElapsedGameTime.TotalSeconds);
            player.GoUp((float)gameTime.ElapsedGameTime.TotalSeconds, canMove);
        }
        if (kstate.IsKeyDown(Keys.S))
        {
            newPlayerState = PlayerState.Down;
            bool canMove = _gameService.CanMove(player, Direction.Down, (float)gameTime.ElapsedGameTime.TotalSeconds);
            player.GoDown((float)gameTime.ElapsedGameTime.TotalSeconds, canMove);
        }

        if (kstate.IsKeyDown(Keys.A))
        {
            newPlayerState = PlayerState.Left;
            bool canMove = _gameService.CanMove(player, Direction.Left,(float)gameTime.ElapsedGameTime.TotalSeconds);
            player.GoLeft((float)gameTime.ElapsedGameTime.TotalSeconds, canMove);
        }

        if (kstate.IsKeyDown(Keys.D))
        {
            newPlayerState = PlayerState.Right;
            bool canMove = _gameService.CanMove(player, Direction.Right,(float)gameTime.ElapsedGameTime.TotalSeconds);
            player.GoRight((float)gameTime.ElapsedGameTime.TotalSeconds, canMove);
        }
        player.PlayerState = newPlayerState; 
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        foreach (var floor in _gameService.Floors)
        {
            DrawObject(floor); 
        }
        foreach (var graphicObject in _gameService.GraphicObjects)
        {
           DrawObject(graphicObject); 
        }
        foreach (var gameObject in _gameService.Objects)
        {
            DrawObject(gameObject);
        }
        DrawObject(_gameService._playerOne);
        DrawWindows();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawWindows()
    {
        var windows = _gameService.Windows.ToArray();
        for(int i = windows.Length-1; i >= 0; i--)
        {
            DrawObject(windows[i]);
            if (windows[i] is TextWindow textWindow)
            {
                DrawObjects(textWindow._title);
                DrawObjects(textWindow.Content);
            }
        }
    }

    private void DrawObjects(IEnumerable<GameObject> gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
           DrawObject(gameObject); 
        }
    }
    private void DrawObject(GameObject gameObject)
    {
        if (gameObject is not IVisible iVisible) return;
        Texture2D texture= iVisible.GetStaticTexture();
        Vector2 vector2 = new(gameObject.PositionX, gameObject.PositionY);
        var sourceRectangle = gameObject.CurrentSprite;
        var color = Color.White;
        _spriteBatch.Draw(texture,vector2,sourceRectangle,color); 
    }
}