USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_update_stock]    Script Date: 6/13/2024 11:16:41 AM ******/
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
@Cost decimal(18,4),
@DateTime DateTime
)
as
begin
    update Stock_tb set
	                   SupplierId = @SupplierId,
                       ProductId = @ProductId ,
	                   Count = @Count,
	                   Cost = @Cost,
					   DateTime = @DateTime
					   where StockId = @StockId
end