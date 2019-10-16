using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicoParallax
{
    public class ParallaxBackground
    {
        public Texture2D textura;
        public Rectangle rectangulo;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, rectangulo, Color.White);
        }

    }

    public class DesplazarBackground : ParallaxBackground
    {
        public DesplazarBackground(Texture2D texture2D, Rectangle rectangle)
        {
            textura = texture2D;
            rectangulo = rectangle;
        }

        public void Update(int velocidad)
        {
            rectangulo.X -= velocidad;
        }
    }

}
