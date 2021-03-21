using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TRex.Entidades
{
    public interface IEntidadesDelJuego
    {
        int OrdenDeDibujo { get; }
        void Actualizar(GameTime gameTime);
        void Dibujar(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
