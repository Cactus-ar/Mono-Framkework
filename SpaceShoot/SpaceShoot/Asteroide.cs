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
    public class Asteroide
    {
        public Texture2D textura;
        public Vector2 posicion;
        public Vector2 origen;
        public float angulo;
        public int velocidad;
        public bool haChocado, destruido;

        public Asteroide()
        {
            posicion = new Vector2(1250, 400);
            textura = null;
            velocidad = 4;
            haChocado = false;
            destruido = false;
        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);

        }

        public void CargarContenido(ContentManager content)
        {
            textura = content.Load<Texture2D>("");

        }
    }
}
