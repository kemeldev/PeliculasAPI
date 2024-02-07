﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController: CustonBaseController
    {
        // estos ya se podrian borrar
        //private readonly ApplicationDbContext context;
        //private readonly IMapper mapper;

        public GenerosController(
            ApplicationDbContext context,
            IMapper mapper
            ) : base (context, mapper)
        {
            // estos ya se podrian borrar
            //this.context = context;
            //this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            return await Get<Genero, GeneroDTO>();
            // Esta era la forma anterior a implementar CustonBaseController
            //var entidades = await context.Generos.ToListAsync();
            //var dtos = mapper.Map<List<GeneroDTO>>(entidades);
            //return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            return await Get<Genero, GeneroDTO>(id);
            // Esta era la forma anterior a implementar CustonBaseController
            //var entidad = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            //if (entidad == null) { return NotFound(); };
            //var dto = mapper.Map<GeneroDTO>(entidad);
            //return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            return await Post<GeneroCreacionDTO, Genero, GeneroDTO>(generoCreacionDTO, "obtenerGenero");
            // Esta era la forma anterior a implementar CustonBaseController
            //var entidad = mapper.Map<Genero>(generoCreacionDTO);
            //context.Add(entidad);
            //await context.SaveChangesAsync();
            //var generoDTO = mapper.Map<GeneroDTO>(entidad);
            //return new CreatedAtRouteResult("obtenerGenero", new { id = generoDTO.Id }, generoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put (int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            return await Put<GeneroCreacionDTO, Genero>(id, generoCreacionDTO);

            // Esta era la forma anterior a implementar CustonBaseController
            //var entidad = mapper.Map<Genero>(generoCreacionDTO);
            //entidad.Id = id;
            //context.Entry(entidad).State = EntityState.Modified;
            //await context.SaveChangesAsync();
            //return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete( int id)
        {
            return await Delete<Genero>(id);
            // Esta era la forma anterior a implementar CustonBaseController
            //var existe = await context.Generos.AnyAsync(x => x.Id == id);
            //if (!existe) { return NotFound(); }
            //context.Remove(new Genero() { Id = id });
            //await context.SaveChangesAsync();
            //return NoContent();

        }
    }
}
