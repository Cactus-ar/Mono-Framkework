using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TRex.Graficos;

namespace TRex.Entidades
{
    public class TileDeTerreno : IEntidadesDelJuego
    {

        public float PosicionX { get; set; }
        public Sprite Sprite { get; }
        public int OrdenDeDibujo { get; set; }

        private float _posicionY;

        public TileDeTerreno(float posicionX, float posicionY, Sprite sprite)
        {
            PosicionX = posicionX;
            _posicionY = posicionY;
            Sprite = sprite;
        }

        public void Actualizar(GameTime gameTime)
        {
            
        }

        public void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite.Dibujar(spriteBatch, new Vector2(PosicionX, _posicionY));
        }
    }
}
