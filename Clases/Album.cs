using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio14BibliotecaMusica.Clases
{
    public class Album
    {
        public string Titulo { get; set; }

        public string ArtistaAlbum { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public List<Cancion> Canciones { get; set; }

        public Album()
        {
            Canciones = new List<Cancion>();
        }
    }
}
