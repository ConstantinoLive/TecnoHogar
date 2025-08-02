-- Eliminar base de datos (Descomentar las siguientes 3 lineas, seleccionarlas y ejecutar. Luego comentarlas y ejecutar el resto)
--USE master
--ALTER DATABASE Comercio_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE Comercio_DB;


-- Crear base de datos
CREATE DATABASE Comercio_DB;
GO

USE Comercio_DB;
GO
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(100) NOT NULL,
	Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Contrasena VARCHAR(200) NOT NULL, -- Guardar encriptada
    FechaAlta DATE NOT NULL DEFAULT GETDATE(),
    Admin bit NOT NULL, -- Ej: 'Vendedor = 0', 'Admin = 1'
	Activo BIT NOT NULL DEFAULT 1 -- Empleado inactivo por cambio de Sector/Planta, despido, etc
);
-- Tabla: Categorias (ELECTRODOMESTICOS-AUDIO-INFORMATICA-GAMING-TELEFONIA)
CREATE TABLE Categorias (
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
	Activo BIT NOT NULL DEFAULT 1 -- Categoria inactiva por no contar con productos en esa categoria
);

-- Tabla: Marcas
CREATE TABLE Marcas (
    IdMarca INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
	Activo BIT NOT NULL DEFAULT 1 -- Marca inactiva por no contar con productos de la marca
);

-- Tabla: TiposProducto (MICROONDAS, LAVARROPAS, CONSOLAS, CELULARES, NOTEBOOKS, AURICULARES, TV, PARLANTES)
CREATE TABLE TiposProducto (
    IdTipoProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    IdCategoria INT NOT NULL,
	Activo BIT NOT NULL DEFAULT 1, -- TipoProducto inactivo por no contar con productos de ese tipo
    FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria)
);


-- Tabla: Productos
CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    CodigoArticulo VARCHAR(50),
    Nombre VARCHAR(100),
    Descripcion VARCHAR(255),
    PrecioCompra DECIMAL(18,2),
    PorcentajeGanancia DECIMAL(5,2),
    StockActual INT,
    StockMinimo INT,
    ImagenUrl VARCHAR(500),
    IdMarca INT NOT NULL,
    IdTipoProducto INT NOT NULL,
	Activo BIT NOT NULL DEFAULT 1, -- Producto inactivo por falta de stock o estar discontinuado
    FOREIGN KEY (IdMarca) REFERENCES Marcas(IdMarca),
    FOREIGN KEY (IdTipoProducto) REFERENCES TiposProducto(IdTipoProducto)
);



-- Tabla: Proveedores
CREATE TABLE Proveedores (
    IdProveedor INT PRIMARY KEY IDENTITY(1,1),
    RazonSocial VARCHAR(150) NOT NULL,
	CUIT VARCHAR(20) NOT NULL,
	Direccion VARCHAR(50),
    Telefono VARCHAR(20),
    Email VARCHAR(100),
	Activo BIT NOT NULL DEFAULT 1 -- Proveedor inactivo que por X motivo se corta la relacion comercial
);

-- Tabla: ProductoProveedor (relación N a N)
CREATE TABLE ProductoProveedor (
    IdProducto INT NOT NULL,
    IdProveedor INT NOT NULL,
    PRIMARY KEY (IdProducto, IdProveedor),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
    FOREIGN KEY (IdProveedor) REFERENCES Proveedores(IdProveedor)
);

-- Tabla: Compras
CREATE TABLE Compras (
    IdCompra INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    IdProveedor INT NOT NULL,
    IdUsuario INT NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (IdProveedor) REFERENCES Proveedores(IdProveedor)
);

-- Tabla: CompraDetalle
CREATE TABLE CompraDetalle (
    IdCompraDetalle INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdCompra INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnit DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (IdCompra) REFERENCES Compras(IdCompra),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);

-- Tabla: Clientes
CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
	Apellido VARCHAR(100) NOT NULL,
	Dni VARCHAR(10) NOT NULL,
    Telefono VARCHAR(20),
    Email VARCHAR(100),
    Direccion VARCHAR(150),
	Activo BIT NOT NULL DEFAULT 1 -- Cliente inactivo por X motivo ya no se le vende.
);

-- Tabla: Ventas
CREATE TABLE Ventas (
    IdVenta INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    IdCliente INT NOT NULL,
	IdUsuario INT NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- Tabla: VentaDetalle
CREATE TABLE VentaDetalle (
    IdVentaDetalle INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnit DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (IdVenta) REFERENCES Ventas(IdVenta),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);