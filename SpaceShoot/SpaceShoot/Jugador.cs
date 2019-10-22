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
        public Texture2D textura_nave, textura_laser, textura_HP;
        public Vector2 posicionNave, posicion_barraHP;
        public Rectangle limites, barra_HP;
        public float cadenciaDisparos;
        public int tirosEnPantalla;
        public int velocidad, vida;
        public bool haChocado;
        public List<Laser> disparos;


        public Jugador()
        {
            posicionNave = new Vector2(30, 300);
            posicion_barraHP = new Vector2(30, 30); //borde superior izquierdo
            disparos = new List<Laser>();
            velocidad = 10;
            vida = 300;
            cadenciaDisparos = 10;
            tirosEnPantalla = 10;
            haChocado = false;
            

        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura_nave, posicionNave, Color.White);
            spriteBatch.Draw(textura_HP, barra_HP, Color.White);

            foreach (Laser disparo in disparos)
            {
                disparo.Dibujar(spriteBatch);
            }
            
        }

        public void Disparar()
        {
            //el disparo sale solo si la cadencia se ha reseteado
            if (cadenciaDisparos >= 0)
            {

                cadenciaDisparos--;

            }else
            {
                Laser tiro = new Laser(textura_laser);

                int nav1 = textura_nave.Height - textura_laser.Height;
                int nav2 = textura_nave.Width;

                tiro.Posicion = new Vector2(posicionNave.X + nav2 , posicionNave.Y + nav1 /2);

                tiro.esVisible = true;

                if(disparos.Count < tirosEnPantalla)
                    disparos.Add(tiro);

                cadenciaDisparos = 10;

            }

        }

        public void ActualizarTiros()
        {
            //eliminar los tiros que se fueron de pantalla y mover los que quedan
            foreach (Laser disparo in disparos)
            {
                disparo.Posicion.X += disparo.Velocidad;

                disparo.Limites = new Rectangle((int)disparo.Posicion.X, (int)disparo.Posicion.Y, textura_laser.Width, textura_laser.Height);

                if (disparo.Posicion.X > 1200)
                    disparo.esVisible = false;

            }

            for (int i = 0; i < disparos.Count; i++)
            {
                if (!disparos[i].esVisible) //si alguno de la lista ya no es visible..sacarlo
                {
                    disparos.RemoveAt(i);
                    i--;
                }
                    

            }


        }

        public void CargarContenido(ContentManager content)
        {
            textura_nave = content.Load<Texture2D>("playerShip1_orange");
            textura_laser = content.Load<Texture2D>("laserRed06");
            textura_HP = content.Load<Texture2D>("hp_unit_1");

        }

        public void Actualizar(GameTime gameTime)
        {
            //chequear teclas
            KeyboardState estadoTeclas = Keyboard.GetState();
            if (estadoTeclas.IsKeyDown(Keys.Down))
                posicionNave.Y += velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Up))
                posicionNave.Y -= velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Left))
                posicionNave.X -= velocidad;
            if (estadoTeclas.IsKeyDown(Keys.Right))
                posicionNave.X += velocidad;
            if (estadoTeclas.IsKeyDown(Keys.A))
                Disparar();


            //generar caja de colision de la nave
            limites = new Rectangle((int)posicionNave.X, (int)posicionNave.Y, textura_nave.Width, textura_nave.Height);
            //generar barra de HP
            barra_HP = new Rectangle((int)posicion_barraHP.X, (int)posicion_barraHP.Y, vida, textura_HP.Height);


            //chequear limites de la pantalla
            
            if (posicionNave.Y < 0) posicionNave.Y = 0;
            if (posicionNave.Y > 800 - textura_nave.Height) posicionNave.Y = 800 - textura_nave.Height;
            if (posicionNave.X < 0) posicionNave.X = 0;
            if (posicionNave.X > 600 - textura_nave.Width) posicionNave.X = 600 - textura_nave.Width;

            //actualizar tiros
            ActualizarTiros();


        }

    }
}
