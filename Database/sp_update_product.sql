USE [ado_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_update_product]    Script Date: 6/14/2024 9:43:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER    procedure [dbo].[sp_update_product]
(
@ProductId int,
@Prod_Name varchar(50)

)
as
begin
    update Product_tb set
	                 
                       Prod_Name = @Prod_Name 
	                   where ProductId = @ProductId
end