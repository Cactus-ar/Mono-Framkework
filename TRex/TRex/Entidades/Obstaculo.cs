using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;

namespace TRex.Entidades
{



    public abstract class Obstaculo : IEntidadesDelJuego
    {

        private Trex _trex;
        

        public abstract Rectangle CajaDeColision { get; }

        public int OrdenDeDibujo { get; set; }
        public Vector2 Posicion { get; private set; }

        protected Obstaculo(Trex trex, Vector2 posicion)
        {
            _trex = trex;
            Posicion = posicion;
        }



        public void Actualizar(GameTime gameTime)
        {
            float posX = Posicion.X - _trex.Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Posicion = new Vector2(posX, Posicion.Y);
        }

        public abstract void Dibujar(SpriteBatch spriteBatch, GameTime gameTime);
        



    }
}
