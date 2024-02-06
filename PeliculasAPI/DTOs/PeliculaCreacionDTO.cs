using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Helpers;
using PeliculasAPI.Validaciones;

namespace PeliculasAPI.DTOs
{
    public class PeliculaCreacionDTO : PeliculaPatchDTO
    {
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIDs { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculasCreacionDTO>>))]
        public List<ActorPeliculasCreacionDTO> Actores { get; set; }
    }
}
