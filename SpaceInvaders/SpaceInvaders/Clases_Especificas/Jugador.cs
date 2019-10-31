using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Clases_Especificas
{
    public class Jugador : ObjetoJuego
    {
        private float velocidadMovimiento = 3f;
        private float cadenciaDisparo = 1f;
        private float temporizador;




        public Jugador(Texture2D _txt, Vector2 _pos, Vector2 _esc, string _tag) : base(_txt, _pos, _esc, _tag)
        {
            esVisible = true;
        }

        public override void Actualizar()
        {
            throw new NotImplementedException();
        }

        public override void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, ObtenerLimites(), Color.White);
        }

        //Mecanicas del jugador
        private void Moverse()
        {
            if (Entrada.ObtenerTecla(Keys.Left))
            {
                velocidad.X -= 1;
            }
            if (Entrada.ObtenerTecla(Keys.Right))
            {
                velocidad.X += 1;
            }

            temporizador += Manejador.tiempoDelta;
            if(Entrada.TeclaPresionada(Keys.A) && temporizador >= cadenciaDisparo)
            {
                Disparar();
                temporizador = 0f;
            }

            ChequearColision();
        }

        private void Disparar()
        {
            float xPos = (escala.X / 2f) + posicion.X;
            float yPos = posicion.Y;
            LaserJugador laser = new LaserJugador(Cargador.ObtenerTextura("Bullet"), new Vector2(xPos, yPos), new Vector2(10f, 20f), "Bala");
            ManejadorEscenas.escenaActual.AgregarObjeto(laser);
        }

        private void ChequearColision()
        {
            foreach (ObjetoJuego item in ManejadorEscenas.escenaActual.objetosEnEscena)
            {
                if (item.etiqueta == "Enemigo" && item.esVisible)
                {
                    if (HayColision(item))
                    {
                        //TODO -- activar el fin de juego..o restar vidas..etc
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
            }
            else
            {
                return false;
            }
        }

    }
}
