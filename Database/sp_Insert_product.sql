create or alter procedure sp_Insert_product
(
@Prod_Name varchar(50),
@Prod_Code int,
@Prod_Quantity varchar(50),
@Prod_Price decimal(18,4)

)
as
begin
     insert into Product_tb (Prod_Name,Prod_Code,Prod_Quantity,Prod_Price) values (@Prod_Name,@Prod_Code,@Prod_Quantity,@Prod_Price)
end