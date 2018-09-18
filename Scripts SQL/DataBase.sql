Create Database Boneage;
use Boneage;

--drop Database Bonage
Create table Expediente (
	ID int IDENTITY(1,1),
	Cedula int,
	Nombre varchar(15),
	Apellidos varchar(25), 
	FechaNacimiento date
	Primary key (ID));

Create table TipoDeUsuario (
	ID int IDENTITY(1,1),
	Nombre varchar(15)
	Primary key (ID));

Create table Usuario (
	ID int IDENTITY(1,1),
	Cedula int,
	Nombre varchar(15),
	Apellidos varchar(25), 
	TipoUsuario int,
	Primary key (ID),
	Foreign key (TipoUsuario) references TipoDeUsuario(ID));

Create table Diagnostico (
	ID int IDENTITY(1,1),
	IDExpediente int,
	EdadCalculada int,  --Edad del diagnostico
	Doctor int,
	Fecha date,
	Primary key (ID),
	Foreign key (IDExpediente) references Expediente(ID),
	Foreign key (Doctor) references Usuario(ID));

-------Expediente
GO
CREATE PROC Insertar_Expediente
    @Cedula int,
	@Nombre varchar(15),
	@Apellidos varchar(25),
	@FechaNacimiento date
AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Expediente]
           ([Cedula]
           ,[Nombre]
           ,[Apellidos]
           ,[FechaNacimiento])
     VALUES
           (@Cedula,
			@Nombre,
			@Apellidos,
			@FechaNacimiento)
GO
/*
EXECUTE [dbo].Insertar_Expediente 
   @Cedula =702330809,
	@Nombre = 'Alonso',
	@Apellidos = 'Rivas Solano',
	@FechaNacimiento = '2018-06-04'
GO
*/

GO
Create Procedure Eliminar_Expediente @Cedula    INT 

AS
BEGIN

SET NOCOUNT ON 
DELETE FROM [dbo].[Expediente] WHERE Cedula = @Cedula;
END
GO

/*
EXECUTE [dbo].Eliminar_Expediente 
   @Cedula =702330809
GO
*/

CREATE PROCEDURE Seleccionar_Expediente 
WITH EXECUTE AS CALLER  
AS  
    SET NOCOUNT ON;  
    SELECT Cedula as 'Cédula', 
		   Nombre, 
		   Apellidos, 
		   convert(varchar, FechaNacimiento, 120) as 'Fecha de nacimiento (AAAA-DD-MM)' 
    FROM Expediente E    
    ORDER BY E.Nombre ASC;  
GO  



/*
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Actualizar_Expediente]
		@Cedula = 702330809,
		@Nombre = 'Alfonsos',
		@Apellidos = 'Rivas Solano',
		@FechaNacimiento = '1995-02-18'

SELECT	'Return Value' = @return_value
GO
*/
Create PROCEDURE Actualizar_Expediente
    @Cedula int = NULL,
	@Nombre varchar(15) = NULL,
	@Apellidos varchar(25)= NULL,
	@FechaNacimiento date= NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Expediente
    SET Apellidos=ISNULL(@Apellidos,Apellidos), 
        Nombre=ISNULL(@Nombre,Nombre), 
        Cedula=ISNULL(@Cedula, Cedula), 
        FechaNacimiento=ISNULL(@FechaNacimiento, FechaNacimiento)
    WHERE Cedula=@Cedula AND (@Apellidos IS NOT NULL OR
                  @Nombre IS NOT NULL OR
                  @Cedula IS NOT NULL OR
				  @FechaNacimiento IS NOT NULL)
END 

-------Diagnostico



GO
CREATE PROC Insertar_Diagnostico
    @IDExpediente int,
	@EdadCalculada int,
	@Doctor int,
	@Fecha date
AS 

SET NOCOUNT ON

INSERT INTO [dbo].Diagnostico
           (IDExpediente
           ,EdadCalculada
           ,Doctor
           ,Fecha)
     VALUES
           (@IDExpediente,
			@EdadCalculada,
			@Doctor,
			@Fecha)
GO


GO
Create Procedure Eliminar_Diagnostico @ID    INT 

AS
BEGIN

SET NOCOUNT ON 
DELETE FROM [dbo].Diagnostico WHERE ID = @ID;
END
GO

/*
EXECUTE [dbo].Eliminar_Expediente 
   @Cedula =702330809
GO
*/

CREATE PROCEDURE Seleccionar_Diagnostico
WITH EXECUTE AS CALLER  
AS  
    SET NOCOUNT ON;  
    SELECT d.IDExpediente 'ID Expediente',   
      d.EdadCalculada AS 'Edad Calculada',   
      d.Doctor  'Doctor' , convert(varchar, d.Fecha , 120) AS 'Fecha'
    FROM Diagnostico D 
    ORDER BY D.Fecha ASC;  
GO  



