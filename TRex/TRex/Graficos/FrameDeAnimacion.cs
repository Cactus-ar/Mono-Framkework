using System;
using System.Collections.Generic;
using System.Text;

namespace TRex.Graficos
{
    public class FrameDeAnimacion
    {
        private Sprite _sprite;


        public Sprite Sprite { 
            get => _sprite;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("valor", "no puede ser nulo");
                else
                    _sprite = value;
            }
        }

        public float TimeStamp { get; }

        public FrameDeAnimacion(Sprite sprite, float timestamp)
        {
            Sprite = sprite;
            TimeStamp = timestamp;
        }

    }
}
