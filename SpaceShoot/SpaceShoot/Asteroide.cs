using Microsoft.Xna.Framework;
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
        public bool esVisible; 
        public Rectangle limites;

        public Asteroide(Texture2D texture2D, Vector2 vector2)
        {
            posicion = vector2;
            textura = texture2D;
            velocidad = 4;
            esVisible = true;
        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            if(esVisible)
            {
                origen.X = textura.Width / 2;
                origen.Y = textura.Height / 2;
                spriteBatch.Draw(textura, posicion, null, Color.White, angulo, origen, 1.0f, SpriteEffects.None, 0f);
            }

        }

        public void Actualizar(GameTime gameTime)
        {

            limites = new Rectangle((int)posicion.X, (int)posicion.Y, textura.Width, textura.Height);
            posicion.X -= velocidad;    //velocidad podria ser rnd


            //TODO..si llega al limite izquierdo remover..
            if (posicion.X < 0) esVisible = false;


            //Rotacion del asteroide
            float espera = (float)gameTime.ElapsedGameTime.TotalSeconds;
            angulo += espera;
            float circunsferencia = MathHelper.Pi * 2;
            angulo %= circunsferencia;

        }

        
    }
}
