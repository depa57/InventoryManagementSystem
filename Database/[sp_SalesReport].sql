
create or ALTER   procedure [dbo].[sp_SalesReport]
(
@From DateTime,
@To DateTime
)
as
begin


	select sm.SalesDate,sm.CustomerName,sd.Productname,sd.Quantity,sd.Price Per_price,sd.TotalPrice from  Sales_mast_tb sm
	inner join sales_detl_tb sd on sd.SalesId=sm.SalesId
	where sm.SalesDate between @From and @To

end
