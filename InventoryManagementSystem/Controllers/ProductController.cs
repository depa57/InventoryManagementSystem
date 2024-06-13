using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: ProductController
        public ActionResult ProductList()
        {
            try
            {
                var responseData = new List<ProductDetails>();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "select *from Product_tb";
                        cmd.CommandType = System.Data.CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                responseData.Add(new ProductDetails()
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Prod_Name = (reader["Prod_Name"].ToString()),
                                  
                                    Prod_Quantity = Convert.ToInt32(reader["Prod_Quantity"]),
                                    Prod_Price = (reader.GetDecimal(reader.GetOrdinal("Prod_Price")))

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

                throw ex;
            }

        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            ProductDetails responseData = GetProductById(id);

            return View(responseData);
        }

        private ProductDetails GetProductById(int id)
        {
            var responseData = new ProductDetails();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select *from Product_tb where ProductId=@ProductId";
                    cmd.Parameters.Add(new SqlParameter("@ProductId", id));
                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            responseData.ProductId = Convert.ToInt32(reader["ProductId"]);
                            responseData.Prod_Name = (reader["Prod_Name"].ToString()); 
                         
                            responseData.Prod_Quantity = Convert.ToInt32(reader["Prod_Quantity"]);
                            responseData.Prod_Price = (reader.GetDecimal(reader.GetOrdinal("Prod_Price")));


                        }

                    }

                }
                con.Close();

            }

            return responseData;
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductDetails data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "sp_Insert_product";
                        cmd.Parameters.Add(new SqlParameter("@Prod_Name", data.Prod_Name));
                        cmd.Parameters.Add(new SqlParameter("@Prod_Quantity", data.Prod_Quantity));
                        cmd.Parameters.Add(new SqlParameter("@Prod_Price", data.Prod_Price));
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();


                    }
                    con.Close();

                }
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ProductDetails responseData = GetProductById(id);

            return View(responseData);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductDetails data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "sp_update_product";

                        cmd.Parameters.Add(new SqlParameter("@ProductId", data.ProductId));
                        cmd.Parameters.Add(new SqlParameter("@Prod_Name", data.Prod_Name));
                      
                        cmd.Parameters.Add(new SqlParameter("@Prod_Quantity", data.Prod_Quantity));
                        cmd.Parameters.Add(new SqlParameter("@Prod_Price", data.Prod_Price));
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();


                    }
                    con.Close();

                }
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            ProductDetails responseData = GetProductById(id);

            return View(responseData);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductDetails collection)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "delete from Product_tb where ProductId = @ProductId";
                        cmd.Parameters.Add(new SqlParameter("@ProductId", id));
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();


                    }
                    con.Close();

                }
                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }
    }
}
