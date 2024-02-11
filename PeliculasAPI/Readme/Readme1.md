# Iniciamos repositorio

desabilitamos nullables
creamos startud
creamos carpeta entidades y la entidad genero

instalamos los packetes nugets de entityframework y tools

en appsettingjson developmente creamos los conections strings

creamos el aplicationDbcotnext y el dbset de tipo Genero llamado Generos

en el startup configuramos el servicio de adddbcontext  
services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

Hacemos las primer migracion, y el update database.

Creamos el primer controlador GenerosController y el primer metodo get,

Hay que crear los dtos y mappers para no exponer las entidades directamente al cliente, por lo que instalamos el paquete de automapper.extension.Microsoft.DependencyInjection

configuramos el servicio de automapper en services en startup

Creamos un folder helpers y una classe AutoMapperProfiles , y creamos los mapeos

Configuramos que el generoscontroller devuelve un DTO y no una entidad

<Commit>

Creamos otro metodo get pero por iD
Creamos el dto de creacion, 
hacemos el mapeo
creamos metodos de post, put, delete, y hacemos las pruebas de que funcional

------

# vamos a trabajar con la entidad Actores, la creamos
y la metemos en el db context a traves de un dbset
add migrations, update database

creamos controllador autores
creamos metodo get, post, put, delet
creamos autores DTO
creamos el mapeo de entidad autores a autores DTO

------

# recibir fotos de actores
usar Iformfile en ActorCreacion DTO
en actores controller hay que cambiar el metodo post de frombody a fromform porque por ahi va a venir al imagen

hay que hacer las validaciones para 
# 1 Definir el tipo de archivo que el usuario puede subir
# 2 el tamaño maximo del archivo

creamos carpeta validaciones y ahi la clase para validar el peso del archivo
luego esta validacion se la pasamos al actorcreacion DTO

tambien creamos la validacion para el tipo de archivo y unos enums 

--- 

# Creamos una Azure storage account

Instalamos el paquete nuget llamado Azure.Stora.Blobs

Creamos una nueva carpeta servicios para ayudarnos a trabajar con azure storage
Creamos en el una interfaz IAlmacenadorArchivos

Creamos tambien una clase AlmacenadorArchivosAzure que implemente la interfaz y creamos lo metodos para crear, editar y borrar archivos en azure

De azure obtenemos la conection strings y las colocamos en los appsettings

En startup configuramos el servicio

Ahora vamos a actores controller, cambiamos los metodos post y put, 
en el put se hizo un toque, que tambien tuvimos que modificar el AutomapperProfiles
CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore());

Se completan los metodos y se prueba su funcionamiento

----

# Se va a hacer una implementacion alternativa para guardar archivos de forma localdas0

Creamos la clase en servicios y le implementamos una dependencias distintas, ver codigo
y creamos una carpeta wwwroot

Hay que habilitar en startup el servicio y tambien una opcion para que nuestra api pueda servir static files

--- 

# Implementar HTTP Patch

creamos un nuevo DTO para que se puedan actualizar datos sin tener que cambiar foto

para no repetir codigo implementamos una herencia en ActorCreacionDTO

implementamos el metodo actores controller 
public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument)

<Importante> intstallar Microsoft.AspNetCore.JsonPatch y Otro paquete Microsoft.AspNetCore.Mvc.NewtonsoftJson

hay que configurar este servicio en startuod

tambien esto en automapper  CreateMap<ActorPatchDTO, Actor>().ReverseMap();

----

# Paginacion

crear DTO paginacion
creamos una clase auxiliar en helpers que contenga el total de recursos 
implementamos el metodo Get en actores controller

en helpers se crea otra clase para que este sistema de paginacion pueda ser implementado en cualquier request

ya con los metodos estaticos creados se pueden aplicar de forma simple en cualquier métodos

# Entidad Pelicula

Creamos entidad pelicula
Agregamos el dbset y hacemos migracions

Creamos un peliculas controller con el metodo get para la lista y get por id

hay que crear un DTO de creacionPeliculas

Creamos el metodo Post, Put patch delete, el codigo es practicamente reutilizado de los actoresController

hacemos los automappers

# Relaciones muchos a muchos

Relacionar entidades 
Generos-Peliculas  Muchos a muchos
Peliculas-Actores Muchos a Muchos

