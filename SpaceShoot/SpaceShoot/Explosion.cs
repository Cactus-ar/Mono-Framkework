using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoot
{
    public class Explosion
    {
        public Texture2D Textura;
        public Vector2 origen;
        public Vector2 posicion;
        public float timer;
        public float intervalo;
        public int frameActual;
        public int totalFrames;
        public int Columnas;
        public int Filas;
        public Rectangle limites;
        public bool esVisible;

        public Explosion(Texture2D texture2D, Vector2 vector2, int filas, int columnas)
        {
            Textura = texture2D;
            posicion = vector2;
            Filas = filas;
            Columnas = columnas;
            timer = 0f;
            intervalo = 30f; //intervalo..jugar con el valor o pasarlo segun el sheet
            frameActual = 1; //primer frame;
            totalFrames = Filas * Columnas;
            esVisible = true;
        }

        public void CargarContenido(ContentManager contentManager)
        {

        }

        public void Actualizar(GameTime gameTime)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //basicamente un contador de ms

            if (timer > intervalo) //..si llega a 20ms
            {
                frameActual++;  //incrementa el frame a mostrar

                if (frameActual == totalFrames) //si es el ultimo..ir al primero
                {
                    frameActual = 0; //..reset
                    esVisible = false;
                }
                timer = 0f; //..reset
            }
            
        }

        public void Dibujar(SpriteBatch spritebatch, Vector2 posicion)
        {
            int ancho = Textura.Width / Columnas;
            int alto = Textura.Height / Filas;

            int fila = (int)((float)frameActual / (float)Columnas);
            int columna = frameActual % Columnas;

            Rectangle sourceimagen = new Rectangle(ancho * columna, alto * fila, ancho, alto);
            Rectangle destinoimagen = new Rectangle((int)posicion.X, (int)posicion.Y, ancho, alto);
            spritebatch.Draw(Textura, destinoimagen, sourceimagen, Color.White);
            
        }



    }
}
