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
    public class LaserJugador : ObjetoJuego
    {
        private float tiempoMuerto = 3f;
        private float temporizador;
        private float velocidadViaje = 4f;

        public LaserJugador(Texture2D _txt, Vector2 _pos, Vector2 _esc, string _tag) : base(_txt, _pos, _esc, _tag)
        {
        }

        public override void Actualizar()
        {
            temporizador += Manejador.tiempoDelta;

            if(temporizador >= tiempoMuerto)    //Diferente forma de sacar los objetos de pantalla..no por su posicion sino por tiempo
            {
                this.esVisible = false;
            }

            velocidad.Y -= 1f * velocidadViaje;
            posicion += velocidad;
            velocidad = Vector2.Zero;
            ChequearColision();

        }

        private void ChequearColision()
        {
            foreach (ObjetoJuego item in ManejadorEscenas.escenaActual.objetosEnEscena)
            {
                if(item.etiqueta == "Enemigo" && item.esVisible)
                {
                    if (HayColision(item))
                    {
                        //TODO -- agregar puntaje
                        item.esVisible = false;
                        this.esVisible = false;
                    }
                }
            }
        }

        private bool HayColision(ObjetoJuego _obj)
        {
            Rectangle bala = ObtenerLimites();
            Rectangle enemigo = _obj.ObtenerLimites();

            if (bala.Intersects(enemigo))
            {
                return true;
            }else
            {
                return false;
            }
        }




        public override void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, ObtenerLimites(), Color.White);
        }
    }
}
