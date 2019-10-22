using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoot
{
    public class Enemigo
    {
        public Rectangle limites;
        public Texture2D texturaEnemigo, texturaDisparoEnemigo;
        public Vector2 posicionEnemigo;
        public int vida, velocidad, cadenciaDisparo, dificultad, tirosEnPantalla, danioDelArma;
        public bool esVisible;

        public List<Laser> disparosEnemigo;

        public Enemigo(Texture2D texture2DEnemigo, Texture2D laserEnemigo, Vector2 vector2)
        {
            texturaEnemigo = texture2DEnemigo;
            posicionEnemigo = vector2;
            texturaDisparoEnemigo = laserEnemigo;
            disparosEnemigo = new List<Laser>();
            vida = 5;
            dificultad = 1;
            tirosEnPantalla = 3;
            cadenciaDisparo = 80;
            velocidad = 5;
            danioDelArma = 10;
            esVisible = true;
        }

        public void Actualizar(GameTime gameTime)
        {
            //actualizar la caja de colision
            limites = new Rectangle((int)posicionEnemigo.X, (int)posicionEnemigo.Y, texturaEnemigo.Width, texturaEnemigo.Height);

            //actualizar movimiento
            posicionEnemigo.X -= velocidad;
            if (posicionEnemigo.X < 0)
                esVisible = false;

            EnemigoDispara();
            ActualizarDisparos();

        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            //nave
            spriteBatch.Draw(texturaEnemigo, limites, Color.White);

            //disparo
            foreach (Laser disparo in disparosEnemigo)
            {
                disparo.Dibujar(spriteBatch);
            }
        }

        public void EnemigoDispara()
        {
            if (cadenciaDisparo >= 0)
                cadenciaDisparo--;
            else
            {
                Laser nuevoDisparo = new Laser(texturaDisparoEnemigo);

                int nav1 = texturaEnemigo.Height - texturaDisparoEnemigo.Height;
                nuevoDisparo.Posicion = new Vector2(posicionEnemigo.X, posicionEnemigo.Y + nav1 / 2);
                nuevoDisparo.esVisible = true;

                if (disparosEnemigo.Count < tirosEnPantalla)
                    disparosEnemigo.Add(nuevoDisparo);

                cadenciaDisparo = 80;
            }
            
        }

        public void ActualizarDisparos()
        {
            //eliminar los tiros que se fueron de pantalla y mover los que quedan
            foreach (Laser disparo in disparosEnemigo)
            {
                disparo.Posicion.X -= disparo.Velocidad;

                disparo.Limites = new Rectangle((int)disparo.Posicion.X, (int)disparo.Posicion.Y, texturaDisparoEnemigo.Width, texturaDisparoEnemigo.Height);

                if (disparo.Posicion.X < 0)
                    disparo.esVisible = false;

            }

            for (int i = 0; i < disparosEnemigo.Count; i++)
            {
                if (!disparosEnemigo[i].esVisible) //si alguno de la lista ya no es visible..sacarlo
                {
                    disparosEnemigo.RemoveAt(i);
                    i--;
                }


            }
        }


    }
}
