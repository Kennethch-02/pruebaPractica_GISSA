IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'appWeb_BD')
BEGIN
    CREATE DATABASE appWeb_BD;
END;
GO

USE appWeb_BD;
GO

/*Creacion de las tablas*/
CREATE TABLE test_TipoUsuario (
  TipoUsuarioID int IDENTITY(1,1) PRIMARY KEY,
  NombreTipoUsuario varchar(50) NOT NULL
);
INSERT INTO test_TipoUsuario (NombreTipoUsuario)
VALUES  ('Administrador'), 
		('Consultor');

CREATE TABLE test_TipoIdentificacion (
  TipoIdentificacionID int IDENTITY(1,1) PRIMARY KEY,
  NombreTipoIdentificacion varchar(50) NOT NULL
);
INSERT INTO test_TipoIdentificacion (NombreTipoIdentificacion)
VALUES  ('Nacional'), 
		('Extrangero');

CREATE TABLE test_HabilidadesBlandas (
  HabilidadBlandaID int IDENTITY(1,1) PRIMARY KEY,
  NombreHabilidadBlanda varchar(50) NOT NULL
);
INSERT INTO test_HabilidadesBlandas (NombreHabilidadBlanda)
VALUES  ('Buena comunicación'), 
		('Buena organización'), 
		('Trabajo en equipo'), 
		('Puntualidad'), 
		('Ser creativo'), 
		('Facilidad de adaptación');

CREATE TABLE test_Usuarios (
  UsuarioID int IDENTITY(1,1) PRIMARY KEY,
  TipoUsuarioID int NOT NULL FOREIGN KEY REFERENCES test_TipoUsuario(TipoUsuarioID),
  TipoIdentificacionID int NOT NULL FOREIGN KEY REFERENCES test_TipoIdentificacion(TipoIdentificacionID),
  NumeroIdentificacion varchar(50) NOT NULL,
  NombreCompleto varchar(100) NOT NULL,
  NombreUsuario varchar(50) NOT NULL,
  Clave varchar(50) NOT NULL,
  CorreoElectronico varchar(100) NOT NULL,
  Telefono varchar(50) NOT NULL,
  HabilidadBlandaID int NOT NULL FOREIGN KEY REFERENCES test_HabilidadesBlandas(HabilidadBlandaID)
);
GO

/* Creacion de los procedimientos almacenados */
CREATE PROCEDURE test_CrearUsuario (@TipoUsuarioID int, @TipoIdentificacionID int, @NumeroIdentificacion varchar(50), @NombreCompleto varchar(100), @NombreUsuario varchar(50), @Clave varchar(50), @CorreoElectronico varchar(100), @Telefono varchar(50), @HabilidadBlandaID int)
AS
BEGIN
  INSERT INTO test_Usuarios (TipoUsuarioID, TipoIdentificacionID, NumeroIdentificacion, NombreCompleto, NombreUsuario, Clave, CorreoElectronico, Telefono, HabilidadBlandaID)
  VALUES (@TipoUsuarioID, @TipoIdentificacionID, @NumeroIdentificacion, @NombreCompleto, @NombreUsuario, @Clave, @CorreoElectronico, @Telefono, @HabilidadBlandaID)
END;
GO

CREATE PROCEDURE test_LeerUsuario (@id INT)
AS
BEGIN
  SELECT * FROM test_Usuarios WHERE UsuarioID = @id
END
GO

CREATE PROCEDURE test_ActualizarUsuario (@id INT, @tipoUsuario VARCHAR(20), @tipoIdentificacion VARCHAR(20), @numIdentificacion VARCHAR(9),
                                        @nombreCompleto VARCHAR(100), @username VARCHAR(50), @clave VARCHAR(50), @correoElectronico VARCHAR(100), @telefono VARCHAR(8))
AS
BEGIN
  UPDATE test_Usuarios SET 
	  TipoUsuarioID = @tipoUsuario, 
	  TipoIdentificacionID = @tipoIdentificacion, 
	  NumeroIdentificacion = @numIdentificacion,
	  nombreCompleto = @nombreCompleto, 
	  NombreUsuario = @username, 
	  clave = @clave, 
	  correoElectronico = @correoElectronico, 
	  telefono = @telefono
  WHERE UsuarioID = @id
END

GO
CREATE PROCEDURE test_EliminarUsuario (@id INT)
AS
BEGIN
  DELETE FROM test_Usuarios WHERE UsuarioID = @id
END
GO

CREATE PROCEDURE test_ListarUsuarios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM test_Usuarios
END
GO

CREATE PROCEDURE test_ListarHabilidades
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM test_HabilidadesBlandas
END
GO

CREATE PROCEDURE test_ListarTipoIdentificacion
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM test_TipoIdentificacion
END
GO

CREATE PROCEDURE test_ListarTipoUsuario
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM test_TipoUsuario
END
GO