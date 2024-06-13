USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_stock]    Script Date: 6/13/2024 1:25:13 PM ******/
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
@Date DateTime
)
as
begin
     insert into Stock_tb (SupplierId,ProductId,[Count],Cost,Date) values (@SupplierId,@ProductId,@Count,@Cost,@Date)
end
begin
      update Product_tb set 
	    Prod_Quantity = Prod_Quantity+@Count,
		Prod_Price = Prod_Price+@Cost 

end

