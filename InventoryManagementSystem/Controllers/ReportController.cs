using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IConfiguration _configuration;
        public ReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: ReportController/PurchaseReport
        public ActionResult PurchaseReport()
        {
            var items = new List<SupplierDetails>();
            var a = _configuration.GetConnectionString("DefaultConnection");
            // Establish a connection to the database
            using (SqlConnection con = new SqlConnection(a))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    // SQL query to retrieve data
                    cmd.Connection = con;
                    cmd.CommandText = "select SupplierID,SupplierName from supplier_tb";
                    cmd.CommandType = System.Data.CommandType.Text;
                    // Execute the query and read the data
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Add items to the list
                        items.Add(new SupplierDetails
                        {

                            SupplierID = Convert.ToInt32(reader["SupplierID"]),
                            SupplierName = reader["SupplierName"].ToString()
                        });
                    }

                    reader.Close();
                }
            }
            PurchaseRepoDropDown purchaseRepoDropDown = new PurchaseRepoDropDown()
            {
                details = new PurchaseReportModel(),
                SupplierList = items.Select
            (u =>
                new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierID.ToString()
                }
            )
            };
            return View(purchaseRepoDropDown);
        }

        // POST: ReportController/PurchaseReport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseReport(PurchaseRepoDropDown purchaseRepoDropDown)
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
                        cmd.CommandText = "sp_SupplierPurchaseReport";
                        cmd.Parameters.Add(new SqlParameter("@PurchaseId", purchaseRepoDropDown.details.PurchaseId));
                        cmd.Parameters.Add(new SqlParameter("@From", purchaseRepoDropDown.details.From));
                        cmd.Parameters.Add(new SqlParameter("@To", purchaseRepoDropDown.details.To));
                        cmd.Parameters.Add(new SqlParameter("@SupplierId", purchaseRepoDropDown.details.SupplierId));

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        
                       SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                responseData.Add(new StockModel() {
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Count = Convert.ToInt32(reader["Count"]),
                                    TotalCost = (reader.GetDecimal(reader.GetOrdinal("Cost"))),
                                    Date = Convert.ToDateTime(reader["Date"].ToString())
                                });
                              


                            }

                        }


                    }
                    con.Close();
                }

                return RedirectToAction(nameof(PurchaseReportList), new { data = JsonConvert.SerializeObject(responseData) });
            }
            catch
            {
                return View();
            }
        }


        public ActionResult PurchaseReportList(string data)
        {
            var purchaseList = JsonConvert.DeserializeObject(data);
            return View(purchaseList);
        }

    }
}
