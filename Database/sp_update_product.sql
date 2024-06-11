USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_update_product]    Script Date: 6/11/2024 3:20:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER    procedure [dbo].[sp_update_product]
(
@ProductId int,
@Prod_Name varchar(50),
@Prod_Quantity varchar(50),
@Prod_Price decimal(18,4)
)
as
begin
    update Product_tb set
	                 
                       Prod_Name = @Prod_Name ,
	                   Prod_Quantity = @Prod_Quantity,
					   Prod_Price = @Prod_Price
					   where ProductId = @ProductId
end