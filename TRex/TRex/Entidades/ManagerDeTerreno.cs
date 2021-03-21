using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRex.Graficos;

namespace TRex.Entidades
{


    public class ManagerDeTerreno : IEntidadesDelJuego
    {

        private const float TILE_TERRENO_POS_Y = 119;
        private const int LARGO_SPRITE = 600;
        private const int ALTO_SPRITE = 14;

        private const int POS_X = 2;
        private const int POS_Y = 54;


        private Texture2D _hojaDeSprites;
        private readonly List<TileDeTerreno> _tilesDeTerreno;
        private readonly ManagerDeEntidades _managerDeEntidades;

        private Sprite _terrenoRegular;
        private Sprite _terrenoEscabroso;
        private Trex _trex;
        private Random _random;



        public int OrdenDeDibujo { get; set; }

        public ManagerDeTerreno(Texture2D hojaDeSprites, ManagerDeEntidades managerDeEntidades, Trex trex)
        {
            _hojaDeSprites = hojaDeSprites;
            _managerDeEntidades = managerDeEntidades;
            _tilesDeTerreno = new List<TileDeTerreno>();

            _terrenoRegular = new Sprite(hojaDeSprites, POS_X, POS_Y, LARGO_SPRITE, ALTO_SPRITE, Color.White);
            _terrenoEscabroso = new Sprite(hojaDeSprites, POS_X + LARGO_SPRITE, POS_Y, LARGO_SPRITE, ALTO_SPRITE, Color.White);

            _trex = trex;
            _random = new Random();

        }

        public void Inicializar()
        {
            _tilesDeTerreno.Clear();
            TileDeTerreno terrenoInicial = CrearTileRegular(0);
            _tilesDeTerreno.Add(terrenoInicial);
            _managerDeEntidades.Agregar(terrenoInicial);
        }

        private void GenerarTile(float posicionMaximaX)
        {
            double numeroRandom = _random.NextDouble();
            float posX = posicionMaximaX + LARGO_SPRITE;

            TileDeTerreno tileDeTerreno;

            if (numeroRandom > 0.5)
                tileDeTerreno = CrearTileEscabroso(posX);
            else
                tileDeTerreno = CrearTileRegular(posX);


            _managerDeEntidades.Agregar(tileDeTerreno);
            _tilesDeTerreno.Add(tileDeTerreno);

        }

        public void Actualizar(GameTime gameTime)
        {
            if(_tilesDeTerreno.Any())
            {
                float posMaxX = _tilesDeTerreno.Max(t => t.PosicionX);
                if (posMaxX < 0)
                    GenerarTile(posMaxX);

            }

            List<TileDeTerreno> tilesParaRemover = new List<TileDeTerreno>();

            foreach (TileDeTerreno tile in _tilesDeTerreno)
            {
                tile.PosicionX -= _trex.Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if(tile.PosicionX < -LARGO_SPRITE)
                {
                    _managerDeEntidades.Remover(tile);
                    tilesParaRemover.Add(tile);
                }

            }

            foreach (TileDeTerreno item in tilesParaRemover)
            {
                _tilesDeTerreno.Remove(item);
            }

        }

        public void Dibujar(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        private TileDeTerreno CrearTileRegular(float posicionX)
        {
            TileDeTerreno tileDeTerreno = new TileDeTerreno(posicionX, TILE_TERRENO_POS_Y, _terrenoRegular);
            return tileDeTerreno;
        }

        private TileDeTerreno CrearTileEscabroso(float posicionX)
        {
            TileDeTerreno tileDeTerreno = new TileDeTerreno(posicionX, TILE_TERRENO_POS_Y, _terrenoEscabroso);
            return tileDeTerreno;
        }
    }
}
