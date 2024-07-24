USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_SupplierPurchaseReport]    Script Date: 7/16/2024 12:48:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   procedure [dbo].[sp_SupplierPurchaseReport]
(
@From DateTime,
@To DateTime,
@SupplierId int,
@PurchaseId int
)
as
begin
    select SupplierName,Prod_Name,[Count],per_Product_Cost,[Date] from Purchase_tb pt
	inner join supplier_tb st 
	on st.SupplierID=pt.SupplierId
    inner join Product_tb pro
	on pt.ProductId=pro.ProductId


    where [Date] between @From and @To and pt.SupplierId=@SupplierId
end
