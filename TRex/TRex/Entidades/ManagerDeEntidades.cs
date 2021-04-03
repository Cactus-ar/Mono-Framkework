using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TRex.Entidades
{
    public class ManagerDeEntidades 
    {
        private readonly List<IEntidadesDelJuego> _listaDeEntidades = new List<IEntidadesDelJuego>();
        private readonly List<IEntidadesDelJuego> _listaDeEntidadesParaAgregar = new List<IEntidadesDelJuego>();
        private readonly List<IEntidadesDelJuego> _listaDeEntidadesParaRemover = new List<IEntidadesDelJuego>();

        public IEnumerable<IEntidadesDelJuego> Entidades => new ReadOnlyCollection<IEntidadesDelJuego>(_listaDeEntidades);


        public void Actualizar(GameTime gameTime)
        {
            foreach (IEntidadesDelJuego entidad in _listaDeEntidades)
            {
                entidad.Actualizar(gameTime);
            }

            foreach (IEntidadesDelJuego entidad in _listaDeEntidadesParaAgregar)
            {
                _listaDeEntidades.Add(entidad);
            }

            _listaDeEntidadesParaAgregar.Clear();

            foreach (IEntidadesDelJuego entidad in _listaDeEntidadesParaRemover)
            {
                _listaDeEntidades.Remove(entidad);
            }

            _listaDeEntidadesParaRemover.Clear();

        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (IEntidadesDelJuego entidad in _listaDeEntidades.OrderBy(e => e.OrdenDeDibujo))
            {
                entidad.Dibujar(spriteBatch, gameTime);
            }

        }

        public void Agregar(IEntidadesDelJuego entidad)
        {
            if(entidad is null)
            {
                throw new ArgumentNullException(nameof(entidad), "Entidad nula no puede agregarse");
            }else
            {
                _listaDeEntidadesParaAgregar.Add(entidad);
            }
        }

        public void Remover(IEntidadesDelJuego entidad)
        {
            if (entidad is null)
            {
                throw new ArgumentNullException(nameof(entidad), "Entidad nula no puede agregarse");
            }
            else
            {
                _listaDeEntidadesParaRemover.Add(entidad);
            }
        }

        public void EliminarTodo()
        {
            _listaDeEntidadesParaRemover.AddRange(_listaDeEntidades);
        }


        public IEnumerable<T> ObtenerEntidadesDelTipo<T>() where T: IEntidadesDelJuego
        {
            return _listaDeEntidades.OfType<T>();
        }

    }
}
