USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_update_stock]    Script Date: 6/9/2024 3:40:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER    procedure [dbo].[sp_update_stock]
(
@StockId int,
@SupplierId int,
@ProductId int,
@Count int,
@Cost decimal(18,4)
)
as
begin
    update Stock_tb set
	                   SupplierId = @SupplierId,
                       ProductId = @ProductId ,
	                   Count = @Count,
	                   Cost = @Cost
					   where StockId = @StockId
end