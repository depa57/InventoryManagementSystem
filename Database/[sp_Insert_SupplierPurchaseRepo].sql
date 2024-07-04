USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_SupplierPurchaseRepo]    Script Date: 7/3/2024 2:40:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER     procedure [dbo].[sp_Insert_SupplierPurchaseRepo]
(
@From DateTime,
@To DateTime,
@SupplierId int
)
as
begin
    select SupplierId,ProductId,[Count],Cost,[Date] from Stock_tb 
	where [Date] between @From and @To
end


