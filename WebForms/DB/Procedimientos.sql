    USE Comercio_DB;
GO

/*PRODUCTOS*/

CREATE OR ALTER PROCEDURE SP_ListarProductos
AS
BEGIN
    SELECT 
        P.CodigoArticulo, 
        P.Nombre, 
        P.Descripcion, 
        P.PrecioCompra, 
        CAST(P.PrecioCompra * (P.PorcentajeGanancia / 100 + 1) AS DECIMAL(10,2)) AS PrecioVenta,
        P.PorcentajeGanancia, 
        P.StockActual, 
        P.StockMinimo, 
        P.ImagenUrl, 
        P.IdMarca,
        M.Nombre AS Marca, 
        TP.IdTipoProducto,
        TP.Nombre AS NombreTP,
        C.IdCategoria,
        C.Nombre AS Categoria
    FROM Productos P
    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
    --WHERE P.Activo = 1
    ORDER BY C.Nombre ASC, P.Nombre ASC;
END;
GO

CREATE OR ALTER PROCEDURE SP_ListarProductosStockBajo
AS
BEGIN
    SELECT 
        p.IdProducto,
        p.CodigoArticulo,
        p.Nombre AS Producto,
        p.Descripcion,
        p.StockActual,
        p.StockMinimo,
        m.Nombre AS Marca,
        tp.Nombre AS TipoProducto
    FROM Productos p
    INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
    INNER JOIN TiposProducto tp ON p.IdTipoProducto = tp.IdTipoProducto
    INNER JOIN Categorias c ON tp.IdCategoria = c.IdCategoria
    WHERE 
        p.StockActual < p.StockMinimo
        AND p.Activo = 1
    ORDER BY p.Nombre ASC;
END;

go

/*VENTAS*/

CREATE OR ALTER PROCEDURE SP_ListarVentas
AS
BEGIN
    SELECT 
        V.IdVenta, 
        V.Fecha,
	    SUM(VD.Cantidad * VD.PrecioUnit) AS Total,
        C.IdCliente,
        C.Nombre,
        C.Apellido,
        C.Dni,
        C.Telefono,
        C.Email,
        C.Direccion,
        U.IdUsuario,
        U.Nombre AS NombreUsuario,
	    U.Apellido AS ApellidoUsuario,
	    U.Email AS EmailUsuario,
	    U.FechaAlta,
	    U.Admin,	
        COUNT(VD.IdVentaDetalle) AS CantidadProductos
    FROM Ventas V
    INNER JOIN Clientes C ON C.IdCliente = V.IdCliente
    INNER JOIN Usuario U ON V.IdUsuario = U.IdUsuario
    INNER JOIN VentaDetalle VD ON V.IdVenta = VD.IdVenta
    GROUP BY 
        V.IdVenta, V.Fecha,
	    C.IdCliente, C.Nombre, C.Apellido, C.Dni, C.Telefono, C.Email, C.Direccion,
        U.IdUsuario, U.Nombre, U.Apellido, U.Email, U.FechaAlta, U.Admin
    --WHERE Activo = 1;
END;
GO

CREATE OR ALTER PROCEDURE SP_ListarDetalleVenta
    @idVenta INT
AS
BEGIN
    SELECT 
        VD.IdVentaDetalle,
        VD.IdVenta,
        VD.Cantidad,
        VD.PrecioUnit,
        VD.IdProducto,
        P.CodigoArticulo,
        P.Nombre AS NombreProducto,
        P.Descripcion, 
        P.PrecioCompra,
        P.PorcentajeGanancia, 
        P.StockActual, 
        P.StockMinimo, 
        P.ImagenUrl, 
        P.IdMarca,
        M.Nombre AS Marca, 
        TP.IdTipoProducto,
        TP.Nombre AS NombreTP,
        C.IdCategoria,
        C.Nombre AS Categoria
    FROM VentaDetalle VD
    INNER JOIN Productos P ON VD.IdProducto = P.IdProducto
    INNER JOIN Marcas M ON P.IdMarca = M.IdMarca
    INNER JOIN TiposProducto TP ON P.IdTipoProducto = TP.IdTipoProducto
    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
    WHERE VD.IdVenta = @idVenta;
