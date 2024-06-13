USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_stock]    Script Date: 6/13/2024 11:15:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   procedure [dbo].[sp_Insert_stock]
(
@SupplierId int,
@ProductId int,
@Count int,
@Cost decimal(18,4),
@DateTime DateTime


)
as
begin
     -- stock table insert data
	 insert into Stock_tb (SupplierId,ProductId,[Count],Cost,DateTime) 
	 values (@SupplierId,@ProductId,@Count,@Cost,@DateTime)

	 --product table data update
	 update product_tb set 
	   Prod_Quantity = Prod_Quantity+@Count,
	   Prod_Price = Prod_Price+@Cost
	   where ProductId = @ProductId
	         

end
