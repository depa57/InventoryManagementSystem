create or alter  procedure sp_update_product
(
@ProductId int,
@Prod_Name varchar(50),
@Prod_Code int,
@Prod_Quantity varchar(50),
@Prod_Price decimal(18,4)
)
as
begin
    update Product_tb set
	                 
                       Prod_Name = @Prod_Name ,
	                   Prod_Code= @Prod_Code,
	                   Prod_Quantity = @Prod_Quantity,
					   Prod_Price = @Prod_Price
					   where ProductId = @ProductId
end