END;


	--- INSERTAR VENTA COMPLETA CON DETALLES Y TODO
GO
CREATE TYPE dbo.VentaDetalleType AS TABLE -- TABLA PARA ALOJAR DATOS DE LOS VentaDetalle PARA DESPUES INSERTARLOS EN "SP_InsertarVentaCompleta"
(
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnit DECIMAL(18,2) NOT NULL
);

GO
CREATE OR ALTER PROCEDURE SP_InsertarVentaCompleta 
    @IdCliente INT,
    @IdUsuario INT,
    @Detalles dbo.VentaDetalleType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- INSERTO LA VENTA
        INSERT INTO Ventas (IdCliente, IdUsuario, Total)
        VALUES (
            @IdCliente,
            @IdUsuario,
            (SELECT SUM(Cantidad * PrecioUnit) FROM @Detalles)
        );

        -- OBTENGO IdVenta GENERADO
        DECLARE @IdVenta INT = SCOPE_IDENTITY();

        -- INSERTO DETALLES
        INSERT INTO VentaDetalle (IdVenta, IdProducto, Cantidad, PrecioUnit)
        SELECT
            @IdVenta,
            IdProducto,
            Cantidad,
            PrecioUnit
        FROM @Detalles;

        -- ACTUALIZO STOCK EN CADA PRODUCTO
        UPDATE p
        SET p.StockActual = p.StockActual - d.Cantidad
        FROM Productos p
        INNER JOIN @Detalles d ON p.IdProducto = d.IdProducto;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

        -- Re-lanzar el error
        THROW;
    END CATCH
END

--- FIN DE INSERT DE VENTA COMPLETA CON DETALLES Y TODO




/*CLIENTES*/
GO
CREATE OR ALTER PROCEDURE SP_ListarClientes
AS
BEGIN
    SELECT 
        IdCliente, 
        Nombre, 
        Apellido, 
        Dni, 
        Telefono, 
        Email, 
        Direccion,
        Activo
    FROM Clientes
    WHERE Activo = 1
	order by Apellido, nombre Asc
END;
GO

CREATE OR ALTER PROCEDURE SP_ListarClientesEliminados
AS
BEGIN
    SELECT 
        IdCliente, 
        Nombre, 
        Apellido, 
        Dni, 
        Telefono, 
        Email, 
        Direccion,
        Activo
    FROM Clientes
    WHERE Activo = 0
	order by Apellido, nombre Asc
END;
GO

CREATE OR ALTER PROCEDURE SP_AgregarCliente
    @Nombre varchar(100),
    @Apellido varchar(100),
    @Dni varchar(10),
    @Telefono varchar(20),
    @Email varchar(100),
    @Direccion varchar(150),
    @Activo bit = 1 
AS
BEGIN
    INSERT INTO Clientes (Nombre, Apellido, Dni, Telefono, Email, Direccion, Activo)
    VALUES (@Nombre, @Apellido, @Dni, @Telefono, @Email, @Direccion, @Activo);
END

go

create or alter procedure SP_ModificarCliente
@Nombre varchar(100),
@Apellido varchar(100),
@Dni varchar(10),
@Telefono varchar(150),
@Email varchar(100),
@Direccion varchar(150),
@IdCliente int
as
BEGIN
update clientes 
	set Nombre = @Nombre, 
	Apellido = @Apellido, 
	Direccion =@Direccion, 
	Email = @Email, 
	Dni = @Dni, 
	Telefono = @Telefono 
where IdCliente = @IdCliente
END

go

create or alter procedure SP_EliminarCliente
@IdCliente int
AS
BEGIN
update Clientes set Activo = 0 where IdCliente = @IdCliente
END

go

create or alter Procedure SP_ReactivarCliente
@Id int
as
Begin
update Clientes set Activo = 1 where IdCliente = @Id
END

go

/*PROVEEDORES*/

create or Alter procedure SP_listarProveedores
as
Begin
select IdProveedor, RazonSocial, CUIT, Direccion,Telefono, Email, Activo from Proveedores where activo = 1 order by RazonSocial asc
end

