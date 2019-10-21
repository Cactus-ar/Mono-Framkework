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
    public class Jugador
    {
        public Texture2D textura;
        public Vector2 posicion;
        public Rectangle limites;
        public int velocidad;
        public bool haChocado;

        public Jugador()
        {
            posicion = new Vector2(30, 300);
            velocidad = 10;
            haChocado = false;

        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);
            
        }

        public void CargarContenido(ContentManager content)
        {
            textura = content.Load<Texture2D>("playerShip1_orange");
            
        }

        public void Actualizar(GameTime gameTime)
        {
            //chequear teclas
            KeyboardState estadoTeclas = Keyboard.GetState();
            if (estadoTeclas.IsKeyDown(Keys.Down))
                posicion.Y += velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Up))
                posicion.Y -= velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Left))
                posicion.X -= velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Right))
                posicion.X += velocidad;


            //chequear limites
            
            if (posicion.Y < 0) posicion.Y = 0;
            if (posicion.Y > 800 - textura.Height) posicion.Y = 800 - textura.Height;
            if (posicion.X < 0) posicion.X = 0;
            if (posicion.X > 600 - textura.Width) posicion.X = 600 - textura.Width;



        }

    }
}
