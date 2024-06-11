USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_product]    Script Date: 6/11/2024 5:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   procedure [dbo].[sp_Insert_product]
(
@Prod_Name varchar(50),
@Prod_Quantity varchar(50),
@Prod_Price decimal(18,4)

)
as
begin
     insert into Product_tb (Prod_Name,Prod_Quantity,Prod_Price) values (@Prod_Name,@Prod_Quantity,@Prod_Price)
end