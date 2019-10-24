using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoot
{
    public class Sonido
    {
        public SoundEffect SndEfectoDisparo;
        public SoundEffect SndEfectoExplosion;
        public Song Musica;

        public Sonido()
        {
            SndEfectoDisparo = null;
            SndEfectoExplosion = null;
            Musica = null;
        }

        public void CargarContenido(ContentManager content)
        {
            SndEfectoDisparo = content.Load<SoundEffect>("shoot_missile");
            SndEfectoExplosion = content.Load<SoundEffect>("big_explosion");

            //Background music by PlayOnLoop.com
            //Licensed under Creative Commons by Attribution 4.0
            //Musica = content.Load<Song>("POL-the-foyer-short");
            Musica = content.Load<Song>("Musica");


        }

    }
}
