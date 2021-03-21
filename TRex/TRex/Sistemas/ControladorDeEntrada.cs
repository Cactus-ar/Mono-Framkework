using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TRex.Entidades;

namespace TRex.Sistemas
{
    public class ControladorDeEntrada
    {
        private Trex _trex;
        private KeyboardState _estadoPrevioDelTeclado;

        public ControladorDeEntrada(Trex trex)
        {
            _trex = trex;
        }

        public void ProcesarControles(GameTime gameTime)
        {
            KeyboardState estadoDelTeclado = Keyboard.GetState();

            if (!_estadoPrevioDelTeclado.IsKeyDown(Keys.Up) && estadoDelTeclado.IsKeyDown(Keys.Up))
            {
                if (_trex.Estado != EstadosDelTRex.Saltando)
                    _trex.ComenzarASaltar();

            }else if(_trex.Estado == EstadosDelTRex.Saltando && !estadoDelTeclado.IsKeyDown(Keys.Up))
            {
                _trex.DejarDeSaltar();
            }
            else if (estadoDelTeclado.IsKeyDown(Keys.Down))
            {
                if (_trex.Estado == EstadosDelTRex.Saltando || _trex.Estado == EstadosDelTRex.Cayendo)
                    _trex.Arrojarse();
                else
                    _trex.Agacharse();

            }
            else if (_trex.Estado == EstadosDelTRex.Agachandose && !estadoDelTeclado.IsKeyDown(Keys.Down))
            {
                _trex.Levantarse();
            }

            _estadoPrevioDelTeclado = estadoDelTeclado;

        }
    }
}
