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
    public class Boton : ObjetoJuego
    {
        private Color colorDeDibujo;
        private Color colorNormal = Color.White;
        private Color colorResaltado = Color.Yellow;

        public event EventHandler Click;
        public string texto;
        private SpriteFont fuente;

        public Boton(Texture2D _txt, Vector2 _pos, Vector2 _esc, string _texto, string _tag) : base(_txt, _pos, _esc, _tag)
        {
            esColisionable = false;
            texto = _texto;
            fuente = Cargador.ObtenerFuentes("Arial");
        }

        public override void Actualizar()
        {
            if (IntersectaMouse())
            {
                EntraAlMouse();
            }
            else
            {
                SaleDelMouse();
            }

            if(IntersectaMouse() && Entrada.BotonMousePresionado(0))
            {
                Click?.Invoke(this, new EventArgs());
            }
        }

        public override void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, ObtenerLimites(), colorDeDibujo);
            spriteBatch.DrawString(fuente, texto, PosFuente(), Color.Black);
        }

        private Vector2 PosFuente()
        {
            Vector2 puntoMedio = new Vector2(posicion.X + escala.X / 2, posicion.Y + escala.Y / 2);
            Vector2 tamanioTexto = fuente.MeasureString(texto);
            Vector2 puntoMedioTexto = new Vector2(tamanioTexto.X / 2, tamanioTexto.Y / 2);
            Vector2 posicionTexto = new Vector2((int)(puntoMedio.X - tamanioTexto.X), (int)(puntoMedio.Y) - tamanioTexto.Y);
            return posicionTexto;
        }

        public virtual void EntraAlMouse()
        {
            colorDeDibujo = colorResaltado;
        }

        public virtual void SaleDelMouse()
        {
            colorDeDibujo = colorNormal;
        }

        public virtual void MouseClick() //puede implementarse el cambio de color al hacer click
        {

        }

        private bool IntersectaMouse()
        {
            Vector2 mousePos = Entrada.ObtenerPosicionMouse();
            Rectangle mouseRect = new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1);
            if (mouseRect.Intersects(ObtenerLimites()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
