using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TRex.Graficos;

namespace TRex.Entidades
{
    public class GrupoDeCactus : Obstaculo
    {
        public enum Tamanios
        {
            pequenios = 0,
            medianos = 1, 
            largos = 2
        }

        private const int CACTUS_PEQUE_POS_X = 228;
        private const int CACTUS_PEQUE_POS_Y = 0;
        private const int CACTUS_PEQUE_LARGO = 17;
        private const int CACTUS_PEQUE_ALTO = 36;

        private const int CACTUS_GRANDES_POS_X = 332;
        private const int CACTUS_GRANDES_POS_Y = 0;
        private const int CACTUS_GRANDES_LARGO = 25;
        private const int CACTUS_GRANDES_ALTO = 51;

        public override Rectangle CajaDeColision => throw new NotImplementedException();
        public bool EsLargo { get; }
        public Tamanios Dimension { get; }
        public Sprite grupoDeCactus { get; private set; }


        public GrupoDeCactus(Texture2D textura, bool esLargo, Tamanios tamanio, Trex trex, Vector2 posicion) : base(trex, posicion)
        {
            EsLargo = esLargo;
            Dimension = tamanio;
            grupoDeCactus = GenerarSprite(textura);
        }

        private Sprite GenerarSprite(Texture2D textura)
        {
            Sprite cactus = null;

            if (!EsLargo)
            {
                int offsetX = 0;
                int ancho = CACTUS_PEQUE_LARGO;

                switch (Dimension)
                {
                    case Tamanios.pequenios:
                        offsetX = 0;
                        ancho = CACTUS_PEQUE_LARGO;
                        break;
                    case Tamanios.medianos:
                        offsetX = 1;
                        ancho = CACTUS_PEQUE_LARGO * 2;
                        break;
                    case Tamanios.largos:
                        offsetX = 3;
                        ancho = CACTUS_PEQUE_LARGO * 3;
                        break;
                }

                cactus = new Sprite(textura, 
                    CACTUS_PEQUE_POS_X + offsetX * CACTUS_PEQUE_LARGO, 
                    CACTUS_PEQUE_POS_Y, 
                    ancho, 
                    CACTUS_PEQUE_ALTO, 
                    Color.White);


            }
            else
            {
                int offsetX = 0;
                int ancho = CACTUS_GRANDES_LARGO;

                switch (Dimension)
                {
                    case Tamanios.pequenios:
                        offsetX = 0;
                        ancho = CACTUS_GRANDES_LARGO;
                        break;
                    case Tamanios.medianos:
                        offsetX = 1;
                        ancho = CACTUS_GRANDES_LARGO * 2;
                        break;
                    case Tamanios.largos:
                        offsetX = 3;
                        ancho = CACTUS_GRANDES_LARGO * 3;
                        break;
                }

                cactus = new Sprite(textura,
                    CACTUS_GRANDES_POS_X + offsetX * CACTUS_GRANDES_LARGO,
                    CACTUS_GRANDES_POS_Y,
                    ancho,
                    CACTUS_GRANDES_ALTO,
                    Color.White);
            }
            return cactus;
        }

        public override void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            grupoDeCactus.Dibujar(spriteBatch, Posicion);
        }
    }
}
