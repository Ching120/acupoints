
using WebApplication13.Service;
using System.Data.SqlClient;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplication13.Model;
using WebApplication13.Controllers;
using System.Linq;
using WebApplication13.ViewModel;
using System.Security.Principal;

namespace WebApplication13.Service
{
    public class Acupuncture_PointsDBService
    {
        private readonly UserDBService _userService;
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly IWebHostEnvironment _environment;
        public Acupuncture_PointsDBService(UserDBService userdbService, IConfiguration Configuration, IWebHostEnvironment environment)
        {
            _userService = userdbService;
            configuration = Configuration;
            connectionString = configuration.GetConnectionString("Local");
            _environment = environment;
        }
        #region 新增穴道資訊
        public string NewAcupuncture_Points(Acupuncture_PointsViewModel value)
        {

            string rootRoot = _environment.ContentRootPath + @"\wwwroot\img\";
            var filename = "";
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (value.acupuncture_points_img.Length > 0)
            {
                filename = date + value.acupuncture_points_img.FileName;
                using (var stream = System.IO.File.Create(rootRoot + filename))
                {
                    value.acupuncture_points_img.CopyTo(stream);
                }
            }


            SqlConnection conn = new SqlConnection(connectionString);
            string sql = $@"INSERT INTO Acupuncture_Points
                        (acupuncture_points_id,acupuncture_points_name,acupuncture_points_location,acupuncture_points_press,acupuncture_points_detail,acupuncture_points_content,acupuncture_points_img,isdel,create_id,create_time,update_id,update_time) 
                        VALUES (@acupuncture_points_id,@acupuncture_points_name,@acupuncture_points_location,@acupuncture_points_press,@acupuncture_points_detail,@acupuncture_points_content,@acupuncture_points_img,@isdel,@create_id,@create_time,@update_id,@update_time)";

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@acupuncture_points_id", Guid.NewGuid());
                command.Parameters.AddWithValue("@acupuncture_points_name", value.acupuncture_points_name);
                command.Parameters.AddWithValue("@acupuncture_points_location", value.acupuncture_points_location);
                command.Parameters.AddWithValue("@acupuncture_points_press", value.acupuncture_points_press);
                command.Parameters.AddWithValue("@acupuncture_points_detail", value.acupuncture_points_detail);
                command.Parameters.AddWithValue("@acupuncture_points_content", value.acupuncture_points_content);
                command.Parameters.AddWithValue("@acupuncture_points_img", filename);

                command.Parameters.AddWithValue("@isdel", 0);
                command.Parameters.AddWithValue("@create_id", "Admin");
                command.Parameters.AddWithValue("@create_time", DateTime.Now);
                command.Parameters.AddWithValue("@update_id", "Admin");
                command.Parameters.AddWithValue("@update_time", DateTime.Now);
                int num = command.ExecuteNonQuery();
                if (num > 0)
                {
                    return "新增成功！";
                }
                else
                {
                    return "新增失敗，請重試！";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 修改穴道資訊
        public string PutAcupuncture_Points(Guid acupuncture_points_id, UpdateAcupuncture_PointsViewModel value)
        {
            var filename = "";
            if (value.acupuncture_points_img != null)
            {
                //圖片存入資料夾
                string rootRoot = _environment.ContentRootPath + @"\wwwroot\img\";
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (value.acupuncture_points_img.Length > 0)
                {
                    filename = date + value.acupuncture_points_img.FileName;
                    using (var stream = System.IO.File.Create(rootRoot + filename))
                    {
                        value.acupuncture_points_img.CopyTo(stream);
                    }
                }
            }
            string sql = $@"UPDATE Acupuncture_Points SET acupuncture_points_name=@acupuncture_points_name,acupuncture_points_location=@acupuncture_points_location,acupuncture_points_press=@acupuncture_points_press,acupuncture_points_detail=@acupuncture_points_detail,acupuncture_points_content=@acupuncture_points_content,acupuncture_points_img=@acupuncture_points_img,update_id=@update_id,update_time=@update_time WHERE acupuncture_points_id = @acupuncture_points_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@acupuncture_points_id", acupuncture_points_id);
                try
                {

                    conn.Open();
                   
                    command.Parameters.AddWithValue("@acupuncture_points_name", value.acupuncture_points_name);
                    command.Parameters.AddWithValue("@acupuncture_points_location", value.acupuncture_points_location);
                    command.Parameters.AddWithValue("@acupuncture_points_press", value.acupuncture_points_press);
                    command.Parameters.AddWithValue("@acupuncture_points_detail", value.acupuncture_points_detail);
                    command.Parameters.AddWithValue("@acupuncture_points_content", value.acupuncture_points_content);
                    if (value.acupuncture_points_img != null)
                    {
                        command.Parameters.AddWithValue("@acupuncture_points_img", filename);
                    }
                    command.Parameters.AddWithValue("@update_time", DateTime.Now);
                    command.Parameters.AddWithValue("@update_id", "admin");
                    int row = command.ExecuteNonQuery();
                    if (row > 0)
                    {
                        return "修改成功！";
                    }
                    else
                    {
                        return "修改失敗，請重試！";
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region 刪除穴道資訊
        public string DeleteAcupuncture_Points(string acupuncture_points_id)
        {
            string sql = $@"  UPDATE Acupuncture_Points SET isdel=@isdel WHERE acupuncture_points_id = @acupuncture_points_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@isdel", '1');
                    command.Parameters.AddWithValue("@acupuncture_points_id", acupuncture_points_id);
                    int num = command.ExecuteNonQuery();
                    if (num > 0)
                    {
                        return "修改成功！";
                    }
                    else
                    {
                        return "修改失敗，請重試！";
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion



        #region 總覽穴道名稱

        public List<GetAcupuncture_PointsViewModel> GetAcupuncture_PointsName()
        {
            string Sql = "SELECT * FROM Acupuncture_Points where isdel='false'";

            List<GetAcupuncture_PointsViewModel> DataList = new List<GetAcupuncture_PointsViewModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                try
                {
                    conn.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        GetAcupuncture_PointsViewModel Data = new GetAcupuncture_PointsViewModel();
                        Data.acupuncture_points_id = (Guid)dr["acupuncture_points_id"];
                        Data.acupuncture_points_name = dr["acupuncture_points_name"].ToString();
                        DataList.Add(Data);
                    }
                }
                catch (Exception e)
                {
                    //丟出錯誤
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return DataList;
            }
        }
        #endregion
        #region 總覽	詳細穴位資訊

        public List<GetAP_InformationViewModel> GetAcupuncture_Points_Infromation(Guid acupuncture_points_id)
        {
            string Sql = $@"SELECT * FROM Acupuncture_Points where acupuncture_points_id=@acupuncture_points_id";


        
               //string Sql = @$"SELECT * FROM Acupuncture_Points where isdel='false'";
          
            List<GetAP_InformationViewModel> DataList = new List<GetAP_InformationViewModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
             
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@acupuncture_points_id", acupuncture_points_id);
                    SqlDataReader dr = command.ExecuteReader();
                  
                    dr.Read();

                    GetAP_InformationViewModel Data = new GetAP_InformationViewModel();
                   
                    Data.acupuncture_points_name = dr["acupuncture_points_name"].ToString();
                    Data.acupuncture_points_location = dr["acupuncture_points_location"].ToString();
                    Data.acupuncture_points_press = dr["acupuncture_points_press"].ToString();
                    Data.acupuncture_points_detail = dr["acupuncture_points_detail"].ToString();
                    Data.acupuncture_points_content = dr["acupuncture_points_content"].ToString();
                    Data.update_id = dr["update_id"].ToString();
                    Data.update_time = Convert.ToDateTime(dr["update_time"]);
                    DataList.Add(Data);

                }
                catch (Exception e)
                {
                    //丟出錯誤
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return DataList;
            }
        }
        #endregion
        #region 詳細穴位資訊

        public List<GetAll_Acupuncture_PointsViewModel> GetAcupuncture_Points(Guid user_id, DateTime R_record_date)
        {

            string Sql = $@"
                    SELECT * FROM (( ""user""
                     INNER JOIN R_record  ON  ""user"".user_id =R_record.user_id)
                     INNER JOIN Acupuncture_Points  ON  R_record.acupuncture_points_id =Acupuncture_Points.acupuncture_points_id)
                        WHERE ""user"".user_id = @user_id AND Acupuncture_Points.isdel='false' AND R_record.R_record_date=@R_record_date";

    //        string Sql = $@"
    //SELECT * FROM (( ""user""
    //INNER JOIN R_record ON ""user"".user_id = R_record.user_id)
    //INNER JOIN Acupuncture_Points ON R_record.acupuncture_points_id = Acupuncture_Points.acupuncture_points_id)
    //WHERE ""user"".user_id = @user_id AND Acupuncture_Points.isdel = 'false' AND R_record.R_record_date = @R_record_date";

            var img = "";
            List<GetAll_Acupuncture_PointsViewModel> DataList = new List<GetAll_Acupuncture_PointsViewModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(Sql, conn);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@R_record_date", R_record_date);
                try
                {
                    conn.Open();

                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        GetAll_Acupuncture_PointsViewModel Data = new GetAll_Acupuncture_PointsViewModel();
                        Data.acupuncture_points_id = (Guid)dr["acupuncture_points_id"];
                        Data.acupuncture_points_name = dr["acupuncture_points_name"].ToString();
                        Data.acupuncture_points_location = dr["acupuncture_points_location"].ToString();
                        Data.acupuncture_points_press = dr["acupuncture_points_press"].ToString();
                        Data.acupuncture_points_detail = dr["acupuncture_points_detail"].ToString();
                        Data.acupuncture_points_content = dr["acupuncture_points_content"].ToString();
                        Data.acupuncture_points_img = img;
                        Data.R_record_finish = Convert.ToInt32(dr["R_record_finish"]);
                        DataList.Add(Data);
                    }
                }
                catch (Exception e)
                {
                    //丟出錯誤
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return DataList;
            }
        }
        #endregion








    }
}


