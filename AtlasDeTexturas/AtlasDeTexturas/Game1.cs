using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



//Utilizacion de un atlas para la animacion de un sprite basico.
//la imagen no es la mejor, pero se puede ver el ejemplo facilmente
//dentro de la clase se puede jugar con el intervalo de tiempo para visualizar mejor 

namespace AtlasDeTexturas
{
 
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteAnimado spriteAnimado;

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
            // TODO: use this.Content to load your game content here

            Texture2D actor = Content.Load<Texture2D>("actor_camina_2");
            spriteAnimado = new SpriteAnimado(actor, 2, 3);

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
            spriteAnimado.Update(gameTime);

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Vector2 posicion = new Vector2(230, 50);
            spriteAnimado.Draw(spriteBatch, posicion);

            base.Draw(gameTime);
        }
    }
}
