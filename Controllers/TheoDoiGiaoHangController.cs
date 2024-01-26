using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web4.Models;

namespace web4.Controllers
{
    public class TheoDoiGiaoHangController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand sqlc = new SqlCommand();
        SqlDataReader dt;
        // GET: BaoCaoTienVeCN


        public void connectSQL()
        {
            con.ConnectionString = "Data source= " + "118.69.109.109" + ";database=" + "SAP_OPC" + ";uid=sa;password=Hai@thong";
        }

        // GET: TheoDoiGiaoHang

        public List<TheoDoiGiaoHang> LoadDmTDV()
        {
            string ma_dvcs = Request.Cookies["MA_DVCS"] != null ? Request.Cookies["MA_DVCS"].Value : "";
            connectSQL();
            
            List<TheoDoiGiaoHang> dataItems = new List<TheoDoiGiaoHang>();

            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("[usp_DanhSachTDV]", connection))
                {
                    command.CommandTimeout = 950;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@_Ma_Dvcs", ma_dvcs);

                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        // Kiểm tra xem DataSet có bảng dữ liệu hay không
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];

                            foreach (DataRow row in dt.Rows)
                            {
                                TheoDoiGiaoHang dataItem = new TheoDoiGiaoHang
                                {
                                    Ma_NVGH = row["Ma_CbNv"].ToString(),
                                    Ten_NVGH = row["hoten"].ToString(),
                                    Dvcs    = row["Ma_Dvcs"].ToString()
                                };

                                dataItems.Add(dataItem);
                            }
                        }
                    }
                }
            }

            return dataItems;
        }


        public List<TheoDoiGiaoHang> LoadHD()
        {
            string ma_dvcs = Request.Cookies["MA_DVCS"] != null ? Request.Cookies["MA_DVCS"].Value : "";
            string Ma_cbnv = Request.Cookies["Ma_NVGH"] != null ? Request.Cookies["Ma_NVGH"].Value : "";
            connectSQL();

            List<TheoDoiGiaoHang> dataItems = new List<TheoDoiGiaoHang>();

            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("[usp_DanhSachHoaDonGiaoHang_SAP]", connection))
                {
                    command.CommandTimeout = 950;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@_Ma_Dvcs",ma_dvcs);
                    command.Parameters.AddWithValue("@_Ma_CbNv", Ma_cbnv);


                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        // Kiểm tra xem DataSet có bảng dữ liệu hay không
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];

                            foreach (DataRow row in dt.Rows)
                            {
                                TheoDoiGiaoHang dataItem = new TheoDoiGiaoHang
                                {
                                    So_HD = row["so_ct"].ToString(),
                                    Ngay_HD = Convert.ToDateTime(row["Ngay_Ct1"]),
                                    
                                    Ma_Dt = row["Ma_dt"].ToString(),
                                    Ten_Dt = row["Ten_Dt"].ToString(),
                                    Ma_NVGH = row["Ma_nvgh"].ToString(),
                                    Tien_HD = float.Parse(row["tien"].ToString())


                            };

                                dataItems.Add(dataItem);
                            }
                        }
                    }
                }
            }

            return dataItems;
        }


        public List<TheoDoiGiaoHang> UpdateLoadHD()
        {
            string ma_dvcs = Request.Cookies["MA_DVCS"] != null ? Request.Cookies["MA_DVCS"].Value : "";
            string Ma_cbnv = Request.Cookies["NV_GiaoHang"] != null ? Request.Cookies["NV_GiaoHang"].Value : "";
            connectSQL();

            List<TheoDoiGiaoHang> dataItems = new List<TheoDoiGiaoHang>();

            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("[usp_DanhSachHoaDonGiaoHang_SAP]", connection))
                {
                    command.CommandTimeout = 950;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@_Ma_Dvcs", ma_dvcs);
                    command.Parameters.AddWithValue("@_Ma_CbNv", Ma_cbnv);


                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        // Kiểm tra xem DataSet có bảng dữ liệu hay không
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];

                            foreach (DataRow row in dt.Rows)
                            {
                                TheoDoiGiaoHang dataItem = new TheoDoiGiaoHang
                                {
                                    So_HD = row["so_ct"].ToString(),
                                    Ngay_HD = Convert.ToDateTime(row["Ngay_Ct1"]),

                                    Ma_Dt = row["Ma_dt"].ToString(),
                                    Ten_Dt = row["Ten_Dt"].ToString(),
                                    Ma_NVGH = row["Ma_nvgh"].ToString(),
                                    Tien_HD = float.Parse(row["tien"].ToString())


                                };

                                dataItems.Add(dataItem);
                            }
                        }
                    }
                }
            }

            return dataItems;
        }

        public ActionResult InsertGiaoHang()
        {
            List<TheoDoiGiaoHang> dmDlistTDV = LoadDmTDV();
           
            ViewBag.DataTDV = dmDlistTDV;
           
            return View();
        }
        public ActionResult InsetGiaoHangLoadHD()
        {
            List<TheoDoiGiaoHang> dmDlistTDV = LoadDmTDV();
            List<TheoDoiGiaoHang> dmListHD = LoadHD();
            ViewBag.DataTDV = dmDlistTDV;
            ViewBag.DataHD = dmListHD;
            return View();
        }
        public ActionResult SaveHD(TheoDoiGiaoHang TDGH)
        {
            TDGH.Dvcs = Request.Cookies["MA_DVCS"] != null ? Request.Cookies["MA_DVCS"].Value : "";
            TDGH.Ma_NVGH = Request.Cookies["Ma_NVGH"] != null ? Request.Cookies["Ma_NVGH"].Value : "";
            TDGH.Ten_NVGH = Request.Cookies["Ten_NVGH"] != null ? Request.Cookies["Ten_NVGH"].Value : "";


            string result = "Error!";
            connectSQL();
            if (TDGH != null && TDGH.Details != null)
            {
                try
                {
                    var detailsTable = new DataTable();
                    detailsTable.Columns.Add("So_Hd", typeof(int));
                    detailsTable.Columns.Add("Ngay_HD", typeof(string));
                    detailsTable.Columns.Add("Ma_Dt", typeof(int));
                    detailsTable.Columns.Add("Ten_Dt", typeof(string));
                    detailsTable.Columns.Add("NV_GN", typeof(string));
                    detailsTable.Columns.Add("Giao_HD", typeof(bool));
                    detailsTable.Columns.Add("Tien", typeof(float));
                    detailsTable.Columns.Add("Noi_Dung", typeof(string));
                    detailsTable.Columns.Add("Chua_giao_hang", typeof(bool));
                    foreach (var detail in TDGH.Details)
                    {
                        detailsTable.Rows.Add(detail.So_Hd, detail.Ngay_HD, detail.Ma_Dt,detail.Ten_Dt,detail.NV_GiaoNhan,detail.Giao_HD,detail.Tien_HD,detail.Noi_Dung,detail.Chua_giao_hang);
                    }

                    using (var connection = new SqlConnection(con.ConnectionString))
                    {
                        connection.Open();

                        using (var command = new SqlCommand("InsertTheoDoiGiaoHang_SAP", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@_Ngay_Ct", TDGH.Ngay_Ct);
                            command.Parameters.AddWithValue("@_so_Ct", TDGH.So_Ct);
                            command.Parameters.AddWithValue("@_NV_GiaoHang", TDGH.Ma_NVGH);
                            command.Parameters.AddWithValue("@_Ten_NVGiaoHang", TDGH.Ten_NVGH);

                            command.Parameters.AddWithValue("@_Dvcs", TDGH.Dvcs);
                            command.Parameters.AddWithValue("@_Ly_Do", TDGH.Ly_do);

                            // Pass details as a TVP parameter
                            var detailsParam = command.Parameters.AddWithValue("@_Details", detailsTable);
                            detailsParam.SqlDbType = SqlDbType.Structured;
                            detailsParam.TypeName = "B30GdetailType"; // Replace with your actual type name

                            command.ExecuteNonQuery();

                            result = "Success! Đã Lưu";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    result = $"Error! {ex.Message}";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            connectSQL();

            string Ma_DvCs = Request.Cookies["MA_DVCS"].Value;
            //Acc.UserName = Request.Cookies["UserName"].Value;
            //string query = "exec usp_Vth_BC_BHCNTK_CN @_ngay_Ct1 = '" + Acc.From_date + "',@_Ngay_Ct2 ='"+ Acc.To_date+"',@_Ma_Dvcs='"+ Acc.Ma_DvCs_1+"'";
            string Pname = "DanhSachTheoDoiGiaoHang";


            using (SqlCommand cmd = new SqlCommand(Pname, con))
            {
                cmd.CommandTimeout = 950;

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;


                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("@_Ma_Dvcs", Ma_DvCs);

                    sda.Fill(ds);

                }
            }


            return View(ds);
        }
        public ActionResult MauInGiaoHang()
        {
            DataSet ds = new DataSet();
            connectSQL();
            

            string Pname = "MauInGiaoHang";
            string Stt = Request.QueryString["STT"];
            using (SqlCommand cmd = new SqlCommand(Pname, con))
            {
                cmd.CommandTimeout = 950;

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;


                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("@_Stt", Stt);
                    sda.Fill(ds);

                }
            }


            return View(ds);
        }
        public ActionResult UpdateGiaoHang()
        {
            List<TheoDoiGiaoHang> dmDlistTDV = LoadDmTDV();
            List<TheoDoiGiaoHang> dmListHD = LoadHD();
            ViewBag.DataTDV = dmDlistTDV;
            ViewBag.DataHD = dmListHD;

            DataSet ds = new DataSet();
            connectSQL();

            string Pname = "[EditTheoDoiGiaoHang]";
            string Stt = Request.QueryString["STT"];
            using (SqlCommand cmd = new SqlCommand(Pname, con))
            {
                cmd.CommandTimeout = 950;

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;


                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {

                    cmd.Parameters.AddWithValue("@_Stt", Stt);
                    sda.Fill(ds);

                }
            }


            return View(ds);
        }
        public ActionResult SaveUpdate(TheoDoiGiaoHang TDGH)
        {
            TDGH.Dvcs = Request.Cookies["MA_DVCS"] != null ? Request.Cookies["MA_DVCS"].Value : "";
            TDGH.Ma_NVGH = Request.Cookies["Ma_NVGH"] != null ? Request.Cookies["Ma_NVGH"].Value : "";
            TDGH.Ten_NVGH = Request.Cookies["Ten_NVGH"] != null ? Request.Cookies["Ten_NVGH"].Value : "";

            string Stt = Request.QueryString["STT"];

            string result = "Error!";
            connectSQL();
            if (TDGH != null && TDGH.Details != null)
            {
                try
                {
                    var detailsTable = new DataTable();
                    detailsTable.Columns.Add("So_Hd", typeof(int));
                    detailsTable.Columns.Add("Ngay_HD", typeof(string));
                    detailsTable.Columns.Add("Ma_Dt", typeof(int));
                    detailsTable.Columns.Add("Ten_Dt", typeof(string));
                    detailsTable.Columns.Add("NV_GN", typeof(string));
                    detailsTable.Columns.Add("Giao_HD", typeof(bool));
                    detailsTable.Columns.Add("Tien", typeof(float));
                    detailsTable.Columns.Add("Noi_Dung", typeof(string));
                    detailsTable.Columns.Add("Chua_giao_hang", typeof(bool));
                    foreach (var detail in TDGH.Details)
                    {
                        detailsTable.Rows.Add(detail.So_Hd, detail.Ngay_HD, detail.Ma_Dt, detail.Ten_Dt, detail.NV_GiaoNhan, detail.Giao_HD, detail.Tien_HD, detail.Noi_Dung, detail.Chua_giao_hang);
                    }

                    using (var connection = new SqlConnection(con.ConnectionString))
                    {
                        connection.Open();

                        using (var command = new SqlCommand("UpdateTheoDoiGiaoHang_SAP", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@_Ngay_Ct", TDGH.Ngay_Ct);
                          
                            command.Parameters.AddWithValue("@_NV_GiaoHang", TDGH.Ma_NVGH);
                            command.Parameters.AddWithValue("@_Ten_NVGiaoHang", TDGH.Ten_NVGH);
                            command.Parameters.AddWithValue("@_Stt", Stt);


                            // Pass details as a TVP parameter
                            var detailsParam = command.Parameters.AddWithValue("@_Details", detailsTable);
                            detailsParam.SqlDbType = SqlDbType.Structured;
                            detailsParam.TypeName = "B30GdetailType"; // Replace with your actual type name

                            command.ExecuteNonQuery();

                            result = "Success! Đã Sửa";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    result = $"Error! {ex.Message}";
                }
                
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExportToExcel()
        {
            // Đường dẫn đến file mẫu Excel
            var templatePath = Server.MapPath("~/PathToYourTemplate/template.xlsx");
            FileInfo fileInfo = new FileInfo(templatePath);

            using (var package = new ExcelPackage(fileInfo))
            {
                // Chỉ định worksheet
                var worksheet = package.Workbook.Worksheets["Sheet1"]; // Hoặc tên sheet mẫu của bạn

                // Lấy dữ liệu từ database
                var dataItems = LoadHD();

                // Bắt đầu điền dữ liệu từ hàng nào đó, giả sử hàng thứ 10
                int row = 10;
                foreach (var item in dataItems)
                {
                    worksheet.Cells[row, 1].Value = item.So_HD;
                    worksheet.Cells[row, 2].Value = item.Ngay_HD.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 3].Value = item.Ten_Dt;
                    worksheet.Cells[row, 4].Value = item.Tien_HD;
                    // Điền tiếp các cột khác nếu cần
                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Lưu vào luồng và trả về file để tải về
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TheoDoiGiaoHang.xlsx");
            }
        }
    }
}