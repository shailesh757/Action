USE [JewelofIndia]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetApartmentWithStatus]    Script Date: 02/13/2015 22:45:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetApartmentWithStatus] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [BedRoom]
      ,[Bathroom]
      ,[Garage]
      ,[Description]
      ,[FloorLevel]      
	  ,AAS.[IsBlocked]
	  ,AAS.BlockStartDate
	  ,AAS.BlockEndDate
	  ,ApartmentSalesType.SalesType
	  
  FROM [dbo].[Apartments] A  
  LEFT join [dbo].[ApartmetSales] AAS on A.id = AAS.apartmentId
  LEFT JOIN dbo.ApartmentSalesType ON	ApartmentSalesType.Id = AAS.SalesType
   
END

GO

USE [JewelOfIndia]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetApartmentSalesDetail]    Script Date: 03/26/2015 00:05:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetApartmentSalesDetail]
	-- Add the parameters for the stored procedure here
	@UserId BIGINT,
	@SalesType VARCHAR(20)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
	DECLARE @IsAdmin BIT
	DECLARE @salesTypeId SMALLINT
	SELECT @IsAdmin = IsOwner FROM dbo.Users WHERE id =  @UserId
	SELECT @salesTypeId = id FROM dbo.ApartmentSalesType WHERE SalesType=@SalesType
	IF @IsAdmin = 1
	BEGIN
		SELECT p.Description AS [PropertyDesc],T.TowerName AS [TowerDesc],A.Description AS [ApartmentDesc],A.BedRoom,A.FloorLevel,A.Area,CONVERT(VARCHAR(24),
		BlockStartDate,103) AS [StartDate] ,DATEDIFF(DAY,GETDATE(),BlockEndDate) AS [DaysLeftForLockToExpire],dbo.Users.UserName FROM dbo.Properties p WITH(NOLOCK) 
		INNER JOIN dbo.Towers T ON	T.PropertyId = p.Id 
		INNER JOIN dbo.Apartments A ON	A.TowerId = T.Id
		INNER JOIN dbo.ApartmetSales ON	ApartmetSales.ApartmentId = A.Id
		INNER JOIN dbo.Users ON	Users.Id = ApartmetSales.UserId
		WHERE ApartmetSales.IsBlocked = 1 AND dbo.ApartmetSales.SalesType= @salesTypeId
		
	END
	ELSE
	BEGIN
		SELECT p.Description AS [PropertyDesc],T.TowerName AS [TowerDesc],A.Description AS [ApartmentDesc],A.BedRoom,A.FloorLevel,A.Area,CONVERT(VARCHAR(24),
		BlockStartDate,103) AS [StartDate] ,DATEDIFF(DAY,GETDATE(),BlockEndDate) AS [DaysLeftForLockToExpire],dbo.Users.UserName FROM dbo.Properties p WITH(NOLOCK) 
		INNER JOIN dbo.Towers T ON	T.PropertyId = p.Id 
		INNER JOIN dbo.Apartments A ON	A.TowerId = T.Id
		INNER JOIN dbo.ApartmetSales ON	ApartmetSales.ApartmentId = A.Id
		INNER JOIN dbo.Users ON	Users.Id = ApartmetSales.UserId
		WHERE UserId = @UserId AND ApartmetSales.IsBlocked = 1 AND dbo.ApartmetSales.SalesType= @salesTypeId
		
	END
END

GO