go

create or alter procedure SP_AgregarProveedor
@RazonSocial varchar(150),
@Cuit varchar(20),
@Direccion varchar(50),
@Telefono varchar(20),
@Email varchar(100),
@Activo bit = 1
as
begin
insert into Proveedores (RazonSocial, CUIT, Direccion, Telefono, Email, Activo) values (@RazonSocial, @Cuit, @Direccion, @Telefono, @Email, 1)
end

go

create or alter procedure SP_ModificarProveedor
@RazonSocial varchar(150),
@Cuit varchar(20),
@Direccion varchar(50),
@Telefono varchar(20),
@Email varchar(100),
@IdProveedor int
as
begin
update Proveedores set RazonSocial = @RazonSocial, CUIT = @Cuit, Direccion = @Direccion, Telefono = @Telefono, Email = @Email where IdProveedor = @IdProveedor
end

go

create or alter procedure SP_EliminarProveedor
@IdProveedor int
as
begin
update Proveedores set Activo = 0 where IdProveedor = @IdProveedor
end

go

CREATE OR ALTER PROCEDURE SP_ListarProveedoresEliminados
as
begin
select * from proveedores where Activo = 0
end

go

create or alter procedure SP_ReactivarProveedor
@IdProveedor int
AS
BEGIN
update Proveedores set activo = 1 where IdProveedor = @IdProveedor
END

go

CREATE OR ALTER PROCEDURE SP_ListarUsuariosEliminados
AS
BEGIN
Select * from Usuario where Activo = 0 ORDER BY NombreUsuario, apellido, nombre asc
END
go
CREATE OR ALTER PROCEDURE SP_ReactivarUsuario
@IdUsuario int
AS
BEGIN
update Usuario set Activo = 1 where IdUsuario = @IdUsuario
END

/*USUARIOS*/
go
/*
create or alter procedure SP_AgregarUsuario
@NombreUsuario varchar(100),
@Nombre varchar(100),
@Apellido varchar(100),
@Email varchar(150),
@Contraseña varchar(50),
@FechaAlta date,
@Activo bit = 1,
@Admin bit
as
begin
insert into Usuario values(@NombreUsuario,@Nombre,@Apellido,@Email,@Contraseña, @FechaAlta, @Admin, @Activo)
end

GO

create or alter procedure SP_ModificarUsuario
@IdUsuario int,
@NombreUsuario varchar(100),
@Nombre varchar(100),
@Apellido varchar(100),
@Email varchar(150),
@Contraseña varchar(200),
@FechaAlta date,
@Admin bit
as
begin
update Usuario set NombreUsuario = @NombreUsuario, Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Contrasena = @Contraseña, FechaAlta = @FechaAlta, Admin = @Admin where IdUsuario = @IdUsuario
end

go
*/
-- ============================================
-- SP para Ingresar Usuario (con Comisiones)
-- ============================================

ALTER PROCEDURE SP_AgregarUsuario
    @NombreUsuario VARCHAR(100),
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Email VARCHAR(150),
    @Contraseña VARCHAR(50),
    @FechaAlta DATE,
    @Activo BIT = 1,
    @Admin BIT,
    @PorcentajeComision DECIMAL(5,2) -- Nuevo parámetro
AS
BEGIN
    INSERT INTO Usuario
        (NombreUsuario, Nombre, Apellido, Email, Contrasena, FechaAlta, Admin, Activo, PorcentajeComision)
    VALUES
        (@NombreUsuario, @Nombre, @Apellido, @Email, @Contraseña, @FechaAlta, @Admin, @Activo, @PorcentajeComision);
END
GO

-- ============================================
-- SP para Modificar Usuario (con Comisiones)
-- ============================================
ALTER PROCEDURE SP_ModificarUsuario
    @IdUsuario INT,
    @NombreUsuario VARCHAR(100),
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Email VARCHAR(150),
    @Contraseña VARCHAR(200),
    @FechaAlta DATE,
    @Admin BIT,
    @PorcentajeComision DECIMAL(5,2) -- Nuevo parámetro
