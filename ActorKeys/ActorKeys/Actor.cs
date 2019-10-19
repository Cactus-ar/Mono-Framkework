using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorKeys
{
    public class Actor
    {
        public Texture2D cuerpo, laser;
        public Vector2 posicion;
        public int velocidad;
        public float cadenciaDiaparo;
        public List<DisparoLaser> disparos;

        //contructor
        public Actor()
        {
            disparos = new List<DisparoLaser>();
            cuerpo = null;
            posicion = new Vector2(400, 200);
            velocidad = 10;
            cadenciaDiaparo = 20f;


        }

        //cargar contenido;
        public void CargarContenido(ContentManager content)
        {
            
            cuerpo = content.Load<Texture2D>("nave_text1");
            laser = content.Load<Texture2D>("lasers_txt2");


            
        }

        public void Dibujar (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cuerpo, posicion, Color.White);

            foreach(DisparoLaser disparo in disparos)
            {
                disparo.Dibujar(spriteBatch);
            }
        }

        public void ActualizarDisparos()
        {
            foreach (var disparo in disparos)
            {
                disparo.posicion.Y -= disparo.velocidad;

                if (disparo.posicion.Y <= 0)    //si alcanzo el limite superior de la pantalla 
                    disparo.esVisible = false;
            }

            for (int i = 0; i < disparos.Count; i++) //si ya no es visible, removerlo de la lista
            {
                if (!disparos[i].esVisible) 
                {
                    disparos.RemoveAt(i);
                    i--;
                }
                
            }
        }

        public void ComenzarDisparar()
        {
            //Comenzar a disparar si la cadencia es 0
            if (cadenciaDiaparo >= 0)
                cadenciaDiaparo--;

            //si llega a 0 crear un nuevo disparo y hacerlo visible, ademas agregarlo a la lista
            if(cadenciaDiaparo <= 0)
            {
                DisparoLaser dispara = new DisparoLaser(laser)
                {
                    posicion = new Vector2(posicion.X, posicion.Y),
                    esVisible = true
                };

                if (disparos.Count() < 20)
                    disparos.Add(dispara);
            }

            //reset de la cadencia de disparos
            if (cadenciaDiaparo == 0)
                cadenciaDiaparo = 20f;
        }

        public void Actualizar()
        {
            //Obtener el estado del teclado
            KeyboardState estadoTecla = Keyboard.GetState();


            //Mover la Nave
            if (estadoTecla.IsKeyDown(Keys.W))
                posicion.Y -= velocidad;
            if (estadoTecla.IsKeyDown(Keys.S))
                posicion.Y += velocidad;
            if (estadoTecla.IsKeyDown(Keys.A))
                posicion.X -= velocidad;
            if (estadoTecla.IsKeyDown(Keys.D))
                posicion.X += velocidad;

            //disparar laser
            if (estadoTecla.IsKeyDown(Keys.Space))
                ComenzarDisparar();

            ActualizarDisparos();


            //mantener la nave dentro de los limites de la pantalla


            if (posicion.X <= 0) //Borde izquierdo
                posicion.X = 0;

            if (posicion.X >= 800 - cuerpo.Width) //Borde Derecho
                posicion.X = 800 - cuerpo.Width;

            if (posicion.Y <= 400)  //Mas de la mitad de la pantalla no puede pasar (ejeY)
                posicion.Y = 400;

            if (posicion.Y >= 950 - cuerpo.Height)  //limite inferior
                posicion.Y = 950 - cuerpo.Height;





        }

    }
}