/*
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Actualizar_Expediente]
		@Cedula = 702330809,
		@Nombre = 'Alfonsos',
		@Apellidos = 'Rivas Solano',
		@FechaNacimiento = '1995-02-18'

SELECT	'Return Value' = @return_value
GO
*/
Create PROCEDURE Actualizar_EdadCalculada
    @ID int = NULL,
	@IDExpediente int = NULL,
	@EdadCalculada int = NULL,
	@Doctor int = NULL,
	@Fecha date = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Diagnostico
    SET IDExpediente=ISNULL(@IDExpediente,IDExpediente), 
        EdadCalculada=ISNULL(@EdadCalculada,EdadCalculada), 
        Doctor=ISNULL(@Doctor, Doctor), 
        Fecha=ISNULL(@Fecha, Fecha)
    WHERE ID=@ID AND (@IDExpediente IS NOT NULL OR
                  @EdadCalculada IS NOT NULL OR
                  @Doctor IS NOT NULL OR
				  @Fecha IS NOT NULL)
END 




-------TipoDeUsuario



GO
CREATE PROC Insertar_TipoDeUsuario
    @Nombre varchar(15)
AS 

SET NOCOUNT ON

INSERT INTO [dbo].TipoDeUsuario
           (Nombre
           )
     VALUES
           (@Nombre)
GO


GO
Create Procedure Eliminar_TipoDeUsuario @Nombre    varchar(15) 

AS
BEGIN

SET NOCOUNT ON 
DELETE FROM [dbo].TipoDeUsuario WHERE Nombre = @Nombre;
END
GO

/*
EXECUTE [dbo].Eliminar_Expediente 
   @Cedula =702330809
GO
*/

CREATE PROCEDURE Seleccionar_TipoDeUsuario
WITH EXECUTE AS CALLER  
AS  
    SET NOCOUNT ON;  
    SELECT T.ID ID, T.Nombre 
    FROM TipoDeUsuario T
    ORDER BY T.Nombre ASC;  
GO  



/*
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Actualizar_Expediente]
		@Cedula = 702330809,
		@Nombre = 'Alfonsos',
		@Apellidos = 'Rivas Solano',
		@FechaNacimiento = '1995-02-18'

SELECT	'Return Value' = @return_value
GO
*/
Create PROCEDURE Actualizar_TipoDeUsuario
    @ID int = NULL,
	@Nombre Varchar(15) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE TipoDeUsuario
    SET Nombre=ISNULL(@Nombre,Nombre)
    WHERE ID=@ID AND (@Nombre IS NOT NULL)
END 


-------Usuario
GO
CREATE PROC Insertar_Usuario
    @Cedula int,
	@Nombre varchar(15),
	@Apellidos varchar(25),
	@Usuario varchar(15)
AS 

SET NOCOUNT ON

INSERT INTO [dbo].Usuario
           ([Cedula]
           ,[Nombre]
           ,[Apellidos],
		   TipoUsuario)
     VALUES
           (@Cedula,
			@Nombre,
			@Apellidos,
			(select ID from TipoDeUsuario T where @Usuario= t.Nombre))
GO
/*
EXECUTE [dbo].Insertar_Expediente 
   @Cedula =702330809,
	@Nombre = 'Alonso',
	@Apellidos = 'Rivas Solano',
	@FechaNacimiento = '2018-06-04'
GO
*/

GO
Create Procedure Eliminar_Usuario @Cedula    INT 

AS
BEGIN

SET NOCOUNT ON 
DELETE FROM [dbo].Usuario WHERE Cedula = @Cedula;
END
GO

/*
EXECUTE [dbo].Eliminar_Expediente 
   @Cedula =702330809
GO
*/

CREATE PROCEDURE Seleccionar_Usuario
WITH EXECUTE AS CALLER  
AS  
    SET NOCOUNT ON;  
    SELECT U.ID as ID, U.Nombre AS Nombre , U.Apellidos 'Apellidos',   
      U.Cedula AS 'Cedula',   
      u.TipoUsuario AS 'Tipo de Usuario'
    FROM Usuario U   
    ORDER BY u.Apellidos ASC;  
GO  



/*
DECLARE	@return_value int
EXEC	@return_value = [dbo].[Actualizar_Expediente]
		@Cedula = 702330809,
		@Nombre = 'Alfonsos',
		@Apellidos = 'Rivas Solano',
		@FechaNacimiento = '1995-02-18'

SELECT	'Return Value' = @return_value
GO
*/
Create PROCEDURE Actualizar_Usuario
    @Cedula int = NULL,
	@Nombre varchar(15) = NULL,
	@Apellidos varchar(25)= NULL,
	@TipoUsuario varchar(15)= NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Usuario
    SET Apellidos=ISNULL(@Apellidos,Apellidos), 
        Nombre=ISNULL(@Nombre,Nombre), 
        Cedula=ISNULL(@Cedula, Cedula), 
        TipoUsuario=ISNULL((select ID from TipoDeUsuario where Nombre = @TipoUsuario ), TipoUsuario)
    WHERE Cedula=@Cedula AND (@Apellidos IS NOT NULL OR
                  @Nombre IS NOT NULL OR
                  @Cedula IS NOT NULL OR
				  @TipoUsuario IS NOT NULL)
END 