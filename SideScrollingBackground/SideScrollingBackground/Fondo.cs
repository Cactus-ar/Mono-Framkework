using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScrollingBackground
{
    public class Fondo
    {
        public Texture2D textura;

        //Uno solo es visible mientras que el otro entra y sale de escena
        public Vector2 backPos1, backPos2;

        public int velocidad;


        public Fondo()
        {
            textura = null;
            backPos1 = new Vector2(0, 0);
            backPos2 = new Vector2(800, 0); //Directamente encima uno del otro
            velocidad = 5;  //Modificar la velocidad a la que se mueve el fondo

        }

        //carcar contenido
        public void CargarContent(ContentManager contenido)
        {
            textura = contenido.Load<Texture2D>("fondo");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, backPos1, Color.White);
            spriteBatch.Draw(textura, backPos2, Color.White);
        }

        //update
        public void Update(GameTime gameTime)
        {
            backPos1.X -= velocidad;
            backPos2.X -= velocidad;

            //scroll
            if (backPos1.X <= -800)
            {
                backPos1.X = 0;
                backPos2.X = 800;
            }

        }
    }
}
