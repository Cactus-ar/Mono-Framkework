using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Engine
{
    public static class Cargador
    {
        private static Game1 juego;

        public static void Init(Game1 game1)
        {
            juego = game1;
        }

        public static Texture2D ObtenerTextura(string ruta)
        {
            Texture2D textura = juego.Content.Load<Texture2D>(ruta);
            return textura;
        }

        public static SpriteFont ObtenerFuentes(string ruta)
        {
            SpriteFont fuente = juego.Content.Load<SpriteFont>(ruta);
            return fuente;
        }

    }
}
