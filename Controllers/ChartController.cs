using ASPNET_MVC_ChartsDemo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;
using web4.Models;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using OfficeOpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using OfficeOpenXml.Table;
using System.Globalization;

namespace ASPNET_MVC_ChartsDemo.Controllers
{
    public class ChartController : Controller
    {
        // GET: Home
        SqlConnection con = new SqlConnection();
        SqlCommand sqlc = new SqlCommand();
        SqlDataReader dt;
        public void connectSQL()
        {
            con.ConnectionString = "Data source= " + "118.69.109.109" + ";database=" + "SAP_OPC" + ";uid=sa;password=Hai@thong";
        }
        public ActionResult Chart()
        {
            List<Chart> dataPoint = new List<Chart>();

            dataPoint.Add(new Chart("Samsung", 25));
            dataPoint.Add(new Chart("Micromax", 13));
            dataPoint.Add(new Chart("Lenovo", 8));
            dataPoint.Add(new Chart("Intex", 7));
            dataPoint.Add(new Chart("Reliance", (int)6.8));
            dataPoint.Add(new Chart("Others", (int)40.2));


            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoint);

            return View();
        }
        public ActionResult Demo()
        {
            List<B20DmDvt> dvcsList = GetDvcsData();
            return View(dvcsList);
        }
        public ActionResult DoanhThuChiNhanh_Admin()
        {
            List<Chart> dataPoint = new List<Chart>();
                
            dataPoint.Add(new Chart("Samsung", 25));
            dataPoint.Add(new Chart("Micromax", 13));
            dataPoint.Add(new Chart("Lenovo", 8));
            dataPoint.Add(new Chart("Intex", 7));
            dataPoint.Add(new Chart("Reliance", (int)6.8));
            dataPoint.Add(new Chart("Others",(int) 40.2));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoint);

            return View("DoanhThuChiNhanh_Admin");
        }
      
        public List<B20DmDvt> GetDvcsData()
        {
            List<B20DmDvt> dvcsList = new List<B20DmDvt>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Data source= " + "118.69.109.109" + ";database=" + "SAP_OPC" + ";uid=sa;password=Hai@thong"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT tendangnhap, hoten, Ma_Dvcs FROM view_user", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                B20DmDvt dvcs = new B20DmDvt
                                {
                                    tendangnhap = reader.GetString(0),       // Thay đổi index nếu cần
                                    hoten = reader.GetString(1),   // Thay đổi index nếu cần
                                    Ma_Dvcs = reader.GetString(2)      // Thay đổi index nếu cần
                                };

                                dvcsList.Add(dvcs);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
            }

            return dvcsList;
        }
    }
   

}