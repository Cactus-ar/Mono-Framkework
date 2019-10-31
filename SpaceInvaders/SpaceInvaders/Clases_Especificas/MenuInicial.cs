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
    class MenuInicial : Escena
    {
        public Boton botonPlay;
        public ObjetoJuego cosa;
        private SpriteFont fuenteTitulo;

        public MenuInicial(int _id, string _nomb) : base(_id, _nomb)
        {
        }
               
        public override void CargarContenido()
        {
            fuenteTitulo = Cargador.ObtenerFuentes("Arial");
            botonPlay = new Boton(Cargador.ObtenerTextura("buttonGreen"), new Vector2(200f, 200f), new Vector2(150f, 50f), "Iniciar", "Boton");
            botonPlay.posicion.X = (Manejador.anchoPantalla / 2f) - (botonPlay.escala.X / 2f);
            botonPlay.Click += botonPlay_Click;

            objetosEnEscena.Add(botonPlay);

            Manejador.MostrarCursor = true;
            Manejador.puntaje = 0;

        }

        public override void ActualizarEscena()
        {
            base.ActualizarEscena();
        }

        public override void DibujarEscena(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fuenteTitulo, "Space Invaders", new Vector2(250f, 40f), Color.Green);
            base.DibujarEscena(spriteBatch);
        }


        private void botonPlay_Click(object sender, EventArgs e)
        {
            ManejadorEscenas.Cambiar(1);
        }
    }
    
}
