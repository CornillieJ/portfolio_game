using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Portfolio_Game_Core;
using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Interfaces;
using Portfolio_Game_Core.Services;

namespace Portfolio_Game;

public class Game1 : Game
{
    private GameService _gameService;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

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
        _gameService._playerOne.Texture = Content.Load<Texture2D>("character");
        _gameService.Objects.First().Texture = Content.Load<Texture2D>("objects");
        foreach (var floor in _gameService.Floors)
        {
            floor.Texture = Content.Load<Texture2D>("inner");
        }

        foreach (var graphicObject in _gameService.GraphicObjects)
        {
            graphicObject.Texture = Content.Load<Texture2D>("inner");
        }
    }

    protected override void Update(GameTime gameTime)
    {
        var kstate = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            kstate.IsKeyDown(Keys.Escape))
            Exit();

        Player player = _gameService._playerOne;
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
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    private void DrawObject(GameObject gameObject)
    {
        Texture2D texture= gameObject.Texture;
        Vector2 vector2 = new(gameObject.PositionX, gameObject.PositionY);
        var sourceRectangle = gameObject.CurrentSprite;
        var color = Color.White;
        _spriteBatch.Draw(texture,vector2,sourceRectangle,color); 
    }
}