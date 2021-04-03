using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TRex.Entidades;
using TRex.Graficos;
using TRex.Sistemas;

namespace TRex
{

    public enum EstadosDelJuego
    {
        Inicial,
        Transicion,
        Jugando,
        JuegoTermiando
    }


    public class ChromeTRex : Game
    {
        //--Declarar los nombres de los asset
        private const string SPRITE_SHEET = "100-offline-sprite";
        private const string SONIDO_BOTON = "button-press";
        private const string SONIDO_IMPACTO = "sounds_hit";
        private const string SONIDO_RECORDALCANZADO = "score-reached";

        //-- Config General
        public const int LARGO_VENTANA = 600;
        public const int ALTO_VENTANA = 150;
        public const int TREX_POS_INICIAL_Y = ALTO_VENTANA - 16;
        public const int TREX_POS_INICIAL_X = 1;
        private const float VELOCIDAD_FADEIN = 900f;
        private const int PUNTAJE_POS_X = LARGO_VENTANA - 130;
        private const int PUNTAJE_POS_Y = 10;


        //-- Declarar variables de Sonido
        private SoundEffect _sfxBotonPresionado;
        private SoundEffect _sfxImpacto;
        private SoundEffect _sfxRecordAlcanzado;

        private ControladorDeEntrada _entrada;

        //-- Declarar variables de Imagenes
        private Texture2D _hojaDeImagenes;
        private Texture2D _texturaDelFadeIn;
        private KeyboardState _estadoPrevioDelTeclado;
        private float _posXDeTexturaFadeIn;



        //--Entidades
        private Trex _trex;
        private ManagerDeObstaculos _obstaculos;
        private ManagerDeTerreno _terreno;
        private MostrarPuntaje _puntajes;
        private EstadosDelJuego _estadosDelJuego;


        //-- Manejadores por defecto de sprites y tarjeta grafica
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //-- Engine
        private ManagerDeEntidades _manager;


        public ChromeTRex()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _manager = new ManagerDeEntidades();
            _estadosDelJuego = EstadosDelJuego.Inicial;
            _posXDeTexturaFadeIn = Trex.TREX_SPRITE_POS_ANCHO;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferHeight = ALTO_VENTANA;
            _graphics.PreferredBackBufferWidth = LARGO_VENTANA;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //--Cargar los assets
            _sfxBotonPresionado = Content.Load<SoundEffect>(SONIDO_BOTON);
            _sfxImpacto = Content.Load<SoundEffect>(SONIDO_IMPACTO);
            _sfxRecordAlcanzado = Content.Load<SoundEffect>(SONIDO_RECORDALCANZADO);

            _hojaDeImagenes = Content.Load<Texture2D>(SPRITE_SHEET);

            _texturaDelFadeIn = new Texture2D(GraphicsDevice, 1, 1);
            _texturaDelFadeIn.SetData(new Color[] { Color.White });

            _trex = new Trex(_hojaDeImagenes, new Vector2(TREX_POS_INICIAL_X, TREX_POS_INICIAL_Y - Trex.TREX_SPRITE_POS_ALTO), _sfxBotonPresionado);
            _trex.OrdenDeDibujo = 10;
            _trex.SaltoCompleto += trex_SaltoCompleto;

            _puntajes = new MostrarPuntaje(_hojaDeImagenes, new Vector2(PUNTAJE_POS_X, PUNTAJE_POS_Y), _trex);
            //_puntajes.Puntaje = 498;
            //_puntajes.PuntajeRecord = 12345;

            _entrada = new ControladorDeEntrada(_trex);
            _terreno = new ManagerDeTerreno(_hojaDeImagenes, _manager, _trex);
            _obstaculos = new ManagerDeObstaculos(_hojaDeImagenes, _manager, _trex, _puntajes);
            
            _manager.Agregar(_trex);
            _manager.Agregar(_terreno);
            _manager.Agregar(_puntajes);
            _manager.Agregar(_obstaculos);
            _terreno.Inicializar();
        }

        private void trex_SaltoCompleto(object sender, EventArgs e)
        {
            if(_estadosDelJuego == EstadosDelJuego.Transicion)
            {
                _estadosDelJuego = EstadosDelJuego.Jugando;
                _trex.Inicializar();
                _obstaculos.estaActivo = true;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            KeyboardState estado = Keyboard.GetState();

            if (_estadosDelJuego == EstadosDelJuego.Jugando)
                _entrada.ProcesarControles(gameTime);
            else if (_estadosDelJuego == EstadosDelJuego.Transicion)
                _posXDeTexturaFadeIn += (float)gameTime.ElapsedGameTime.TotalSeconds * VELOCIDAD_FADEIN;
            else if (_estadosDelJuego == EstadosDelJuego.Inicial)
            {
                bool esPrimerPresionDeTecla = estado.IsKeyDown(Keys.Up);
                bool presionDeTecla = _estadoPrevioDelTeclado.IsKeyDown(Keys.Up);

                if (esPrimerPresionDeTecla && !presionDeTecla)
                    ComenzarJuego();
            }


            _manager.Actualizar(gameTime);
            _estadoPrevioDelTeclado = estado;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _manager.Dibujar(gameTime, _spriteBatch);

            if(_estadosDelJuego == EstadosDelJuego.Inicial || _estadosDelJuego == EstadosDelJuego.Transicion)
            {
                _spriteBatch.Draw(_texturaDelFadeIn, new Rectangle((int)Math.Round(_posXDeTexturaFadeIn), 0, LARGO_VENTANA, ALTO_VENTANA), Color.White);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        private bool ComenzarJuego()
        {
            if (_estadosDelJuego != EstadosDelJuego.Inicial)
                return false;

            _estadosDelJuego = EstadosDelJuego.Transicion;
            _trex.ComenzarASaltar();
            return true;
        }
    }
}
