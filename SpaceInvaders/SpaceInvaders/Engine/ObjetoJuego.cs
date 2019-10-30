using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Engine
{
    public abstract class ObjetoJuego
    {
        //clase general de la que heredan los demas onjetos dentro del juego
        public Texture2D textura;
        public Vector2 posicion;
        public Vector2 escala;
        public Vector2 velocidad;
        public bool esColisionable;
        public bool esVisible = true;
        public string etiqueta;

        public ObjetoJuego(Texture2D _txt, Vector2 _pos, Vector2 _esc, string _tag)
        {
            textura = _txt;
            posicion = _pos;
            escala = _esc;
            velocidad = Vector2.Zero;
            etiqueta = _tag;
        }

        public abstract void Actualizar();

        public abstract void Dibujar(SpriteBatch spriteBatch);

        public Rectangle ObtenerLimites()
        {
            Rectangle limites = new Rectangle((int)posicion.X, (int)posicion.Y, (int)escala.X, (int)escala.Y);
            return limites;
        }


    }
}
