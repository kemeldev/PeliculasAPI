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






