using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Engine
{
    public static class Entrada
    {
        private static KeyboardState tecladoActual;
        private static KeyboardState tecladoAnterior;

        private static MouseState mouseAnterior;
        private static MouseState mouseActual;
        private static Vector2 mousePosicion;

        //Llamada por Game1 en cada frame para obtener actualizaciones del teclado
        public static void ActualizarEntrada()
        {
            tecladoAnterior = tecladoActual;
            tecladoActual = Keyboard.GetState();

            mouseAnterior = mouseActual;
            mouseActual = Mouse.GetState();

            mousePosicion.X = mouseActual.X;
            mousePosicion.Y = mouseActual.Y;

            if (TeclaPresionada(Keys.P))
            {
                //usado para esconder o mostrar el puntero del mouse si hay pausa

            }
        }

        //Estados de las teclas
        public static bool TeclaPresionada(Keys tecla)
        {
            if(tecladoActual.IsKeyDown(tecla) && tecladoAnterior.IsKeyUp(tecla))
            {
                return true;
            }else
            {
                return false;
            }
        }
        public static bool TeclaSoltada(Keys tecla)
        {
            if (tecladoActual.IsKeyUp(tecla) && tecladoAnterior.IsKeyDown(tecla))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ObtenerTecla(Keys tecla)
        {
            if (tecladoAnterior.IsKeyDown(tecla) && tecladoActual.IsKeyDown(tecla))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Estados del Mouse
        public static bool BotonMousePresionado(int indice) //Izquierdo = 0 Derecho = 1
        {
            if(mouseActual.LeftButton == ButtonState.Pressed && mouseAnterior.LeftButton == ButtonState.Released && indice == 0)
            {
                return true; //boton izquierdo
            }
            if(mouseActual.RightButton == ButtonState.Pressed && mouseAnterior.RightButton == ButtonState.Released && indice == 1)
            {
                return true; //boton derecho
            }

            return false;
        }

        public static bool BotonMouseSoltado(int indice)
        {
            if(mouseActual.LeftButton == ButtonState.Released && mouseAnterior.LeftButton == ButtonState.Pressed && indice == 0)
            {
                return true;
            }
            if(mouseActual.RightButton == ButtonState.Released && mouseAnterior.RightButton == ButtonState.Pressed && indice == 1)
            {
                return true;
            }

            return false;

        }

        public static Vector2 ObtenerPosicionMouse()
        {
            return mousePosicion;
        }

    }
}
