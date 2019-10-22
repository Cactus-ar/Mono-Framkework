using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoot
{
    public class Laser
    {
        public Rectangle Limites;
        public Texture2D Textura;
        public Vector2 Origen;
        public Vector2 Posicion;
        public bool esVisible;
        public float Velocidad;

        public Laser(Texture2D texture2D)   //Pasamos la textura por si en un futuro implemento otro tipo de disparo
        {
            Velocidad = 10;
            Textura = texture2D;
            esVisible = false;

        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textura, Posicion, Color.White);
        }
    }
}
