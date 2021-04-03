using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TRex.Entidades
{
    public class ManagerDeObstaculos : IEntidadesDelJuego
    {

        private const float DISTANCIA_MINIMA_SPAWN = 20;
        private const int DISTANCIA_MINIMA_ENTRE_OBSTACULOS = 10;
        private const int DISTANCIA_MAXIMA_ENTRE_OBSTACULOS = 50;

        private const int TOLERANCIA = 5;

        private double _ultimoPuntajeMostrado = -1;
        private double _distanciaActualObjetivo;

        private readonly ManagerDeEntidades _managerDeEntidades;
        private readonly Trex _trex;
        private readonly MostrarPuntaje _puntajes;
        private readonly Random _random;
        private Texture2D _textura;
        public bool estaActivo { get; set; }
        public bool permitirObstaculos => estaActivo && _puntajes.Puntaje >= DISTANCIA_MINIMA_SPAWN;
        
        public int OrdenDeDibujo => 0;

        public ManagerDeObstaculos(Texture2D textura, ManagerDeEntidades managerDeEntidades, Trex trex, MostrarPuntaje mostrarPuntaje)
        {
            _textura = textura;
            _managerDeEntidades = managerDeEntidades;
            _trex = trex;
            _puntajes = mostrarPuntaje;
            _random = new Random();

        }


        public void Actualizar(GameTime gameTime)
        {
            if (!estaActivo)
                return;

            if(permitirObstaculos && (_ultimoPuntajeMostrado <= 0 || (_puntajes.Puntaje - _ultimoPuntajeMostrado >= _distanciaActualObjetivo)))
            {
                _distanciaActualObjetivo = _random.NextDouble() * (DISTANCIA_MAXIMA_ENTRE_OBSTACULOS - DISTANCIA_MINIMA_ENTRE_OBSTACULOS) + DISTANCIA_MINIMA_ENTRE_OBSTACULOS;
                _distanciaActualObjetivo += (_trex.Velocidad - Trex.VELOCIDAD_TREX_INICIAL) / (Trex.VELOCIDAD_TREX_MAXIMA - Trex.VELOCIDAD_TREX_INICIAL) * TOLERANCIA;
                _ultimoPuntajeMostrado = _puntajes.Puntaje;

                GenerarObstaculoAleatroio();

            }

            foreach (Obstaculo _obstaculo in _managerDeEntidades.ObtenerEntidadesDelTipo<Obstaculo>())
            {
                if (_obstaculo.Posicion.X < -200)
                    _managerDeEntidades.Remover(_obstaculo);
            }

        }

        private void GenerarObstaculoAleatroio()
        {
            Obstaculo obstaculo = null;
            GrupoDeCactus.Tamanios randomTamanio = (GrupoDeCactus.Tamanios) _random.Next((int)GrupoDeCactus.Tamanios.pequenios, (int)GrupoDeCactus.Tamanios.largos + 1);
            bool esLargo = _random.NextDouble() > 0.5f;

            float coordY = esLargo ? 80 : 94;

            var posicion = new Vector2(ChromeTRex.LARGO_VENTANA, coordY);

            obstaculo = new GrupoDeCactus(_textura, esLargo, randomTamanio, _trex, posicion);

            _managerDeEntidades.Agregar(obstaculo);


        }

        public void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }
    }
}
