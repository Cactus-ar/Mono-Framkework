using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasDeTexturas
{
    public class SpriteAnimado
    {
        public Texture2D Textura { get; set; }
        public int Columnas { get; set; }
        public int Filas { get; set; }
        private int actualFrame { get; set; }
        private int totalFrames { get; set; }


        //Una espera entre frames para ver los resultados
        private static readonly TimeSpan intervalo = TimeSpan.FromMilliseconds(150);
        private TimeSpan ultimaVez;

        public SpriteAnimado(Texture2D texture2D, int filas, int columnas)
        {
            Textura = texture2D;
            Filas = filas;
            Columnas = columnas;
            actualFrame = 0;
            totalFrames = Filas * Columnas;

        }

        public void Update(GameTime gameTime)
        {
            // If enough time has passed attack
            if (ultimaVez + intervalo < gameTime.TotalGameTime)
            {
                actualFrame++;
                if (actualFrame == totalFrames)
                    actualFrame = 0;

                ultimaVez = gameTime.TotalGameTime;
            }
        }

        public void Draw(SpriteBatch spritebatch, Vector2 posicion)
        {
            int ancho = Textura.Width / Columnas;
            int alto = Textura.Height / Filas;

            int fila = (int)((float)actualFrame / (float)Columnas);
            int columna = actualFrame % Columnas;

            Rectangle sourceimagen = new Rectangle(ancho * columna, alto * fila, ancho, alto);
            Rectangle destinoimagen = new Rectangle((int)posicion.X, (int)posicion.Y, ancho, alto);

            spritebatch.Begin();
            spritebatch.Draw(Textura, destinoimagen, sourceimagen, Color.White);
            spritebatch.End();
        }


    }
}
