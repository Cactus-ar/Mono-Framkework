using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Clases_Especificas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Engine
{
    public static class ManejadorEscenas
    {
        //Controla que escena se esta reproduciendo actualmente
        //Tambien a falta de abstraccion es donde se inicializan..deberia haber un fix

        public static Escena escenaActual;
        public static List<Escena> escenas = new List<Escena>();

        public static void CargarEscenas() //Nuevas van aqui
        {
            Escena menu = new MenuInicial(0, "Menu");
            Escena juego = new EscenaDelJuego(1, "Juego");
            Escena fin = new FinJuego(2, "Fin");


        }

        public static void Agregar(Escena _escena)
        {
            escenas.Add(_escena);
        }

        public static void Cambiar(int _id)
        {
            if(escenaActual != null)
            {
                escenaActual.Descargar();
            }
            escenaActual = escenas[_id];
            escenaActual.CargarContenido();
        }

        public static void Actualizar()
        {
            escenaActual.ActualizarEscena();
        }

        public static void Dibujar(SpriteBatch spriteBatch)
        {
            escenaActual.DibujarEscena(spriteBatch);
        }

    }
}
