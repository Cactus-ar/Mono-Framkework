using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


//Minimo actor con movimiento de teclas y accion(disparo)

namespace ActorKeys
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Actor jugador = new Actor();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false, //modo ventana;
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 950
            };

            this.Window.Title = "ActorDemo";

            Content.RootDirectory = "Content";

        }

    
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

    
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            jugador.CargarContenido(Content);

            // TODO: use this.Content to load your game content here
        }

    
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

    
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            jugador.Actualizar();

            base.Update(gameTime);
        }

    
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            jugador.Dibujar(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
