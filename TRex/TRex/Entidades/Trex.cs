using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TRex.Graficos;

namespace TRex.Entidades
{
    public class Trex : IEntidadesDelJuego
    {
        private const float FPS_Al_CORRER = 0.1f;
        private const float MINIMA_ALTURA_DE_SALTO = 40f;
        private const float GRAVEDAD = 1600f;
        private const float VELOCIDAD_INICIAL_SALTO = -480f;
        private const float VELOCIDAD_DE_CAIDA = -100f;
        private const float VELOCIDAD_AL_ARROJARSE = 600f;
        private const int IDLE_POS_X = 40;
        private const int IDLE_POS_Y = 0;
        public const int TREX_SPRITE_POS_X = 848;
        public const int TREX_SPRITE_POS_Y = 0;
        public const int TREX_SPRITE_POS_ANCHO = 44;
        public const int TREX_SPRITE_POS_ALTO = 52;
        private const float PARPADEO_ALEATORIO_MIN = 2f;
        private const float PARPADEO_ALEATORIO_MAX = 10f;

        private const int TREX_CORRIENDO_SPRITE_UNO_X = TREX_SPRITE_POS_X + TREX_SPRITE_POS_ANCHO * 2;
        private const int TREX_CORRIENDO_SPRITE_UNO_Y = 0;

        private const int TREX_AGACHADO_SPRITE_ANCHO = 59;
        public const int TREX_AGACHADO_SPRITE_ALTO = 52;
        private const int TREX_AGACHADO_SPRITE_UNO_X = TREX_SPRITE_POS_X + TREX_SPRITE_POS_ANCHO * 6;
        private const int TREX_AGACHADO_SPRITE_UNO_Y = 0;

        public const float VELOCIDAD_TREX_INICIAL = 280f;
        public const float VELOCIDAD_TREX_MAXIMA = 900f;

        private const float ACELERACION = 5f;

        public event EventHandler SaltoCompleto;


        private Sprite _RexIdleBackground;
        private Sprite _spriteOcioso;
        private Sprite _spriteParpadeando;
        private SoundEffect _sonidoSaltando;
        private float _velocidadVertical;
        public float _velocidadAlArrojarse;
        private float _posInicioY;

        private Random _random;

        private AnimacionDeSprite _parpadeando;
        private AnimacionDeSprite _corriendo;
        private AnimacionDeSprite _agachado;


        //public Sprite Sprite { get; private set; }
        public Vector2 Posicion { get; set; }
        public EstadosDelTRex Estado { get; private set; }
        public int OrdenDeDibujo { get; set; }
        public bool EstaVivo { get; private set; }
        public float Velocidad { get; private set; }
        

        
        public bool ComenzarASaltar()
        {
            if(Estado == EstadosDelTRex.Saltando || Estado == EstadosDelTRex.Cayendo)
            {
                return false;
            }
            else
            {
                _sonidoSaltando.Play();
                Estado = EstadosDelTRex.Saltando;
                _velocidadVertical = VELOCIDAD_INICIAL_SALTO;
                return true;
            }
        }

        public bool DejarDeSaltar()
        {
            if (Estado != EstadosDelTRex.Saltando || (_posInicioY - Posicion.Y) < MINIMA_ALTURA_DE_SALTO)
                return false;

            _velocidadVertical = _velocidadVertical < VELOCIDAD_DE_CAIDA ? VELOCIDAD_DE_CAIDA : 0;
            return true;
        }

        public bool Agacharse()
        {
            if (Estado == EstadosDelTRex.Saltando || Estado == EstadosDelTRex.Cayendo)
                return false;

            Estado = EstadosDelTRex.Agachandose;
            return true;
        }

        public bool Levantarse()
        {
            if(Estado != EstadosDelTRex.Agachandose)
              return false;

            Estado = EstadosDelTRex.Corriendo;
            return true;

        }
        
        public bool Arrojarse()
        {
            if (Estado != EstadosDelTRex.Cayendo && Estado != EstadosDelTRex.Saltando)
                return false;

            Estado = EstadosDelTRex.Cayendo;
            _velocidadAlArrojarse = VELOCIDAD_AL_ARROJARSE;
            return true;

        }

