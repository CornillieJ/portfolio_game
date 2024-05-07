using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Portfolio_Game_Core;
using Portfolio_Game_Core.Services;
using Rectangle = System.Drawing.Rectangle;

namespace Portfolio_Game;

public class Game1 : Game
{
    private GameService _gameService;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _gameService = new GameService();
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Player player = _gameService._playerOne; 
        player.PositionX = _graphics.PreferredBackBufferWidth / 2 - (player.Width/2);
        player.PositionY = _graphics.PreferredBackBufferHeight / 2 - (player.Height/2);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _gameService._playerOne.PlayerTexture = Content.Load<Texture2D>("character");
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
            if (_gameService.CanMove(player, Direction.Up))
                player.GoUp((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        if (kstate.IsKeyDown(Keys.S))
        {
            newPlayerState = PlayerState.Down;
            if (_gameService.CanMove(player, Direction.Down))
                player.GoDown((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        if (kstate.IsKeyDown(Keys.A))
        {
            newPlayerState = PlayerState.Left;
            if (_gameService.CanMove(player, Direction.Left)) 
                player.GoLeft((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        if (kstate.IsKeyDown(Keys.D))
        {
            newPlayerState = PlayerState.Right;
            if (_gameService.CanMove(player, Direction.Right))
                player.GoRight((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        player.PlayerState = newPlayerState; 
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        DrawPlayer(_gameService._playerOne);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawPlayer(Player player)
    {
        Texture2D texture= player.PlayerTexture;
        Vector2 vector2 = new(player.PositionX, player.PositionY);
        var sourceRectangle = player.currentSprite;
        var color = Color.White;
        _spriteBatch.Draw(texture,vector2,sourceRectangle,color); 
    }
}