AS
BEGIN
    UPDATE Usuario
    SET
        NombreUsuario = @NombreUsuario,
        Nombre = @Nombre,
        Apellido = @Apellido,
        Email = @Email,
        Contrasena = @Contraseña,
        FechaAlta = @FechaAlta,
        Admin = @Admin,
        PorcentajeComision = @PorcentajeComision
    WHERE IdUsuario = @IdUsuario;
END
GO

create or alter procedure SP_EliminarUsuario
@IdUsuario int
as
begin
update Usuario set Activo = 0 where IdUsuario = @IdUsuario
end

GO

CREATE or ALTER PROCEDURE SP_Loguear
@NombreUsuario varchar(100),
@Contrasena varchar(200)
AS
BEGIN
	select * from Usuario where NombreUsuario = @NombreUsuario and Contrasena = @Contrasena and activo = 1
END

go

/*Compras*/

create or alter procedure SP_HistorialPreciosProducto
    @IdProducto INT
AS
BEGIN
    SELECT 
        P.RazonSocial AS RazonSocial,
        C.Fecha AS FechaCompra,
        CD.PrecioUnit AS PrecioUnitario
    FROM CompraDetalle CD
    INNER JOIN Compras C ON CD.IdCompra = C.IdCompra
    INNER JOIN Proveedores P ON C.IdProveedor = P.IdProveedor
    WHERE CD.IdProducto = @IdProducto
    ORDER BY C.Fecha DESC;
END;

--- INSERTAR COMPRA COMPLETA CON DETALLES Y TODO
GO
CREATE TYPE dbo.CompraDetalleType AS TABLE -- TABLA PARA ALOJAR DATOS DE LOS CompraDetalle PARA DESPUES INSERTARLOS EN "SP_InsertarCompraCompleta"
(
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnit DECIMAL(18,2) NOT NULL
);

GO
CREATE OR ALTER PROCEDURE SP_InsertarCompraCompleta 
    @IdProveedor INT,
    @IdUsuario INT,
    @Detalles dbo.CompraDetalleType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- INSERTO LA COMPRA
        INSERT INTO Compras (IdProveedor, IdUsuario, Total)
        VALUES (
            @IdProveedor,
            @IdUsuario,
            (SELECT SUM(Cantidad * PrecioUnit) FROM @Detalles)
        );

        -- OBTENGO IdCompra GENERADO
        DECLARE @IdCompra INT = SCOPE_IDENTITY();

        -- INSERTO DETALLES
        INSERT INTO CompraDetalle (IdCompra, IdProducto, Cantidad, PrecioUnit)
        SELECT
            @IdCompra,
            IdProducto,
            Cantidad,
            PrecioUnit
        FROM @Detalles;

        -- ACTUALIZO STOCK EN CADA PRODUCTO
        UPDATE p
        SET p.StockActual = p.StockActual + d.Cantidad
        FROM Productos p
        INNER JOIN @Detalles d ON p.IdProducto = d.IdProducto;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

        -- Re-lanzar el error
        THROW;
    END CATCH
END

--- FIN DE INSERT DE COMPRA COMPLETA CON DETALLES Y TODO


GO
/*Marcas*/

CREATE OR ALTER PROCEDURE SP_ListarMarca
AS
BEGIN
    SELECT IdMarca, Nombre, Activo
    FROM Marcas
    WHERE Activo = 1
    ORDER BY Nombre;
END;

GO

CREATE OR ALTER PROCEDURE SP_ListarMarcaEliminadas
AS
BEGIN
    SELECT IdMarca, Nombre, Activo
    FROM Marcas
    WHERE Activo = 0
    ORDER BY Nombre;
END;

GO

CREATE OR ALTER PROCEDURE SP_AgregarMarca
    @Nombre VARCHAR(100)
AS
BEGIN
    INSERT INTO Marcas (Nombre, Activo)
    VALUES (@Nombre, 1);
END;


GO

CREATE OR ALTER PROCEDURE SP_ModificarMarca
    @IdMarca INT,
    @Nombre VARCHAR(100)
AS
BEGIN
    UPDATE Marcas
    SET Nombre = @Nombre
    WHERE IdMarca = @IdMarca;