Hay que crer una tabla intermedia para relacionarlas

Creamos nueva entidad PeliculasGeneros y otra peliculas actores

 public int ActorId { get; set; }
 public int PeliculaId { get; set; }

 las agregamos en el dbcontext

 recordar que como tenemos llaves primarias dobles hay que usar el apifluente, que es el codigo en la parte de arriba que colocamos en ApplicationDbContext

 En peliculaCreacionDTO agregamos una Lista de enteros de generosID para que los clientes puedan enviarlo a la hora de crear una pelicula

 MODEL BINDING no lee bien datos como listas de enteros esn este caso, entonces hay que crear un 

 aprender mas sobre model binder de ASP.Net core

 # Custom Model Binding

 vamos a hacer un model binder para que mapee el listado de numero a nuestro List<int> en creacion DTO

 en helpers creamos una clase typeBinder 

y en pelicula creacion DTO la implementamos

# Custom Model Binding

tambien hay que hacer un binding de los listados de actores porque estos son tipos complejos

para eso creamos un DTO primero

con el binding lo que hicimos es que pasamos un generico, entonces permite que se le pase cualquier tipo de datos

----

configurar el automapper para que cuando se cree una pelicula tambien mapee el listado de generos y de actores

hay que agregar todas las +++propiedades de navegacion+++ a las entidades correspondientes esto a la entidad Pelicula

public List<PeliculasActores> PeliculasActores { get; set; }
public List<PeliculasGeneros> PeliculasGeneros { get; set; }

y agregar esto a la entidad actor

public List<PeliculasActores> PeliculasActores { get; set; }
public List<PeliculasGeneros> PeliculasGeneros { get; set; }

y agregamos esto a la entidad generos

public List<PeliculasGeneros> PeliculasGeneros { get; set; }

---

ORDEN

creamos nuevo metodo en peliculas controller 

se lo aplicamos al Post y al put

# Filtrar datos de peliculas

usamos un seed data ( un codigo para crear o meter datos en la DB a traves de codigo)
lo agregamos en Application DbContext

en el public override <debemos> agregar la seed data SeedData(modelBuilder);

hacemos la migracion y update db

Trabajamos en controlador peliculas, empezamos con el metodo Get, para que devuelva proximos estrenos y otras que están en cines

## OJO

var proximoEstreno = await context.Peliculas
                .Where(x => x.FechaEstreno > hoy)
                .OrderBy(x => x.FechaEstreno)
                .Take(top)
                .ToListAsync();

Creamos estas variables, pero para devolverlas al cliente hay que asignarlas a un DTO, por lo que creamos uno PeliculasIndexDTO que devuelve 2 listas de peliculas

# Filtrar por genero

creamos un metodo http get filtrar y creamos DTO que va a contener lo que devuelve el metodo FiltroPeliculasDTO

# Data Relacionada, como conocer el genero y los actoress cuando se hace un solicitud

Cuando el cliente hace un get por id , la pelicula le entrega su informacion, pero no la data de genero y actores relacionada

Vamos a trabajar en el método, get por id

.Include(x => x.PeliculasActores).ThenInclude(x => x.Actor)
.Include(x => x.PeliculasGeneros).ThenInclude(x => x.Genero)

con este codigo traemos la informaicon, sin embargo, el DTO no tiene propiedades que puedan recibir y pasar esta data a la respuesta enotnces

Generamos un nuevo DTO PeliculaDetallesDTO y dentro de este a su vez tendra otro DOT ActorPeliculaDetalleDTO

ahora que hay que hacer el mapeo de PeliculaDetallesDTO a pelicula

CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.Actores, options => options.MapFrom(MapPeliculasActores));


## Order by

vamos a permitir al usuario odrder los resultados de peliculas tipo ascendente o descendente del campo fecha estreno y titulo

una manera de hacerlo es:

en el metodo filtro , en este DTO FiltroPeliculasDTO, crear 2 campos o propiedades nuevas

public string CampoOrdenar { get; set; }
public bool OrdenAscendente { get; set; } = true;

y modificamos el metodo en filtro, creamos un if

