/*
Run this script on:

        118.67.248.175.JewelofIndia    -  This database will be modified

to synchronize it with:

        SYDCN0756.JewelofIndia

You are recommended to back up your database before running this script

Script created by SQL Compare version 10.7.0 from Red Gate Software Ltd at 23/01/2015 11:48:08 PM

*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Dropping index [IX_FK_ApartmetSales_User] from [dbo].[ApartmetSales]'
GO
DROP INDEX [IX_FK_ApartmetSales_User] ON [dbo].[ApartmetSales]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[ApartmetSales]'
GO
ALTER TABLE [dbo].[ApartmetSales] ADD
[SalesType] [smallint] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD
[UserTypeId] [int] NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[sp_GetApartmentSalesByUser]'
GO
ALTER PROCEDURE [dbo].[sp_GetApartmentSalesByUser]
	-- Add the parameters for the stored procedure here
	@UserId BIGINT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
	DECLARE @IsAdmin BIT
	SELECT @IsAdmin = IsOwner FROM dbo.Users WHERE id =  @UserId
	IF @IsAdmin = 1
	BEGIN
		SELECT p.Description AS [PropertyDesc],T.TowerDirection AS [TowerDesc],A.Description AS [ApartmentDesc],CONVERT(VARCHAR(24),BlockStartDate,103) AS [StartDate] FROM dbo.Properties p WITH(NOLOCK) INNER JOIN dbo.Towers T ON	T.PropertyId = p.Id INNER JOIN dbo.Apartments A ON	A.TowerId = T.Id
		INNER JOIN dbo.ApartmetSales ON	ApartmetSales.ApartmentId = A.Id
		WHERE ApartmetSales.IsBlocked = 1
		
	END
	ELSE
	BEGIN
		SELECT p.Description AS [PropertyDesc],T.TowerDirection AS [TowerDesc],A.Description AS [ApartmentDesc],CONVERT(VARCHAR(24),BlockStartDate,103) AS [StartDate]  FROM dbo.Properties p WITH(NOLOCK) INNER JOIN dbo.Towers T ON	T.PropertyId = p.Id INNER JOIN dbo.Apartments A ON	A.TowerId = T.Id
		INNER JOIN dbo.ApartmetSales ON	ApartmetSales.ApartmentId = A.Id
		WHERE UserId = @UserId AND ApartmetSales.IsBlocked = 1
		
	END
END
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[sp_GetApartments]'
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetApartments]
	-- Add the parameters for the stored procedure here
	@TowerId bigint
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Apartments].[Id]
      ,[BedRoom]
      ,[Bathroom]
      ,[Garage]
      ,[Description]
      ,[FloorLevel]
      ,[Direction]
      ,[TowerId]
  FROM [dbo].[Apartments] WITH(NOLOCK)  
  where TowerId=@TowerId
  AND Id NOT IN (SELECT ApartmentId FROM dbo.ApartmetSales WITH(NOLOCK) WHERE IsBlocked =1 )
END

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[sp_GetProperties]'
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetProperties]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT A.[Id]
      ,[Feature]
      ,A.[Description]
      ,B.Address + ' - ' + B.state + ' - ' + B.Country as Location 
      ,C.CodeReference
  FROM [dbo].[Properties] A, dbo.Locations B, dbo.PropertyTypes C
  
    where A.LocationId = B.Id
	and A.PropertyTypeId = C.Id
END

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[UserType]'
GO
CREATE TABLE [dbo].[UserType]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserTypeCode] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_UserType] on [dbo].[UserType]'
GO
ALTER TABLE [dbo].[UserType] ADD CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[sp_GetUser]'
GO

ALTER PROCEDURE [dbo].[sp_GetUser]
	-- Add the parameters for the stored procedure here
	@UserName NVARCHAR(100),
	@Password NVARCHAR(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	SELECT U.[Id]
      ,[UserName]
      ,[EmailId]
      ,[IsOwner]
      ,[MobileNo]
      ,[DOB]
      ,UserTypeCode
      
  FROM [dbo].[Users] U
  INNER JOIN dbo.UserType ON U.UserTypeId = UserType.Id
  where UserName = @UserName
  AND Password=@Password

END





GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[ApartmentSalesType]'
GO
CREATE TABLE [dbo].[ApartmentSalesType]
(
[Id] [smallint] NOT NULL IDENTITY(1, 1),
[SalesType] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_ApartmentSalesType] on [dbo].[ApartmentSalesType]'
GO
ALTER TABLE [dbo].[ApartmentSalesType] ADD CONSTRAINT [PK_ApartmentSalesType] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[ApartmetSales]'
GO
ALTER TABLE [dbo].[ApartmetSales] ADD CONSTRAINT [FK_ApartmetSales_ApartmentSalesType] FOREIGN KEY ([SalesType]) REFERENCES [dbo].[ApartmentSalesType] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [FK_Users_UserType] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserType] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
