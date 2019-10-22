using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoot
{
    public class Jugador
    {
        public Texture2D textura, laser_textura;
        public Vector2 posicion;
        public Rectangle limites;
        public float cadenciaDisparos;
        public int tirosEnPantalla;
        public int velocidad;
        public bool haChocado;
        public List<Laser> disparos;


        public Jugador()
        {
            posicion = new Vector2(30, 300);
            disparos = new List<Laser>();
            velocidad = 10;

            cadenciaDisparos = 10;
            tirosEnPantalla = 10;
            haChocado = false;

        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);

            foreach (Laser disparo in disparos)
            {
                disparo.Dibujar(spriteBatch);
            }
            
        }

        public void Disparar()
        {
            //el disparo sale solo si la cadencia se ha reseteado
            if (cadenciaDisparos >= 0)
            {

                cadenciaDisparos--;

            }else
            {
                Laser tiro = new Laser(laser_textura);

                int nav1 = textura.Height - laser_textura.Height;
                int nav2 = textura.Width;

                tiro.Posicion = new Vector2(posicion.X + nav2 , posicion.Y + nav1 /2);

                tiro.esVisible = true;

                if(disparos.Count < tirosEnPantalla)
                    disparos.Add(tiro);

                cadenciaDisparos = 10;

            }

        }

        public void ActualizarTiros()
        {
            //eliminar los tiros que se fueron de pantalla y mover los que quedan
            foreach (Laser disparo in disparos)
            {
                disparo.Posicion.X += disparo.Velocidad;

                if (disparo.Posicion.X > 1200)
                    disparo.esVisible = false;

            }

            for (int i = 0; i < disparos.Count; i++)
            {
                if (!disparos[i].esVisible) //si alguno de la lista ya no es visible..sacarlo
                {
                    disparos.RemoveAt(i);
                    i--;
                }
                    

            }


        }

        public void CargarContenido(ContentManager content)
        {
            textura = content.Load<Texture2D>("playerShip1_orange");
            laser_textura = content.Load<Texture2D>("laserRed06");

        }

        public void Actualizar(GameTime gameTime)
        {
            //chequear teclas
            KeyboardState estadoTeclas = Keyboard.GetState();
            if (estadoTeclas.IsKeyDown(Keys.Down))
                posicion.Y += velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Up))
                posicion.Y -= velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Left))
                posicion.X -= velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Right))
                posicion.X += velocidad;
            if (estadoTeclas.IsKeyDown(Keys.A))
                Disparar();

            //chequear limites
            
            if (posicion.Y < 0) posicion.Y = 0;
            if (posicion.Y > 800 - textura.Height) posicion.Y = 800 - textura.Height;
            if (posicion.X < 0) posicion.X = 0;
            if (posicion.X > 600 - textura.Width) posicion.X = 600 - textura.Width;

            //actualizar tiros
            ActualizarTiros();


        }

    }
}
