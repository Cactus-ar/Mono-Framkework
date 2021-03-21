using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TRex.Graficos
{
    public class Sprite
    {
        public Texture2D Textura { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Alto { get; set; }
        public int Ancho { get; set; }
        public Color ColorTinta { get; set; }

        public Sprite(Texture2D textura, int x, int y, int ancho, int alto, Color colortinta)
        {
            Textura = textura;
            X = x;
            Y = y;
            Alto = alto;
            Ancho = ancho;
            ColorTinta = colortinta;
        }

        public void Dibujar(SpriteBatch spriteBatch, Vector2 posicion)
        {
            spriteBatch.Draw(Textura, posicion, new Rectangle(X, Y, Ancho, Alto), ColorTinta);
        }

    }
}
