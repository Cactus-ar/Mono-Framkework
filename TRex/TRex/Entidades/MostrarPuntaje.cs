using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TRex.Entidades
{
    class MostrarPuntaje : IEntidadesDelJuego
    {

        private const int NUMERO_POS_X = 655;
        private const int NUMERO_POS_Y = 0;
        private const int NUMERO_ANCHO = 10;
        private const int NUMERO_ALTO = 13;

        private const byte NUMERO_DE_DIGITOS = 5;

        private Texture2D _texttura;

        public double Puntaje { get; set; }
        public int PuntajeMostrado => (int)Math.Floor(Puntaje);

        public int PuntajeRecord { get; set; }
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

        public MostrarPuntaje(Texture2D textura, Vector2 posicion)
        {
            _texttura = textura;
            Posicion = posicion;
        }

        public void Actualizar(GameTime gameTime)
        {
            
        }

        public void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int[] digitosDelPuntaje = DividirPuntaje(PuntajeMostrado);
            float posX = Posicion.X;

            

            foreach (int digito in digitosDelPuntaje)
            {
                Rectangle coordenadas = ObtenerTexturaDelDigito(digito);
                Vector2 posEnPantalla = new Vector2(posX, Posicion.Y);
                spriteBatch.Draw(_texttura, posEnPantalla, coordenadas, Color.White);
                posX += NUMERO_ANCHO;
                
            }
            
        }
    }
}
