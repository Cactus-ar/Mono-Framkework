using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Side scrolling shooter. Porque? porque eran "exoticos" cuando era chico
//y siempre me llamaron la atencion un poco más que los clasicos

namespace SpaceShoot
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Jugador jugador = new Jugador();
        FondoEstrellado fondo = new FondoEstrellado();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            this.Window.Title = "Shoot BunBun";
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

            fondo.CargarContenido(Content);
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
            fondo.Actualizar(gameTime);
            jugador.Actualizar(gameTime);

            base.Update(gameTime);
        }

    
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            fondo.Dibujar(spriteBatch);
            jugador.Dibujar(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
