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

