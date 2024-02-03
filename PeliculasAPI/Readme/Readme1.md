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