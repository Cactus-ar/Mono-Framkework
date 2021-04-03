using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TRex.Entidades
{
    public class MostrarPuntaje : IEntidadesDelJuego
    {

        private const int NUMERO_POS_X = 655;
        private const int NUMERO_POS_Y = 0;
        private const int NUMERO_ANCHO = 10;
        private const int NUMERO_ALTO = 13;
        private const int MARGEN = 70;
        private const int TEXTURA_HI_X = 755;
        private const int TEXTURA_HI_Y = 0;
        private const int TEXTURA_HI_ANCHO = 20;
        private const int TEXTURA_HI_ALTO = 13;
        private const int TEXTURA_HI_MARGEN = 30;

        private const byte NUMERO_DE_DIGITOS = 5;
        private const float INCREMENTO_MULTIPLICADOR = 0.05f;
        private Texture2D _textura;
        private Trex _trex;

        public double Puntaje { get; set; }
        public int PuntajeMostrado => (int)Math.Floor(Puntaje);
        public int PuntajeRecord { get; set; }
        public bool ExisteRecord => PuntajeRecord > 0;
        public Vector2 Posicion { get; set; }

        public int OrdenDeDibujo => 100;



        private int[] DividirPuntaje(int entrada)
        {
            string cadena = entrada.ToString().PadLeft(NUMERO_DE_DIGITOS, '0');
            int[] resultado = new int[cadena.Length];

            for (int i = 0; i < resultado.Length; i++)
            {
                resultado[i] = (int)char.GetNumericValue(cadena[i]);
            }

            return resultado;

        }

        private Rectangle ObtenerTexturaDelDigito(int digito)
        {
            if (digito < 0 || digito > 9)
                throw new ArgumentOutOfRangeException("digito", "el valor debe estar entre 0 y 9");

            int posX = NUMERO_POS_X + digito * NUMERO_ANCHO;
            int posY = NUMERO_POS_Y;

            return new Rectangle(posX, posY, NUMERO_ANCHO, NUMERO_ALTO);
        }

        public MostrarPuntaje(Texture2D textura, Vector2 posicion, Trex trex)
        {
            _textura = textura;
            _trex = trex;
            Posicion = posicion;
        }

        public void Actualizar(GameTime gameTime)
        {
            Puntaje += _trex.Velocidad * INCREMENTO_MULTIPLICADOR * gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ExisteRecord)
            {
                DibujarPuntaje(spriteBatch, PuntajeRecord, Posicion.X);
                spriteBatch.Draw(_textura, new Vector2(Posicion.X - TEXTURA_HI_MARGEN, Posicion.Y), new Rectangle(TEXTURA_HI_X, TEXTURA_HI_Y, TEXTURA_HI_ANCHO, TEXTURA_HI_ALTO), Color.White);
            }

            DibujarPuntaje(spriteBatch, PuntajeMostrado, Posicion.X + MARGEN);
        }

        private void DibujarPuntaje(SpriteBatch spriteBatch, int puntaje, float posInicialX)
        {
            int[] digitosDelPuntaje = DividirPuntaje(puntaje);
            float posX = posInicialX;

            foreach (int digito in digitosDelPuntaje)
            {
                Rectangle coordenadas = ObtenerTexturaDelDigito(digito);
                Vector2 posEnPantalla = new Vector2(posX, Posicion.Y);
                spriteBatch.Draw(_textura, posEnPantalla, coordenadas, Color.White);
                posX += NUMERO_ANCHO;

            }
        }
    }
}
