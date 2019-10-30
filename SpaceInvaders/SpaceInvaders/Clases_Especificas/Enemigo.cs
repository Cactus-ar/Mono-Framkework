using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Clases_Especificas
{
    public class Enemigo : ObjetoJuego
    {
        private Vector2 vectorMovimiento = new Vector2(1f, 0f);
        private float velocidadMovimiento = 2f;

        public Enemigo(Texture2D _txt, Vector2 _pos, Vector2 _esc, string _tag) : base(_txt, _pos, _esc, _tag)
        {
        }



        public override void Actualizar()
        {
            if(posicion.X <= 50f || posicion.X >= 700f)
            {
                vectorMovimiento.X *= -1f;
                vectorMovimiento.Y += 10f;
                velocidadMovimiento += 0.5f;
            }

            posicion += vectorMovimiento * velocidadMovimiento;
            vectorMovimiento.Y = 0f;

        }

        public override void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, ObtenerLimites(), Color.White);
        }
    }
}
