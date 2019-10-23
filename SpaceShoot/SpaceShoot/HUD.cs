using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoot
{
    public class HUD
    {
        public int puntaje;
        public int pantallaAlto;
        public int pantallaAncho;
        public SpriteFont fuentePuntaje;
        public Vector2 posPuntaje;
        public bool mostrarHud;

        public HUD()
        {
            puntaje = 0;
            mostrarHud = true;
            pantallaAlto = 800;
            pantallaAncho = 1200;
            fuentePuntaje = null;
            posPuntaje = new Vector2(pantallaAncho / 2, 30);
        }

        public void CargarContenido(ContentManager contentManager)
        {
            fuentePuntaje = contentManager.Load<SpriteFont>("fuenteHUD");
        }

        public void Actualizar(GameTime gameTime)
        {
            //chequear teclas
            KeyboardState estadoTeclas = Keyboard.GetState();


        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            if (mostrarHud)
            {
                spriteBatch.DrawString(fuentePuntaje, "Puntaje - " + puntaje, posPuntaje, Color.Yellow);
            }
        }


    }
}
