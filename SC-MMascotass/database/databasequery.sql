USE tempdb
go

--Crear la base de datos
CREATE DATABASE scmascotas
GO

--Usar la base de datos
Use scmascotas
GO

--Crear el schema de la base de datos
CREATE SCHEMA Veterinaria
GO

--Crear las Tablas
CREATE TABLE Veterinaria.Mascota(
	IdMascota INT NOT NULL IDENTITY(1,1),
	IdCliente INT NOT NULL,
	AliasMascota VARCHAR(50) NOT NULL,
	Especie VARCHAR(50) NOT NULL,
	Raza VARCHAR(50) NOT NULL,
	ColorPelo VARCHAR(50) NOT NULL,
	Fecha DATETIME NOT NULL
)
GO

CREATE TABLE Veterinaria.Cliente(
	IdCliente INT NOT NULL IDENTITY(1,1),
	NombreCliente VARCHAR(50) NOT NULL,
	Telefono VARCHAR(12) NOT NULL,
)
GO

CREATE TABLE Veterinaria.HistorialVacunacion(
	IdHistorialVacunacion INT NOT NULL IDENTITY(1,1),
	IdMascota INT NOT NULL,
	IdProducto INT NOT NULL,
	Fecha DATETIME NOT NULL,
)
GO

CREATE TABLE Veterinaria.Inventario(
	IdProducto INT NOT NULL IDENTITY(1,1),
	IdCategoria INT NOT NULL,
	NombreProducto VARCHAR(50) NOT NULL,
	PrecioCosto FLOAT NOT NULL,
	PrecioVenta FLOAT NOT NULL,
	Stock INT NOT NULL,
)
GO

CREATE TABLE Veterinaria.Categoria(
	IdCategoria INT NOT NULL IDENTITY(1,1),
	NombreCategoria VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE Veterinaria.Venta(
	IdVenta INT NOT NULL IDENTITY(1,1),
	IdDetallesVentas INT NOT NULL,
	IdCliente VARCHAR(50) NOT NULL,
	Impuesto FLOAT NOT NULL,
	Total FLOAT NOT NULL,
	Fecha DATETIME NOT NULL,
)
GO

CREATE TABLE Veterinaria.DetallesVenta(
	IdDetallesVentas INT NOT NULL IDENTITY(1,1),
	IdProducto INT NOT NULL,
	Descripcion VARCHAR(50) NOT NULL,
	Cantidad INT NOT NULL,
	PrecioUnitario FLOAT NOT NULL,
	Total FLOAT NOT NULL,
)
GO


CREATE TABLE Usuarios.Usuario (
	id INT NOT NULL IDENTITY (500, 1),
	nombreCompleto VARCHAR(255) NOT NULL,
	username VARCHAR(100) NOT NULL,
	password VARCHAR(100) NOT NULL,
	estado BIT NOT NULL,
	CONSTRAINT PK_Usuario_id
		PRIMARY KEY CLUSTERED (id)
)

CREATE TABLE Veterinaria.Razas(
	IdRaza INT NOT NULL IDENTITY(1,1),
	IdEspecie INT NOT NULL,
	NombreRaza VARCHAR(50) NOT NULL,
	Altura VARCHAR(50) NOT NULL,
	RangoPeso VARCHAR(50) NOT NULL,
	EsperanzaVida INT NOT NULL,
	ActividadFisica VARCHAR(50) NOT NULL,
	TipoDePelo VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE Veterinaria.Especies(
	IdEspecie INT NOT NULL IDENTITY(1,1),
	Descripcion VARCHAR(50) NOT NULL,
	Familia VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE Veterinaria.ExpedienteAnimal(
	IdExpedienteAnimal INT NOT NULL IDENTITY(1,1),
	IdMascota INT NOT NULL,
	FechaRegistro DATE NOT NULL,
	IdUsuario INT NOT NULL,
)
GO

CREATE TABLE Veterinaria.DetalleExpedienteAnimal(
	IdDetalle INT NOT NULL IDENTITY(1,1),
	IdExpediente INT NOT NULL,
	Descripcion VARCHAR(50) NOT NULL,
	Patologia VARCHAR(50) NOT NULL,
	TratamientoRecomendado VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE Veterinaria.Personal(
	IdUsuario INT NOT NULL IDENTITY(1,1),
	Nombre VARCHAR(50) NOT NULL,
	NumeroDeIdentidad VARCHAR(50) NOT NULL,
	Telefono VARCHAR(50) NOT NULL,
	Correo VARCHAR(50) NOT NULL,
	Cargo VARCHAR(50) NOT NULL,
)
GO

-- No puede existir nombres de usuarios repetidos
ALTER TABLE Usuarios.Usuario
	ADD CONSTRAINT AK_Usuarios_Usuario_username
	UNIQUE NONCLUSTERED (username)
GO

-- La contraseña debe contener al menos 6 caracteres
ALTER TABLE Usuarios.Usuario WITH CHECK
	ADD CONSTRAINT CHK_Usuarios_Usuario$VerificarLongitudContraseña
	CHECK (LEN(password) >= 6)
GO

---Ingresa Usuario Unico
insert into Usuarios.Usuario
values ('Administrador','admin','123456',1)

select * from Usuarios.Usuario

--Ingresar Categoria de Vacunas
Insert into Veterinaria.Categoria
values ('Vacunas')
Select * from Veterinaria.Categoria


-- El numero celular debe contener al menos 8 caracteres
ALTER TABLE Veterinaria.Cliente WITH CHECK
	ADD CONSTRAINT CHK_Veterinaria_Cliente$VerificarLongitudTelefono
	CHECK (LEN(Telefono) >= 8)
GO


