using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoot
{
    public class FondoEstrellado
    {
        public Texture2D textura_fondo1, textura_fondo2;
        public Vector2 fondo_1a, fondo_1b, fondo_2a, fondo_2b;
        public int vel_fondo_1, vel_fondo_2;

        public FondoEstrellado()
        {
            textura_fondo1 = null;
            textura_fondo2 = null;

            fondo_1a = new Vector2(0, 0);
            fondo_1b = new Vector2(1200, 0);

            fondo_2a = new Vector2(0, 0);
            fondo_2b = new Vector2(1200, 0);

            vel_fondo_1 = 5;
            vel_fondo_2 = 3;
            
        }

        public void CargarContenido(ContentManager content)
        {
            textura_fondo1 = content.Load<Texture2D>("estrellas1");
            textura_fondo2 = content.Load<Texture2D>("estrellas2");

        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura_fondo1, fondo_1a, Color.White);
            spriteBatch.Draw(textura_fondo1, fondo_1b, Color.White);
            spriteBatch.Draw(textura_fondo2, fondo_2a, Color.White);
            spriteBatch.Draw(textura_fondo2, fondo_2b, Color.White);
        }

        public void Actualizar(GameTime gameTime)
        {
            fondo_1a.X -= vel_fondo_1;
            fondo_1b.X -= vel_fondo_1;
            fondo_2a.X -= vel_fondo_2;
            fondo_2b.X -= vel_fondo_2;



            if (fondo_1a.X + textura_fondo1.Width <= 0)
                fondo_1a.X = fondo_1b.X + textura_fondo1.Width;
            if (fondo_1b.X + textura_fondo1.Width <= 0)
                fondo_1b.X = fondo_1a.X + textura_fondo1.Width;

            if (fondo_2a.X + textura_fondo2.Width <= 0)
                fondo_2a.X = fondo_2b.X + textura_fondo2.Width;
            if (fondo_2b.X + textura_fondo2.Width <= 0)
                fondo_2b.X = fondo_2a.X + textura_fondo2.Width;



        }



    }
}