END;

GO

CREATE OR ALTER PROCEDURE SP_EliminarMarca
    @IdMarca INT
AS
BEGIN
    UPDATE Marcas
    SET Activo = 0
    WHERE IdMarca = @IdMarca;
END;

GO

CREATE OR ALTER PROCEDURE SP_AltaMarca
    @IdMarca INT
AS
BEGIN
    UPDATE Marcas
    SET Activo = 1
    WHERE IdMarca = @IdMarca;
END;

GO

/*Categoria*/

CREATE OR ALTER PROCEDURE SP_ListarCategoria
AS
BEGIN
    SELECT IdCategoria, Nombre, Activo
    FROM Categorias
    WHERE Activo = 1
    ORDER BY Nombre;
END;

GO

CREATE OR ALTER PROCEDURE SP_AgregarCategoria
    @Nombre VARCHAR(100)
AS
BEGIN
    INSERT INTO Categorias (Nombre)
    VALUES (@Nombre);
END;

GO

CREATE OR ALTER PROCEDURE SP_ModificarCategoria
    @IdCategoria INT,
    @Nombre VARCHAR(100)
AS
BEGIN
    UPDATE Categorias
    SET Nombre = @Nombre
    WHERE IdCategoria = @IdCategoria;
END;

GO

CREATE OR ALTER PROCEDURE SP_EliminarCategoria
    @IdCategoria INT
AS
BEGIN
    UPDATE Categorias
    SET Activo = 0
    WHERE IdCategoria = @IdCategoria;
END;

GO

CREATE OR ALTER PROCEDURE SP_ListarCategoriaEliminada
AS
BEGIN
    SELECT IdCategoria, Nombre, Activo
    FROM Categorias
    WHERE Activo = 0
    ORDER BY Nombre;
END;

GO

CREATE OR ALTER PROCEDURE SP_AltaCategoria
    @IdCategoria INT
AS
BEGIN
    UPDATE Categorias
    SET Activo = 1
    WHERE IdCategoria = @IdCategoria;
END;

GO

/*Tipo Producto*/

CREATE OR ALTER PROCEDURE SP_ListarTiposProducto
AS
BEGIN
    SELECT 
        TP.IdTipoProducto,
        TP.Nombre,
        TP.IdCategoria,
        C.Nombre AS NombreCategoria,
        TP.Activo
    FROM TiposProducto TP
    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
    WHERE TP.Activo = 1
END

GO

CREATE OR ALTER PROCEDURE SP_AgregarTipoProducto
    @Nombre VARCHAR(100),
    @IdCategoria INT
AS
BEGIN
    INSERT INTO TiposProducto (Nombre, IdCategoria)
    VALUES (@Nombre, @IdCategoria)
END

GO

CREATE OR ALTER PROCEDURE SP_ModificarTipoProducto
    @IdTipoProducto INT,
    @Nombre VARCHAR(100),
    @IdCategoria INT
AS
BEGIN
    UPDATE TiposProducto
    SET 
        Nombre = @Nombre,
        IdCategoria = @IdCategoria
    WHERE IdTipoProducto = @IdTipoProducto
END

GO

CREATE OR ALTER PROCEDURE SP_EliminarTipoProducto
    @IdTipoProducto INT
AS
BEGIN
    UPDATE TiposProducto
    SET Activo = 0
    WHERE IdTipoProducto = @IdTipoProducto
END

GO

CREATE OR ALTER PROCEDURE SP_AltaTipoProducto
    @IdTipoProducto INT
AS
BEGIN
    UPDATE TiposProducto
    SET Activo = 1
    WHERE IdTipoProducto = @IdTipoProducto
END

GO

CREATE OR ALTER PROCEDURE SP_ListarTiposProductoEliminados
AS
BEGIN
    SELECT 
        TP.IdTipoProducto,
        TP.Nombre,
        TP.IdCategoria,
        C.Nombre AS NombreCategoria,
        TP.Activo
    FROM TiposProducto TP
    INNER JOIN Categorias C ON TP.IdCategoria = C.IdCategoria
    WHERE TP.Activo = 0
END