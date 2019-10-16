using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicoParallax
{
   
    //basico fondo con dierentes capas que se mueven a diferente velocidad
    //dando la sensacion de movimiento
    //dependiendo del "actor", la velocidad de las capas podria alterarse en tiempo real
    //simulando aceleracion o freno

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        DesplazarBackground fondo_ciudad_1;
        DesplazarBackground fondo_ciudad_2;
        DesplazarBackground fondo_nubes_1;
        DesplazarBackground fondo_nubes_2;
        DesplazarBackground fondo_sol;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 288;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 512;   // set this value to the desired height of your window
            graphics.ApplyChanges();
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

            // TODO: use this.Content to load your game content here
            fondo_ciudad_1 = new DesplazarBackground(Content.Load<Texture2D>("flp_back_dark_1"), new Rectangle(0, 0, 288, 512));
            fondo_ciudad_2 = new DesplazarBackground(Content.Load<Texture2D>("flp_back_dark_1"), new Rectangle(288, 0, 288, 512));
            fondo_nubes_1 = new DesplazarBackground(Content.Load<Texture2D>("nubes1"), new Rectangle(288, 0, 288, 512));
            fondo_nubes_2 = new DesplazarBackground(Content.Load<Texture2D>("nubes2"), new Rectangle(288, 0, 288, 512));
            fondo_sol = new DesplazarBackground(Content.Load<Texture2D>("sol"), new Rectangle(288, 0, 288, 512));
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
            if (fondo_ciudad_1.rectangulo.X + fondo_ciudad_1.textura.Width <= 0)
                fondo_ciudad_1.rectangulo.X = fondo_ciudad_2.rectangulo.X + fondo_ciudad_2.textura.Width;

            if (fondo_ciudad_2.rectangulo.X + fondo_ciudad_2.textura.Width <= 0)
                fondo_ciudad_2.rectangulo.X = fondo_ciudad_1.rectangulo.X + fondo_ciudad_2.textura.Width;

            if (fondo_nubes_1.rectangulo.X + fondo_nubes_1.textura.Width <= 0)
                fondo_nubes_1.rectangulo.X = 300;
            if (fondo_nubes_2.rectangulo.X + fondo_nubes_2.textura.Width <= 0)
                fondo_nubes_2.rectangulo.X = 300;
            if (fondo_sol.rectangulo.X + fondo_sol.textura.Width <= 0)
                fondo_sol.rectangulo.X = 300;


            fondo_ciudad_1.Update(2);
            fondo_ciudad_2.Update(2);
            fondo_nubes_1.Update(1);
            fondo_nubes_2.Update(3);
            fondo_sol.Update(2);


            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            fondo_ciudad_1.Draw(spriteBatch);
            fondo_ciudad_2.Draw(spriteBatch);
            fondo_sol.Draw(spriteBatch);
            fondo_nubes_1.Draw(spriteBatch);
            fondo_nubes_2.Draw(spriteBatch);
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
 