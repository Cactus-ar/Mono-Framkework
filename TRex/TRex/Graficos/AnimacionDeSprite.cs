using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRex.Graficos
{
    public class AnimacionDeSprite
    {
        private List<FrameDeAnimacion> _listaDeFramesParaAnimar = new List<FrameDeAnimacion>();
        public bool Ejecutandose { get; private set; }
        public float Progreso { get; private set; }
        public bool RepetirConstantemente { get; set; } = true;
        public float Duracion
        {
            get
            {
                if (!_listaDeFramesParaAnimar.Any())
                    return 0;
                else
                    return _listaDeFramesParaAnimar.Max(f => f.TimeStamp);
            }
        }

        public FrameDeAnimacion FrameActual
        {
            get
            {
                return _listaDeFramesParaAnimar
                        .Where(f => f.TimeStamp <= Progreso)
                        .OrderBy(f => f.TimeStamp)
                        .LastOrDefault();
            }
        }

        public void AgregarFrame(Sprite sprite, float timestamp)
        {
            FrameDeAnimacion frame = new FrameDeAnimacion(sprite, timestamp);
            _listaDeFramesParaAnimar.Add(frame);
        }

        public void Actualizar(GameTime gameTime)
        {
            if (Ejecutandose)
            {
                Progreso += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Progreso > Duracion)
                {
                    if (RepetirConstantemente)
                        Progreso -= Duracion;
                    else
                        Detener();
                }
                    
    
            }
        }

        public void Dibujar(SpriteBatch spriteBatch, Vector2 posicion)
        {
            FrameDeAnimacion frame = FrameActual;
            if(frame != null)
            {
                frame.Sprite.Dibujar(spriteBatch, posicion);
            }
        }



        public void Comenzar()
        {
            Ejecutandose = true;
        }

        public void Detener()
        {
            Ejecutandose = false;
            Progreso = 0;
        }

        public FrameDeAnimacion ObtenerFrame(int indice)
        {
            if (indice < 0 || indice >= _listaDeFramesParaAnimar.Count)
                throw new ArgumentOutOfRangeException(nameof(indice), "Fuera de Rango");

            return _listaDeFramesParaAnimar[indice];

        }

        public void Eliminar()
        {
            Detener();
            _listaDeFramesParaAnimar.Clear();
        }

        

    }
}
