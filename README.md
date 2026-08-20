# Sistema de Gestión Inmobiliaria

> Aplicación web desarrollada con ASP.NET Core MVC para la gestión de una inmobiliaria.  
> En esta primera entrega se implementa el ABM (Alta, Baja y Modificación) de Propietarios e Inquilinos.

---

## 👥 Integrantes del Grupo

- **Antonio Tomas Assat** - GitHub: [AntonioAssat](https://github.com/AntonioAssat)
- **Ana Paula Quevedo** - GitHub: [Quevedoana]

---

## 🛠️ Tecnologías Utilizadas

- **C#**
- **ASP.NET Core MVC**
- **.NET 10**
- **MySQL**
- **DBeaver**
- **HTML / CSS**
- **Git**
- **GitHub**
- **Visual Studio Code**

---

## 📋 Primera Entrega

Para esta primera entrega se desarrolló el **ABM (Alta, Baja y Modificación) de Propietarios e Inquilinos**, utilizando el patrón arquitectónico **MVC (Model-View-Controller)**.

### Propietarios

Se implementaron las siguientes funcionalidades:

- Listado de propietarios.
- Alta de propietarios.
- Modificación de propietarios.
- Baja de propietarios.

### Inquilinos

Se implementaron las siguientes funcionalidades:

- Listado de inquilinos.
- Alta de inquilinos.
- Modificación de inquilinos.
- Baja de inquilinos.

---

## 🏗️ Arquitectura del Proyecto

El proyecto utiliza el patrón **MVC (Model-View-Controller)**.

### Model

Contiene las entidades y la lógica relacionada con el acceso a los datos.

Entre los principales elementos se encuentran:

- `Propietario`
- `Inquilino`
- `IRepositorio`
- `IRepositorioPropietario`
- `IRepositorioInquilino`
- `RepositorioBase`
- `RepositorioPropietario`
- `RepositorioInquilino`

### Controller

Contiene los controladores encargados de recibir las solicitudes del usuario y ejecutar las operaciones correspondientes.

- `HomeController`
- `PropietariosController`
- `InquilinosController`

### View

Contiene las vistas que se muestran al usuario para realizar las distintas operaciones del sistema.

Se incluyen vistas para:

- Listado.
- Alta.
- Modificación.
- Baja.

---


## 🗂️ Estructura del Proyecto

La estructura principal del proyecto es la siguiente:

```text
AnaYAntonio-ProyectoInmobiliaria/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── InquilinoController.cs
│   └── PropietariosController.cs
│
├── Models/
│   ├── ErrorViewModel.cs
│   ├── Inquilino.cs
│   ├── IRepositorio.cs
│   ├── IRepositorioInquilino.cs
│   ├── IRepositorioPropietario.cs
│   ├── Propietario.cs
│   ├── RepositorioBase.cs
│   ├── RepositorioInquilino.cs
│   └── RepositorioPropietario.cs
│
├── Views/
│   ├── Home/
│   ├── Inquilinos/
│   ├── Propietarios/
│   └── Shared/
│
├── wwwroot/
│
├── docs/
│   └── DER.png
│
├── Inmobiliaria.sql
├── README.md
├── .gitignore
├── appsettings.json
├── appsettings.Development.json
└── AnaYAntonio-ProyectoInmobiliaria.csproj

## 🗄️ Base de Datos

El proyecto utiliza **MySQL** como sistema gestor de base de datos.

Para esta primera entrega se implementaron las entidades correspondientes a:

- **Propietario**
- **Inquilino**

La aplicación utiliza repositorios para realizar las operaciones de acceso y modificación de los datos almacenados en la base de datos.

### 📋 Tablas

#### Tabla `Propietario`

Contiene la información correspondiente a los propietarios de los inmuebles.

Los datos principales almacenados son:

- Identificador del propietario.
- DNI.
- Nombre completo.
- Teléfono.
- Mail.

#### Tabla `Inquilino`

Contiene la información correspondiente a los inquilinos.

Los datos principales almacenados son:

- Identificador del inquilino.
- Nombre completo.
- DNI.
- Teléfono.
- Mail.
- Estado.

### 🔌 Conexión a la Base de Datos

La aplicación utiliza una conexión a MySQL mediante una cadena de conexión configurada en ASP.NET Core.

Para el acceso a los datos se utiliza un repositorio base:

`RepositorioBase`

Este repositorio centraliza la configuración de la conexión y permite que los repositorios específicos reutilicen la misma conexión.

Los repositorios implementados son:

- `RepositorioPropietario`
- `RepositorioInquilino`

Cada integrante del grupo puede utilizar su propia base de datos MySQL local. La estructura debe mantenerse igual para todos los integrantes mediante el script SQL incluido en el repositorio.

### 📄 Script SQL

El repositorio incluye el archivo:

`script.sql`

Este archivo contiene las sentencias necesarias para crear e inicializar la base de datos utilizada por el proyecto.

Para utilizarlo:

1. Abrir **DBeaver**.
2. Conectarse al servidor MySQL local.
3. Abrir el archivo `script.sql`.
4. Ejecutar las sentencias SQL.
5. Verificar que se hayan creado correctamente la base de datos y las tablas.
6. Configurar localmente la cadena de conexión correspondiente.

### 🔐 Configuración local

Las credenciales de acceso a MySQL son propias de cada integrante y no deben almacenarse en el repositorio.

De esta forma, ambos integrantes pueden trabajar con sus respectivas bases de datos locales manteniendo la misma estructura y configuración general del proyecto.

## 🚀 Instalación y ejecución del proyecto

### Requisitos previos

Para ejecutar el proyecto es necesario tener instalado:

- .NET 10 SDK
- MySQL Server
- DBeaver
- Git

### 1. Clonar el repositorio

Clonar el repositorio desde GitHub:

```bash
git clone https://github.com/AntonioAssat/AnaYAntonio-ProyectoInmobiliaria.git
```

Ingresar a la carpeta del proyecto:

```bash
cd AnaYAntonio-ProyectoInmobiliaria
```

### 2. Crear la base de datos

Abrir **DBeaver** y conectarse al servidor MySQL local.

Luego:

1. Abrir el archivo `script.sql` ubicado en la raíz del proyecto.
2. Ejecutar todas las sentencias SQL.
3. Verificar que se haya creado correctamente la base de datos.
4. Verificar que se hayan creado las tablas `Propietario` e `Inquilino`.

### 3. Configurar la conexión a la base de datos

El proyecto utiliza **User Secrets** para almacenar las credenciales de conexión de manera local.

Desde una terminal ubicada en la carpeta raíz del proyecto, ejecutar:

```bash
dotnet user-secrets init
```

Luego configurar la cadena de conexión:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=Inmobiliaria;User ID=root;Password=TU_CONTRASEÑA;"
```

Reemplazar `TU_CONTRASEÑA` por la contraseña correspondiente al usuario local de MySQL.

Para comprobar que la configuración se guardó correctamente:

```bash
dotnet user-secrets list
```

Debería aparecer:

```text
ConnectionStrings:DefaultConnection
```

> Las credenciales de acceso a la base de datos se configuran localmente y no se almacenan en el repositorio.

### 4. Restaurar las dependencias

Desde la carpeta del proyecto ejecutar:

```bash
dotnet restore
```

### 5. Compilar el proyecto

Ejecutar:

```bash
dotnet build
```

Si la compilación finaliza correctamente, el proyecto está listo para ejecutarse.

### 6. Ejecutar el proyecto

Ejecutar:

```bash
dotnet run
```

La terminal mostrará la dirección local donde se encuentra disponible la aplicación, por ejemplo:

```text
https://localhost:xxxx
```

Abrir esa dirección en un navegador web.

### 7. Probar las funcionalidades

Una vez iniciada la aplicación, se pueden probar las funcionalidades correspondientes a la primera entrega.

#### Propietarios

- Consultar el listado de propietarios.
- Dar de alta un propietario.
- Modificar un propietario.
- Dar de baja un propietario.

##  Uso de la gestión de Inquilinos

Para acceder a la gestión de inquilinos, ingresar en:

```text
/Inquilino
```

Esta es la vista principal de Inquilinos. Desde allí se puede acceder a las distintas operaciones del ABM mediante los botones disponibles en el listado.

### 📋 Listado de Inquilinos

Ruta inicial:

```text
/Inquilino
```

Esta vista muestra los inquilinos registrados en la base de datos.

Desde el listado se encuentran los botones necesarios para realizar las diferentes operaciones:

- **Nuevo Inquilino:** permite acceder al formulario de alta.
- **Editar:** permite modificar los datos de un inquilino existente.
- **Eliminar:** permite acceder a la confirmación de baja de un inquilino.

### ➕ Alta de Inquilino

Desde el listado de Inquilinos, seleccionar el botón correspondiente a **Nuevo Inquilino**.

La aplicación mostrará un formulario donde se deben completar los datos del nuevo inquilino:

- Nombre completo.
- DNI.
- Teléfono.
- Mail.
- Estado.

Al presionar **Guardar**, el inquilino se registra en la base de datos y la aplicación vuelve al listado.

También se dispone de un botón **Volver** para regresar al listado sin realizar el alta.

### ✏️ Modificación de Inquilino

Desde el listado `/Inquilino`, seleccionar el botón **Editar** correspondiente al inquilino que se desea modificar.

La aplicación mostrará el formulario con los datos actuales del inquilino cargados.

Se pueden modificar:

- Nombre completo.
- DNI.
- Teléfono.
- Mail.
- Estado.

Al presionar **Guardar cambios**, se actualizan los datos en la base de datos y se vuelve al listado.

También se dispone de un botón **Volver** para regresar al listado.

### 🗑️ Baja de Inquilino

Desde el listado `/Inquilino`, seleccionar el botón **Eliminar** correspondiente al inquilino que se desea dar de baja.

La aplicación mostrará una pantalla de confirmación con los datos del inquilino seleccionado.

Para realizar la baja, presionar el botón **Eliminar**.

Si no se desea realizar la operación, se puede seleccionar **Cancelar** para volver al listado.

Una vez confirmada la baja, el registro se elimina de la base de datos y la aplicación vuelve al listado de Inquilinos.

### 🔄 Flujo de navegación

El flujo de uso de la gestión de Inquilinos es:

```text
/Inquilino
     │
     ├── Nuevo Inquilino
     │       ↓
     │    Formulario de Alta
     │       ↓
     │    Guardar
     │       ↓
     │    /Inquilino
     │
     ├── Editar
     │       ↓
     │    Formulario de Modificación
     │       ↓
     │    Guardar cambios
     │       ↓
     │    /Inquilino
     │
     └── Eliminar
             ↓
       Confirmación de Baja
             ↓
          Eliminar
             ↓
        /Inquilino
```