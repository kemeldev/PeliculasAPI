using System.ComponentModel.DataAnnotations;
using PeliculasAPI.Validaciones;

namespace PeliculasAPI.DTOs
{
    public class PeliculaCreacionDTO : PeliculaPatchDTO
    {
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        public IFormFile Poster { get; set; }
    }
}
