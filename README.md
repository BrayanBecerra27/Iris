# Prueba Iris - Dev FullStack
# IrisExam

En este repositorio se encuentra la solución a la prueba técnica de Dev Full Stack de Iris, el cuál consiste en una web app con la funcionalidad de un To Do List.

## 1) Iris_Front
    Aplicación web

  - Desarrollo en angular 15  
  - Funcionalidades
    - Agregar tareas mediante una descripción.
    - Marcar tareas como completadas, mediante un checkbox.
    - Marcar tareas como favoritas, mediante un icono (botón) de estrella.
    - Eliminar tareas del To Do List.
    - Listar las tareas mediante un filtro con las siguientes opciones:
	      -All
      	-Favorite
      	-Completed


## Live server
La aplicación se encuentra desplegada en un App Service Static de Azure. AL cual se puede acceder mediante: `https://icy-pebble-01255190f.6.azurestaticapps.net/`

## Development server

Para la ejecución de proyecto en local, debe contar con:
  - Angular 15+.
  - node js v15+.

Abrir el Workspace del path /Iris_Front

Restaure dependencias: `npm install`

Ejecute aplicación `ng serve`. 

Navegue a `http://localhost:4200`

## 2) IrisBack 
    Api 

  - Desarrollada en .net8.
  - Uso de DynamoDB para persistencia de datos. 
  - Arquitectura limpia, orientada al dominio.
  - CI/CD Implementado con Github Actions.
  - Despliegue en Azure app services
  - Colleción de postman: Iris-test.postman_collection.json - (se encuentra en la carpeta raíz del repositorio).

## Live server

La Api se desplegó en un App Services de Azure. AL cual se puede acceder mediante: `https://iris-todo-dev-ctcvdmf9a2ejd5et.canadaeast-01.azurewebsites.net/`

## Development server

Instalar SDK .Net 8
Configurar las siguientes variables en el archivo appsetting.json

    "Jwt": {
        "Key": "llave de 512 bits",
        "Issuer": "url del api",
        "Audience": "url del api"
        
    },
  "AWS": {
    "Profile": "default",
    "Region": "región donde se encuentra la tabla de DynamoDB"
  }

y las siguientes en launchSettings.json

  "AWS_ACCESS_KEY_ID": "su AWS_ACCESS_KEY_ID",
  "AWS_SECRET_ACCESS_KEY": "su AWS_SECRET_ACCESS_KEY"
  
Sobre el path ./IrisBack/Iris

Ejecutar aplicación.

## Postman

Se adjunta la collection de Postman con los Request para las funcionalidades mencionadas en la sección 1. 
La Api cuenta con autienticación JWT.
Para esto se necesita consumir el servicio de '{baseurl}/auth/login'. Con el siguiente payload:
{
    "username" : "usuario",
    "password" : "1234"
}

La aplicación no cuenta con gestión de usuarios, por lo cuál se simular con estas credenciales el acceso.

con el token devuelto, se configura la autenticación de Auth 2.0 para el resto de Request. Los cuales son un CRUD sobre la tabla de TaskToDo de Amazon DynamoDB.
