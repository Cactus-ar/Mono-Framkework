using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Engine;

namespace SpaceInvaders
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            Cargador.Init(this);
            Manejador.anchoPantalla = graphics.PreferredBackBufferWidth;
            Manejador.altoPantalla = graphics.PreferredBackBufferHeight;

            ManejadorEscenas.CargarEscenas();
            ManejadorEscenas.Cambiar(Manejador.primerEscena);
            
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            Manejador.tiempoDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Entrada.ActualizarEntrada();
            ManejadorEscenas.Actualizar();
            IsMouseVisible = Manejador.MostrarCursor;

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            ManejadorEscenas.Dibujar(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
