using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Clases_Especificas
{
    public class ControladorEnemigos
    {
        private int cantidadAncho; //aliens a lo ancho
        private int cantidadAlto; //aliens a lo alto -> basicamente una grilla
        private float espacio = 20f; //espacio entre cada uno de los aliens

        private int enemigosVisibles; //usado para detemrinar una situacion de fin de juego
        private List<ObjetoJuego> listaEnemigos = new List<ObjetoJuego>(); //lo mismo

        private Escena escena; //este script a que escena le pertenece

        public ControladorEnemigos(int cantidadAncho, int cantidadAlto, Escena escena)
        {
            this.cantidadAncho = cantidadAncho;
            this.cantidadAlto = cantidadAlto;
            this.escena = escena;
            GenerarEnemigos();
        }

        private void GenerarEnemigos()
        {
            Texture2D texturaEnemigo = Cargador.ObtenerTextura("InvaderA1"); //agregar mas texturas luego

            for (int i = 0; i < cantidadAncho; i++)
            {
                for (int j = 0; j < cantidadAlto; j++)
                {
                    Vector2 _escala = new Vector2(75f, 75f); //tamaño del bicho
                    float xPos = ((_escala.X + espacio) * i) + 100f; //calcular posicion del bicho en X
                    float yPos = ((_escala.Y + espacio) * j) + 100f; //calcular posicion del bicho en Y
                    ObjetoJuego enemigo = new Enemigo(texturaEnemigo, new Vector2(xPos, yPos), _escala, "Enemigo");
                    escena.AgregarObjeto(enemigo);
                    listaEnemigos.Add(enemigo);

                }
            }
        }

        public void ChequearCantidad()
        {
            enemigosVisibles = 0;
            foreach (Enemigo item in listaEnemigos)
            {
                if (item.esVisible)
                {
                    enemigosVisibles += 1;
                }

            }

            if(enemigosVisibles == 0)
            {
                //fin del juego parece
                ManejadorEscenas.Cambiar(2);
            }
        }




    }
}
