using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Engine
{
    public abstract class Escena
    {
        //Todas derivan de aqui
        public int indiceEscena;
        public string nombreEscena;
        public List<ObjetoJuego> objetosEnEscena = new List<ObjetoJuego>();

        public Escena(int _id, string _nomb)
        {
            indiceEscena = _id;
            nombreEscena = _nomb;
            ManejadorEscenas.Agregar(this);

        }

        public void AgregarObjeto(ObjetoJuego _obj)
        {
            objetosEnEscena.Add(_obj);
        }

        public abstract void CargarContenido();

        public void Descargar()
        {
            objetosEnEscena.Clear();
        }

        public virtual void ActualizarEscena()
        {
            foreach (ObjetoJuego item in objetosEnEscena)
            {
                if (item.esVisible)
                {
                    item.Actualizar();
                }
            }
        }

        public virtual void DibujarEscena(SpriteBatch spriteBatch)
        {
            foreach (ObjetoJuego item in objetosEnEscena)
            {
                if (item.esVisible)
                {
                    item.Dibujar(spriteBatch);
                }
            }
        }



    }
}