if (!string.IsNullOrEmpty(filtroPeliculasDTO.CampoOrdenar))
            {
                if (filtroPeliculasDTO.CampoOrdenar == "titulo")
                {
                    if (filtroPeliculasDTO.OrdenAscendente)
                    {
                        peliculasQueryable = peliculasQueryable.OrderBy(x => x.Titulo);
                    } else
                    {
                        peliculasQueryable = peliculasQueryable.OrderByDescending(x => x.Titulo);
                    }
                }
            }

este es el codigo manual, pero tambien se puede usar una libreria que permite utilizar string para hacer el ordenamiento

System.Linq.Dynamic.Core

con esta libreria sustituimos el codigo por algo mas simpler. Ver

ya con esto funciona

Tambien se le pueden agregar algunas exepciones por si el cliente nos envia informacion erronea
try/catch

implementamos Ilogger para poder "imprimir" las exepciones


# Crear clase base para controller

Tenemos actualmente codigo repetido en controlladores, 
Ej metodo patch en peliculascontroller y actores es practicamente el mismo

Vamos a utilizar genericos para reutilizar la logica

Creamos nuevo controllador CustonBaseController que hereda de controller base y aca creamos todos los métodos genéricos para poder implementarlos en los demas controlladores

primero lo usamos en generos controller en el primer metodo get

luego en el metodo get por id, 

pero OJO hay que implementar una interfaz cuando creamos este metodo

protected async Task<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class
        {
            var entidad = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id)
debido a que al ser un generico no podemos saber si el DTO que le vamos a pasar tiene ID

creamos esta interfaz en entidades interface IId y se la pasamos a las entidades que ya existian

Tambien hacemos lo mismo con actores controller, pero aca tambien tnemos que crear nuevos metodos para nuestro CustomBaseController

en peliculas controller casi todos los metodos son personalizados por lo que no se usan los metodos genericos, unicamente en Patch


# Entidad sala cine y PeliculasSalasCines

se crean entidades, se crean propiedades de navegacion, a la entidad pelicula tambien le agregamos propuedades de navegacion

en dbcontext creamos los nuevos dbsets

y tambien en application dbcontext configuramos la llave primaria que representa la relacion muchos a muchos
modelBuilder.Entity<PeliculasSalasCine>()
                .HasKey(x => new { x.PeliculaId, x.SalaDeCineId });

creamos la migraciones

luego creamos los DTOs correspondientes , SaladecineDTO y SalaDeCineCreacionDTO

Creamos el controller SalasDeCineControllers, practicamente todos los metodos son facilmente aplicados con los genericos previamente creados

Configuramos el automapper

CreateMap<SalaDeCine, SalaDeCineDTO>().ReverseMap();
CreateMap<SalaDeCineCreacionDTO, SalaDeCine>();

# Agregar campo de ubicacion a la sala de cine, para hacer queryes espaciales (sala mas cercana y a que distancia)

instalamos la libreria NetTopologieSuite, OJO la version de entityframework core

Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite

luego la configuramos en el startup, en AddDbcontext

services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions => sqlServerOptions.UseNetTopologySuite()
                ));

vamos a la entidad de sala de cine y agregamos un campo para representar ubicacion geográfica

public Point Ubicacion { get; set; }

y agregamos una AddMigration SalaCineUbicacion

<Tenemos> un problema con el typo Point, decidí no proceder con esta seccion porque despues podriamos tener algun otro problema con codigo mas adelante

Vamos a saltarla

# Sistemas de usuarios

Lo primero es que el application DBcontext herede de IdentityDbContext
hay que instalar el paruete de asp.net Microsoft.AspNetCore.Identity.EntityFrameworkCore

en el startup hacemos las configuraciones

services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
            ClockSkew = TimeSpan.Zero
        }
    )

practicamentes las copiamos , ademas hay que instalar el paquete de asp.net core 
Microsoft.AspNetCore.Authentication.JwtBearer

esta llave, "jwt:key"

hayq que crearla en el app settings JSON

--- Creamos el cuentas controller --- y pegamos el codigo del controllador que ya habiamos creado en el curso

hay que crear varios DTOs, UserToken, UserInfo, UsuarioDTO, EditarRolDTO

hasta aca el proyecto Compila

# Vamos a crear un usuario de prueba y agregarle el rol de admin

en el appdbcontext en seed data agregamos:

var rolAdminId = "cad66cba-8674-43bf-9fe5-ca2daac4a818";
            var usuarioAdminId = "135dcaea-5dca-4f3c-ac63-28d7df8ac38f";

var rolAdmin = new IdentityRole()
{
    Id = rolAdminId,
    Name = "Admin",
    NormalizedName = "Admin"
};

var passwordHasher = new PasswordHasher<IdentityUser>();

var username = "kemel.developer@gmail.com";

var usuarioAdmin = new IdentityUser()
{
    Id = usuarioAdminId,
    UserName = username,
    NormalizedUserName = username,
    Email = username,
    NormalizedEmail = username,
    PasswordHash = passwordHasher.HashPassword(null, "Aa123456!")
};

Y hacemos un migration Add-Migration TablasIdentity y el update database

ahora en appdbcontext agregamos esto

modelBuilder.Entity<IdentityUser>()
                .HasData(rolAdmin);

modelBuilder.Entity<IdentityRole>()
    .HasData(rolAdmin);

modelBuilder.Entity<IdentityUserClaim<string>>()
    .HasData(new IdentityUserClaim<string>()
    {
        Id = 1,
        ClaimType = ClaimTypes.Role,
        UserId = usuarioAdminId,
        ClaimValue = "Admin"
    });

y agregamos una migracion Admin Data

Estamos haciendo estas migraciones por separado para poder modificarlas, porque se crear un ConcurrencyStamp que aparentemente hay que estarlo cambiando, 
para evitar eso:

lo que vamos a hacer es general el SQL de la migracion Admin Data y luego vamos a agregar ese SQL en una nueva migracion

para eso en el package manager console : Script-Migration TablasIdentity ( el nombre de la migracion anterior)

se crea un nueva tabla con el sql

eliminamos esto que estaba al final

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240211180041_AdminData', N'8.0.1');
GO

y copiamos estos datos que salieron de la ultima migracion que se llamaba Admin Data

migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cad66cba-8674-43bf-9fe5-ca2daac4a818");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "135dcaea-5dca-4f3c-ac63-28d7df8ac38f");


y hacemos remove migration que borra esa migracion

Ahora creamos una nueva migracion, de nuevo le llamamos AdminData, pero esta vez estaria vacia

y la llenamos on los datos del script, 

Ahora en la base de datos de SQL, ya tenemos al usuario creado, ademas identificado con rol de administrador y en claim, tambien aparece

ahora si en postman hago un post a https://localhost:7073/api/cuentas/login

con este body

{
    "email": "kemel.developer@gmail.com",
    "password": "Aa123456!"
}

tengo una respuesta Ok y nos envian un token con expiracion

{"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoia2VtZWwuZGV2ZWxvcGVyQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImtlbWVsLmRldmVsb3BlckBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEzNWRjYWVhLTVkY2EtNGYzYy1hYzYzLTI4ZDdkZjhhYzM4ZiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzM5Mjk4NDExfQ.tKqlZ8HMn3sLMOW83wQV9D-JaWxLzLVqKMVDvfKfEgo","expiracion":"2025-02-11T18:26:51.4862988Z"}


ahora puedo ir a este enlace en postman https://localhost:7073/api/cuentas/usuarios

y meter en autorizacion mi token: y darle get

Tenemos un error de mapeo.

Agregamos este que no estaba
CreateMap<IdentityUser, UsuarioDTO>();
ya funciona

Ahora tambien podemos crear usuarios

https://localhost:7073/api/cuentas/crear

{
    "email": "usuarioNormal@gmail.com",
    "password" : "Aa123456!"
}

se crea y nos entrega un token

{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXN1YXJpb05vcm1hbEBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1c3VhcmlvTm9ybWFsQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiNmIwNWMzOWUtNmRkMC00MDc2LWI1ZmYtZDY1N2ExYmMwZWMxIiwiZXhwIjoxNzM5Mjk5MjU4fQ.TfFGAX7qJkps49-VE2miziQg1FUCGK3GIJ0NaJVaYWY",
    "expiracion": "2025-02-11T18:40:58.768759Z"

 Ahora si este nuevo usuario con su token intenta ingresar a ver la lista de usuario

 https://localhost:7073/api/cuentas/usuarios

 va a recibir un 403 forbiden



