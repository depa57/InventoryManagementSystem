using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace InventoryManagementSystem.Controllers
{
    public class StockFillController : Controller
    {
        private readonly IConfiguration _configuration;

        public StockFillController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: StockFillController
        public ActionResult StockList()
        {
            try
            {
                var responseData = new List<StockModel>();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "select *from Stock_tb";
                        cmd.CommandType = System.Data.CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                responseData.Add(new StockModel()
                                {
                                    StockId = Convert.ToInt32(reader["StockId"]),
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Count = Convert.ToInt32(reader["Count"]),
                                    Cost = (reader.GetDecimal(reader.GetOrdinal("Cost")))

                                });
                            }
                        }
                    }
                    con.Close();
                }

                return View(responseData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: StockFillController/Details/5
        public ActionResult Details(int id)
        {
            StockModel responseData = GetStockById(id);

            return View(responseData);

        }
        private StockModel GetStockById(int id)
        {
            var responseData = new StockModel();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select *from Stock_tb where StockId=@StockId";
                    cmd.Parameters.Add(new SqlParameter("@StockId", id));

                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            responseData.StockId = Convert.ToInt32(reader["StockId"]);
                            responseData.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                            responseData.ProductId = Convert.ToInt32(reader["ProductId"]);
                            responseData.Count = Convert.ToInt32(reader["Count"]);
                            responseData.Cost = (reader.GetDecimal(reader.GetOrdinal("Cost")));



                        }

                    }

                }
                con.Close();

            }

            return responseData;
        }

        // GET:StockFillController/Create
        public ActionResult Create()
        {
            var items = new List<ProductDetails>();

            // Establish a connection to the database
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    // SQL query to retrieve data
                    cmd.Connection = con;
                    cmd.CommandText = "select ProductId,Prod_Name from Product_tb";
                    cmd.CommandType = System.Data.CommandType.Text;
                    // Execute the query and read the data
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Add items to the list
                        items.Add(new ProductDetails
                        {

                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            Prod_Name = reader["Prod_Name"].ToString()
                        });
                    }

                    reader.Close();
                }
            }
            StockRelatedComponents stockRelatedcomponents = new StockRelatedComponents()
            {
                details = new StockModel(),
                ProductList = items.Select
            (u =>
                new SelectListItem
                {
                    Text = u.Prod_Name,
                    Value = u.ProductId.ToString()
                }
            )

               


            };
            return View(stockRelatedcomponents);
        }

        // POST: StockFillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockRelatedComponents stockRelatedComponents)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "sp_Insert_stock";
                        cmd.Parameters.Add(new SqlParameter("@SupplierId", stockRelatedComponents.details.SupplierId));
                        cmd.Parameters.Add(new SqlParameter("@ProductId", stockRelatedComponents.details.ProductId));
                        cmd.Parameters.Add(new SqlParameter("@Count", stockRelatedComponents.details.Count));
                        cmd.Parameters.Add(new SqlParameter("@Cost", stockRelatedComponents.details.Cost));
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();


                    }
                    con.Close();
                }

                return RedirectToAction(nameof(StockList));
            }
            catch
            {
                return View();
            }
        }




        // GET: StockFillController/Edit/5
        public ActionResult Edit(int id)
        {
            StockModel responseData = GetStockById(id);

            return View(responseData);
        }

        // POST: StockFillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StockModel data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "sp_update_stock";
                        cmd.Parameters.Add(new SqlParameter("@StockId", data.StockId));
                        cmd.Parameters.Add(new SqlParameter("@SupplierId", data.SupplierId));
                        cmd.Parameters.Add(new SqlParameter("@ProductId", data.ProductId));
                        cmd.Parameters.Add(new SqlParameter("@Count", data.Count));
                        cmd.Parameters.Add(new SqlParameter("@Cost", data.Cost));
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                    }
                    con.Close();

                }
                return RedirectToAction(nameof(StockList));
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        // GET: StockFillController/Delete/5
        public ActionResult Delete(int id)
        {
            StockModel responseData = GetStockById(id);

            return View(responseData);
        }

        // POST: StockFillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StockModel collection)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "delete from Stock_tb where StockId= @StockId";
                        cmd.Parameters.Add(new SqlParameter("@StockId", id));
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();


                    }
                    con.Close();

                }
                return RedirectToAction(nameof(StockList));
            }
            catch
            {
                return View();
            }


        }



        // Action method to render the view with the dropdown
        public ActionResult DropDownList()
        {
            return View();
        }
    }
}

