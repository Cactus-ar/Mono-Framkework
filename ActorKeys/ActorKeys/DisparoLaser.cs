using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorKeys
{
    public class DisparoLaser
    {
        public Texture2D cuerpo;
        public Vector2 posicion;
        public Vector2 origen;
        public float velocidad;
        public bool esVisible;


        public DisparoLaser(Texture2D textura)
        {
            velocidad = 10f;
            cuerpo = textura;
            esVisible = false;
        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cuerpo, posicion, Color.White);
        }

    }
}
