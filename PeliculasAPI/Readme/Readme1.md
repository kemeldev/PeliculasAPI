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

vamos a trabajar con la entidad Actores, la creamos
y la metemos en el db context a traves de un dbset
add migrations, update database

creamos controllador autores
creamos metodo get, post, put, delet
creamos autores DTO
creamos el mapeo de entidad autores a autores DTO

------

recibir fotos de actores
usar Iformfile en ActorCreacion DTO
en actores controller hay que cambiar el metodo post de frombody a fromform porque por ahi va a venir al imagen

hay que hacer las validaciones para 
# 1 Definir el tipo de archivo que el usuario puede subir
# 2 el tamaño maximo del archivo

creamos carpeta validaciones y ahi la clase para validar el peso del archivo
luego esta validacion se la pasamos al actorcreacion DTO

tambien creamos la validacion para el tipo de archivo y unos enums 

--- 

Creamos una Azure storage account

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

Se va a hacer una implementacion alternativa para guardar archivos de forma local

