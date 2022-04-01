Bienvenidos a Examen Tecnico API - Alan Diaz

Se crearon dos endpoints:
## /api/Home/login 
el primero simula un login que necesita usuario y contraseña, si los dastos son validos devuele un token.

## Usuario Valido
"User": "flock",
"Password": "1234",

## /api/Home/getLatitudLongitud/{provincia}
el segundo mediante el token generado en el login y el nombre de una provincia devuelve latitud y logintud de la misma.

## Token Valido
"Token": "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz"

## Lista de Provincias
"Misiones"
"San Luis"
"San Juan"
"Entre Ríos"
"Santa Cruz"
"Río Negro"
"Chubut"
"Córdoba"
"Mendoza"
"La Rioja"
"Catamarca"
"La Pampa"
"Santiago del Estero"
"Corrientes"
"Santa Fe"
"Tucumán"
"Neuquén"
"Salta"
"Chaco"
"Formosa"
"Jujuy"
"Ciudad Autónoma de Buenos Aires"
"Buenos Aires"
"Tierra del Fuego, Antártida e Islas del Atlántico Sur"


## Swagger
Se utilizo swagger para la documentacion de la api

## NLog
Se utilizo NLog para los logs. La ubicacion de log es "C:\Log\applog-${shortdate}.log"

## Aclaraciones
- No utilice base de datos porque para el login se pedia un unico usuario parametrizado, el cual se setea mediante el appsetings.json
- El token es unico y esta hardcodeado a modo de prueba. En una api real tendria un metodo que lo genere y lo persisteria en la base para luego validar si todavia esta activo o caduco
- Se realizaron pruebas unitarias para ambos endpoints
- En caso que hubiera usado base de datos. Habria utilizado entityFramework y code first: crear el context, agregar la migracion y updetear la base
- En caso de tener base de datos tambien hubiera agregado la Service layer y la DAL