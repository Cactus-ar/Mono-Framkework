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
    class FinJuego : Escena
    {
        SpriteFont fuente;
        Boton reiniciar;
        Boton alMenu;
        public string texto = "Game Over";


        public FinJuego(int _id, string _nomb) : base(_id, _nomb)
        {
        }

        public override void CargarContenido()
        {
            fuente = Cargador.ObtenerFuentes("Arial");

            reiniciar = new Boton(Cargador.ObtenerTextura("buttonGreen"), new Vector2(200f, 200f), new Vector2(150f, 50f), "Reiniciar", "Boton");
            reiniciar.Click += Reiniciar_Click;

            alMenu = new Boton(Cargador.ObtenerTextura("buttonGreen"), new Vector2(200f, 300f), new Vector2(150f, 50f), "Menu", "Boton");
            alMenu.Click += AlMenu_Click;

            alMenu.posicion.X = (Manejador.anchoPantalla / 2f) - (alMenu.escala.X / 2f);
            reiniciar.posicion.X = (Manejador.anchoPantalla / 2f) - (reiniciar.escala.X / 2f);

            objetosEnEscena.Add(reiniciar);
            objetosEnEscena.Add(alMenu);
            Manejador.MostrarCursor = true;

        }

        public override void DibujarEscena(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fuente, texto, new Vector2(275f, 100f), Color.Orange);
            base.DibujarEscena(spriteBatch);
        }

        public override void ActualizarEscena()
        {
            base.ActualizarEscena();
        }

        private void AlMenu_Click(object sender, EventArgs e)
        {
            ManejadorEscenas.Cambiar(0);
        }

        private void Reiniciar_Click(object sender, EventArgs e)
        {
            Manejador.puntaje = 0;
            ManejadorEscenas.Cambiar(1);
        }
    }
}
