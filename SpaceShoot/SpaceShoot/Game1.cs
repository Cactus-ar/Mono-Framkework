using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        List<Texture2D> texturas_rocas = new List<Texture2D>();
        List<Asteroide> rocas = new List<Asteroide>();

        Random random = new Random();
        


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

            //Cargar contenido de los asteroides
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_big1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_big2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_big3"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_big4"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_med1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_med3"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_small1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_small2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_tiny1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_tiny2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_big1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_big2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_big3"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_big4"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_med1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_med2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_small1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_small2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_tiny1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_tiny2"));



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
            CargarAsteroides();

            foreach (Asteroide roca in rocas)
            {
                roca.Actualizar(gameTime);
            }

            jugador.Actualizar(gameTime);
            
            

            base.Update(gameTime);
        }

        public void CargarAsteroides()
        {
            int randX = random.Next(1300, 1650);
            int randY = random.Next(0, 800);

            if (rocas.Count < 7) //ajustar a dificultad o cantidad de asterioides
            {
                rocas.Add(new Asteroide(texturas_rocas[random.Next(texturas_rocas.Count)], new Vector2(randX, randY)));

            }

            for (int i = 0; i < rocas.Count; i++)
            {
                if (!rocas[i].esVisible)
                {
                    rocas.RemoveAt(i);
                    i--;
                }
                    
            }

        }
    
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            fondo.Dibujar(spriteBatch);

            foreach (Asteroide roca in rocas)
            {
                roca.Dibujar(spriteBatch);
            }


            jugador.Dibujar(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
