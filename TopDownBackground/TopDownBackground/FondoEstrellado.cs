using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownBackground
{
    public class FondoEstrellado
    {
        public Texture2D textura;

        //Uno solo es visible mientras que el otro entra y sale de escena
        public Vector2 backPos1, backPos2;

        public int velocidad;


        public FondoEstrellado()
        {
            textura = null;
            backPos1 = new Vector2(0, 0);
            backPos2 = new Vector2(0, -950); //Directamente encima uno del otro
            velocidad = 5;  //Modificar la velocidad a la que se mueve el fondo

        }

        //carcar contenido
        public void CargarContent(ContentManager contenido)
        {
            textura = contenido.Load<Texture2D>("fondoestrellas");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, backPos1, Color.White);
            spriteBatch.Draw(textura, backPos2, Color.White);
        }

        //update
        public void Update(GameTime gameTime)
        {
            backPos1.Y += velocidad;    
            backPos2.Y += velocidad;

            //scroll
            if (backPos1.Y >= 950)
            {
                backPos1.Y = 0;
                backPos2.Y = -950;
            }

        }

    }
}