        public Trex(Texture2D textura, Vector2 posicion, SoundEffect sonidoSaltando)
        {
            //--Inicial de Entorno
            _sonidoSaltando = sonidoSaltando;
            _random = new Random();
            _RexIdleBackground = new Sprite(textura, IDLE_POS_X, IDLE_POS_Y, TREX_SPRITE_POS_ANCHO, TREX_SPRITE_POS_ALTO, Color.White);

            //-- Rex Ocioso + Inicial
            Posicion = posicion;
            Estado = EstadosDelTRex.Oscioso;
            _spriteOcioso = new Sprite(textura, TREX_SPRITE_POS_X, TREX_SPRITE_POS_Y, TREX_SPRITE_POS_ANCHO, TREX_SPRITE_POS_ALTO, Color.White);
            _spriteParpadeando = new Sprite(textura, TREX_SPRITE_POS_X + TREX_SPRITE_POS_ANCHO, TREX_SPRITE_POS_Y, TREX_SPRITE_POS_ANCHO, TREX_SPRITE_POS_ALTO, Color.White);
            _parpadeando = new AnimacionDeSprite();

            CrearParpadeo();
            _parpadeando.Comenzar();
            _posInicioY = posicion.Y;

            //-- Rex Corriendo
            _corriendo = new AnimacionDeSprite();
            _corriendo.AgregarFrame(new Sprite(textura, TREX_CORRIENDO_SPRITE_UNO_X, TREX_CORRIENDO_SPRITE_UNO_Y, TREX_SPRITE_POS_ANCHO, TREX_SPRITE_POS_ALTO, Color.White), 0);
            _corriendo.AgregarFrame(new Sprite(textura, TREX_CORRIENDO_SPRITE_UNO_X + TREX_SPRITE_POS_ANCHO, TREX_CORRIENDO_SPRITE_UNO_Y, TREX_SPRITE_POS_ANCHO, TREX_SPRITE_POS_ALTO, Color.White), FPS_Al_CORRER);
            _corriendo.AgregarFrame(new Sprite(textura, TREX_CORRIENDO_SPRITE_UNO_X, TREX_CORRIENDO_SPRITE_UNO_Y, TREX_SPRITE_POS_ANCHO, TREX_SPRITE_POS_ALTO, Color.White), FPS_Al_CORRER * 2);
            _corriendo.Comenzar();

            //--Rex Agachado
            _agachado = new AnimacionDeSprite();
            _agachado.AgregarFrame(new Sprite(textura, TREX_AGACHADO_SPRITE_UNO_X, TREX_AGACHADO_SPRITE_UNO_Y, TREX_AGACHADO_SPRITE_ANCHO, TREX_AGACHADO_SPRITE_ALTO, Color.White), 0);
            _agachado.AgregarFrame(new Sprite(textura, TREX_AGACHADO_SPRITE_UNO_X + TREX_AGACHADO_SPRITE_ANCHO, TREX_AGACHADO_SPRITE_UNO_Y, TREX_AGACHADO_SPRITE_ANCHO, TREX_AGACHADO_SPRITE_ALTO, Color.White), FPS_Al_CORRER);
            _agachado.AgregarFrame(new Sprite(textura, TREX_AGACHADO_SPRITE_UNO_X, TREX_AGACHADO_SPRITE_UNO_Y, TREX_AGACHADO_SPRITE_ANCHO, TREX_AGACHADO_SPRITE_ALTO, Color.White), FPS_Al_CORRER * 2);
            _agachado.Comenzar();

        }


        public void Inicializar()
        {
            Velocidad = VELOCIDAD_TREX_INICIAL;
            Estado = EstadosDelTRex.Corriendo;
        }

        private void CrearParpadeo()
        {
            _parpadeando.Eliminar();
            double timestampDeParpadeo = PARPADEO_ALEATORIO_MIN + _random.NextDouble() * (PARPADEO_ALEATORIO_MAX - PARPADEO_ALEATORIO_MIN);
            _parpadeando.RepetirConstantemente = false;
            _parpadeando.AgregarFrame(_spriteOcioso, 0);
            _parpadeando.AgregarFrame(_spriteParpadeando, (float)timestampDeParpadeo);
            _parpadeando.AgregarFrame(_spriteOcioso, (float)timestampDeParpadeo + 0.5f);

        }

        public void Actualizar(GameTime gameTime)
        {
            if(Estado == EstadosDelTRex.Oscioso)
            {
                if (!_parpadeando.Ejecutandose)
                {
                    CrearParpadeo();
                    _parpadeando.Comenzar();
                }

                _parpadeando.Actualizar(gameTime);

            }else if (Estado == EstadosDelTRex.Saltando || Estado == EstadosDelTRex.Cayendo)
            {

                Posicion = new Vector2(Posicion.X, Posicion.Y + _velocidadVertical * (float)gameTime.ElapsedGameTime.TotalSeconds + _velocidadAlArrojarse * (float)gameTime.ElapsedGameTime.TotalSeconds);
                _velocidadVertical += GRAVEDAD * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if(_velocidadVertical >= 0)
                {
                    Estado = EstadosDelTRex.Cayendo;
                }

                if(Posicion.Y >= _posInicioY)
                {
                    Posicion = new Vector2(Posicion.X, _posInicioY);
                    _velocidadVertical = 0;
                    Estado = EstadosDelTRex.Corriendo;
                    OnSaltoCompleto();
                }


            }else if (Estado == EstadosDelTRex.Corriendo)
            {
                _corriendo.Actualizar(gameTime);
            }
            else if (Estado == EstadosDelTRex.Agachandose)
            {
                _agachado.Actualizar(gameTime);
            }

            if(Estado != EstadosDelTRex.Oscioso)
            {
                Velocidad += ACELERACION * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Velocidad > VELOCIDAD_TREX_MAXIMA)
                Velocidad = VELOCIDAD_TREX_MAXIMA;

            _velocidadAlArrojarse = 0;

        }

        public void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(Estado == EstadosDelTRex.Oscioso)
            {
                _RexIdleBackground.Dibujar(spriteBatch, Posicion);
                _parpadeando.Dibujar(spriteBatch, Posicion);
            }
            else if (Estado == EstadosDelTRex.Saltando || Estado == EstadosDelTRex.Cayendo)
            {
                _spriteOcioso.Dibujar(spriteBatch, Posicion);
            }
            else if(Estado == EstadosDelTRex.Corriendo)
            {
                _corriendo.Dibujar(spriteBatch, Posicion);
            }
            else if (Estado == EstadosDelTRex.Agachandose)
            {
                _agachado.Dibujar(spriteBatch, Posicion);
            }
            //Sprite.Dibujar(spriteBatch, Posicion);
        }

        protected virtual void OnSaltoCompleto()
        {
            EventHandler eventHandler = SaltoCompleto;
            eventHandler?.Invoke(this, EventArgs.Empty);
        }
    
        
    
    }

    public enum EstadosDelTRex
    {
        Oscioso,
        Corriendo,
        Saltando,
        Agachandose,
        Cayendo
    }
}
