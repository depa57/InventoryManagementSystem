USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_Insert_stock]    Script Date: 6/9/2024 2:13:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   procedure [dbo].[sp_Insert_stock]
(
@SupplierId int,
@ProductId int,
@Count int,
@Cost decimal(18,4) 
)
as
begin
     insert into Stock_tb (SupplierId,ProductId,[Count],Cost) values (@SupplierId,@ProductId,@Count,@Cost)
end