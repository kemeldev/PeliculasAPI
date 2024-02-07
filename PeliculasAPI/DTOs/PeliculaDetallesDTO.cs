namespace PeliculasAPI.DTOs
{
    public class PeliculaDetallesDTO : PeliculasDTO
    {
        public List<GeneroDTO> Generos { get; set; }
        public List<ActorPeliculaDetalleDTO> Actores { get; set;  }
    }
}
