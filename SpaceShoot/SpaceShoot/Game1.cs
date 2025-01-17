﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

//Side scrolling shooter. Porque? porque eran "exoticos" cuando era chico
//y siempre me llamaron la atencion un poco más que los clasicos top down de naves

namespace SpaceShoot
{
    
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //hud
        HUD HUD = new HUD();


        //player
        Jugador jugador = new Jugador();

        //background
        FondoEstrellado fondo = new FondoEstrellado();
        
        //enemigos
        List<Texture2D> texturas_enemigos = new List<Texture2D>();
        List<Enemigo> marcianos = new List<Enemigo>();


        //asteroides
        List<Texture2D> texturas_rocas = new List<Texture2D>();
        List<Asteroide> rocas = new List<Asteroide>();

        //explosiones
        List<Explosion> explosiones = new List<Explosion>();

        //Sonido
        Sonido sndManager = new Sonido();
        


        Random random = new Random();
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = 1200,
                PreferredBackBufferHeight = 800
            };
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

            //Cargar skins de los enemigos
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlack1"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlack2"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlack3"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlack4"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlack5"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlue1"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlue2"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlue3"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlue4"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyBlue5"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyGreen1"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyGreen2"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyGreen3"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyGreen4"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyGreen5"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyRed1"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyRed2"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyRed3"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyRed4"));
            texturas_enemigos.Add(Content.Load<Texture2D>("enemyRed5"));


            //Cargar contenido de los asteroides
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_big1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_big2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_big3"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_big4"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_med1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_med3"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_small1"));  //estos son muy chicos?
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_small2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_tiny1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorBrown_tiny2"));   //--
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_big1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_big2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_big3"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_big4"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_med1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_med2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_small1"));   //--
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_small2"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_tiny1"));
            texturas_rocas.Add(Content.Load<Texture2D>("meteorGrey_tiny2"));    //--


            jugador.CargarContenido(Content);

            //Sonido
            sndManager.CargarContenido(Content);
            MediaPlayer.Play(sndManager.Musica); //Ni bien se carga el contenido comienza a reproducirse
            MediaPlayer.IsRepeating = true; //loop infinito de reproduccion
            

            HUD.CargarContenido(Content);

            
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
            CargarEnemigos();
            ManejarExplosiones();


            jugador.Actualizar(gameTime);

            //actualizar enemigos y colisiones y disparos
            foreach (Enemigo enemigo in marcianos)
            {

                if (enemigo.limites.Intersects(jugador.limites))    //si chocamos
                {
                    enemigo.esVisible = false;
                    jugador.vida -= 40;
                }

                foreach (Laser disparo in jugador.disparos) //si le pegamos con el arma
                {
                    if (enemigo.limites.Intersects(disparo.Limites))
                    {
                        HUD.puntaje += 10;
                        enemigo.esVisible = false;
                        disparo.esVisible = false;

                        //Explota
                        sndManager.SndEfectoExplosion.Play();
                        Texture2D explosion = Content.Load<Texture2D>("explosion1_M");
                        Vector2 posicion = new Vector2(enemigo.posicionEnemigo.X, enemigo.posicionEnemigo.Y);
                        explosiones.Add(new Explosion(explosion, posicion, 4, 8));

                    }
                }


                foreach (Laser laser in enemigo.disparosEnemigo)        //si nos pega a nosotros
                {
                    if(laser.Limites.Intersects(jugador.limites))
                    {
                        laser.esVisible = false;
                        sndManager.SndEfectoExplosion.Play();
                        jugador.vida -= enemigo.danioDelArma;
                    }

                }

                enemigo.Actualizar(gameTime);

                foreach(Explosion explota in explosiones)
                {
                    explota.Actualizar(gameTime);
                }

            }

            

            //actualizar asteroides y chequear colisiones
            foreach (Asteroide roca in rocas)
            {


                if (roca.limites.Intersects(jugador.limites)) //Si chocamos contra una roca...
                {
                    roca.esVisible = false;
                    jugador.vida -= 30;

                    //Explota
                    sndManager.SndEfectoExplosion.Play();
                    Texture2D explosion = Content.Load<Texture2D>("explosion3_M");
                    Vector2 posicion = new Vector2(jugador.posicionNave.X - jugador.textura_nave.Width, jugador.posicionNave.Y - jugador.textura_nave.Height);
                    explosiones.Add(new Explosion(explosion, posicion, 3, 3));

                }

                //verificar colision con disparos
                foreach (Laser disparo in jugador.disparos)
                {
                    if (roca.limites.Intersects(disparo.Limites))
                    {
                        HUD.puntaje += 15;
                        roca.esVisible = false;
                        disparo.esVisible = false;


                        //Explota
                        sndManager.SndEfectoExplosion.Play();
                        Texture2D explosion = Content.Load<Texture2D>("explosion2_M");
                        Vector2 posicion = new Vector2(roca.posicion.X, roca.posicion.Y);
                        explosiones.Add(new Explosion(explosion, posicion, 5, 6));
                    }
                }


                roca.Actualizar(gameTime);
            }

            
            //HUD.Actualizar(gameTime);

            base.Update(gameTime);
        }

        public void CargarEnemigos()
        {
            int randX = random.Next(1300, 1650);
            int randY = random.Next(0, 800);

            if (marcianos.Count < 3) //ajustar a dificultad o cantidad de asterioides
            {
                Texture2D laserEnemigo = Content.Load<Texture2D>("laserGreen13");
                Texture2D naveEnemiga = texturas_enemigos[random.Next(texturas_enemigos.Count)];
                marcianos.Add(new Enemigo(naveEnemiga, laserEnemigo, new Vector2(randX, randY)));

            }

            for (int i = 0; i < marcianos.Count; i++)
            {
                if (!marcianos[i].esVisible)
                {
                    marcianos.RemoveAt(i);
                    i--;
                }

            }


        }

        public void ManejarExplosiones()
        {
            for (int i = 0; i < explosiones.Count; i++)
            {
                if (!explosiones[i].esVisible) //si ya no es visible, sacarla de la lista
                {
                    explosiones.RemoveAt(i);
                    i--;
                }
                    
            }
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
            /*
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            RasterizerState state = new RasterizerState();
            state.FillMode = FillMode.WireFrame;
            spriteBatch.GraphicsDevice.RasterizerState = state;
            */



            fondo.Dibujar(spriteBatch);

            foreach (Enemigo enemigo in marcianos)
            {
                enemigo.Dibujar(spriteBatch);
            }

            foreach (Asteroide roca in rocas)
            {
                roca.Dibujar(spriteBatch);
            }

            foreach (Explosion explota in explosiones)
            {
                explota.Dibujar(spriteBatch, new Vector2(explota.posicion.X, explota.posicion.Y));
            }

            jugador.Dibujar(spriteBatch);
            HUD.Dibujar(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
