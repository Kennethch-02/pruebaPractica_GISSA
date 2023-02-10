IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'appWeb_BD')
BEGIN
    CREATE DATABASE appWeb_BD;
END;
GO

-- Usar la base de datos appWeb_BD
USE appWeb_BD;
GO

-- Crear la tabla test_usuarios
CREATE TABLE test_usuarios (
  id INT IDENTITY(1,1) PRIMARY KEY,
  tipo_usuario VARCHAR(50) NOT NULL,
  tipo_identificacion VARCHAR(50) NOT NULL,
  num_identificacion VARCHAR(50) NOT NULL,
  nombre_completo VARCHAR(255) NOT NULL,
  nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
  clave VARCHAR(500) NOT NULL,
  correo_electronico VARCHAR(255) NOT NULL UNIQUE
);
GO

-- Crear la tabla test_telefonos
CREATE TABLE test_telefonos (
  id INT IDENTITY(1,1) PRIMARY KEY,
  telefono VARCHAR(50) NOT NULL,
  usuario_id INT NOT NULL,
  FOREIGN KEY (usuario_id) REFERENCES test_usuarios(id)
);
GO

-- Crear la tabla test_habilidades_blandas
CREATE TABLE test_habilidades_blandas (
  id INT IDENTITY(1,1) PRIMARY KEY,
  habilidad VARCHAR(255) NOT NULL,
  usuario_id INT NOT NULL,
  FOREIGN KEY (usuario_id) REFERENCES test_usuarios(id)
);
GO

-- Funcion para separar String segun un caracter
CREATE FUNCTION SplitString
(    
      @Input NVARCHAR(MAX),
      @Character CHAR(1)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
      SET @StartIndex = 1
      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
          SET @Input = @Input + @Character
      END
      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
          SET @EndIndex = CHARINDEX(@Character, @Input)

          INSERT INTO @Output(Item)
          SELECT SUBSTRING(@Input, @StartIndex, @EndIndex - 1)

          SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END
      RETURN
END
GO

-- Procedimiento para agregar usuario
CREATE PROCEDURE test_agregarUsuario (
@tipo_usuario varchar(50), 
@tipo_identificacion varchar(50), 
@numero_identificacion varchar(50), 
@nombre_completo varchar(100), 
@nombre_usuario varchar(50), 
@clave varchar(500), 
@correo_electronico varchar(100), 
@telefonos nvarchar(255), 
@habilidades_blandas nvarchar(255))
AS
BEGIN
  DECLARE @usuario_id INT;

  INSERT INTO test_usuarios (tipo_usuario, tipo_identificacion, num_identificacion, nombre_completo, nombre_usuario, clave, correo_electronico)
  VALUES (@tipo_usuario, @tipo_identificacion, @numero_identificacion, @nombre_completo, @nombre_usuario, @clave, @correo_electronico);

  SET @usuario_id = SCOPE_IDENTITY();

  DECLARE @telefonos_split TABLE (id INT IDENTITY(1,1), valor nvarchar(255));
  DECLARE @habilidades_split TABLE (id INT IDENTITY(1,1), valor nvarchar(255));
  DECLARE @telefono nvarchar(255);
  DECLARE @habilidad nvarchar(255);

  INSERT INTO @telefonos_split (valor) SELECT * FROM SplitString(@telefonos, ',');
  INSERT INTO @habilidades_split (valor) SELECT * FROM SplitString(@habilidades_blandas, ',');

  WHILE EXISTS (SELECT 1 FROM @telefonos_split)
  BEGIN
    SELECT TOP 1 @telefono = valor FROM @telefonos_split;
    INSERT INTO test_telefonos (telefono, usuario_id) VALUES (@telefono, @usuario_id);
    DELETE FROM @telefonos_split WHERE valor = @telefono;
  END

  WHILE EXISTS (SELECT 1 FROM @habilidades_split)
  BEGIN
    SELECT TOP 1 @habilidad = valor FROM @habilidades_split;
    INSERT INTO test_habilidades_blandas (habilidad, usuario_id) VALUES (@habilidad, @usuario_id);
    DELETE FROM @habilidades_split WHERE valor = @habilidad;
  END
END
GO

-- Procedimiento para verificar el Inicio de sesion
CREATE PROCEDURE test_verificarInicioSesion (
	@correo varchar(100),
	@clave varchar(500), 
	@resultado bit OUTPUT, 
	@tipo_usuario varchar(50) OUTPUT)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id INT;

    SELECT @id = id FROM test_usuarios 
    WHERE correo_electronico = @correo AND clave = @clave;

    IF (@id IS NULL)
        SET @resultado = 0;
    ELSE
    BEGIN
        SET @resultado = 1;
        SELECT @tipo_usuario = tipo_usuario FROM test_usuarios 
        WHERE id = @id;
    END;
END;
GO
--