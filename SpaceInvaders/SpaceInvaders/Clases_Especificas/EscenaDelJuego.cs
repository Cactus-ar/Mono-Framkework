using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Clases_Especificas
{
    public class EscenaDelJuego : Escena
    {

        private SpriteFont fuente;
        private string texto = "Puntaje: ";
        private ObjetoJuego jugador;
        private ControladorEnemigos enemigos;



        public EscenaDelJuego(int _id, string _nomb) : base(_id, _nomb)
        {
        }

        public override void CargarContenido()
        {
            fuente = Cargador.ObtenerFuentes("Arial");
        }
    }
}
