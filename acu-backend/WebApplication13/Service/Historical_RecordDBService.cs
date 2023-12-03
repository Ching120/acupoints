
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
    public class Historical_RecordDBService
    {
        private readonly UserDBService _userService;
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public Historical_RecordDBService(IConfiguration Configuration)
        {

            configuration = Configuration;
            connectionString = configuration.GetConnectionString("Local");

        }

        #region 統計各乾眼症類型數量
        public List<GetD_record_TypeMathPeopleViewModel> GetD_record_TypeMath()
        {


            string Sql = $@"SELECT CM_type_id, COUNT(*) AS PeopleCount
                    FROM CM_output
                    WHERE  CM_type_id BETWEEN 0 AND 6
                    GROUP BY CM_type_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<GetD_record_TypeMathPeopleViewModel> DataList = new List<GetD_record_TypeMathPeopleViewModel>();
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(Sql, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        GetD_record_TypeMathPeopleViewModel Data = new GetD_record_TypeMathPeopleViewModel();
                        int CM_type_id = reader.GetInt32(reader.GetOrdinal("CM_type_id"));
                        int PeopleCount = reader.GetInt32(reader.GetOrdinal("PeopleCount"));

                        switch (CM_type_id)
                        {
                            case 0:
                                Data.None = PeopleCount;
                                break;
                            case 1:
                                Data.Liver_kidney_yin_deficiency = PeopleCount;
                                break;
                            case 2:
                                Data.Insufficient_lung_yin = PeopleCount;
                                break;
                            case 3:
                                Data.Dampness_heat_spleen_stomach = PeopleCount;
                                break;
                            case 4:
                                Data.Spleen_lung_latent_heat = PeopleCount;
                                break;
                            case 5:
                                Data.Liver_Qi_stagnation = PeopleCount;
                                break;
                            case 6:
                                Data.Deficiency_coldness_lungs = PeopleCount;
                                break;
                        }

                        DataList.Add(Data);
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
                return DataList;
            }
        }
        #endregion





        #region 統計今日復健人數
        public List<GetR_RecordPeopleViewModel> GetR_recordP(DateTime R_record_date)
        {
            string Sql = $@"SELECT user_id, COUNT(*) AS R_record_People FROM R_record WHERE R_record_date = @R_record_date GROUP BY user_id";


            List<GetR_RecordPeopleViewModel> DataList = new List<GetR_RecordPeopleViewModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(Sql, conn);
                    command.Parameters.AddWithValue("@R_record_date", R_record_date);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        GetR_RecordPeopleViewModel Data = new GetR_RecordPeopleViewModel();
                        Data.R_record_People = Convert.ToInt32(reader["R_record_People"]);

                        DataList.Add(Data);
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
                return DataList;
            }
        }
        #endregion

        #region 	總覽使用者診斷復健紀錄日期

        public List<DateTime> GetDR_record(Guid user_id)
        {

            string sql = "SELECT R_record_date FROM R_record WHERE user_id=@user_id UNION SELECT D_record_date FROM D_record WHERE user_id=@user_id";
            List<DateTime> dateList = new List<DateTime>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime date = reader.GetDateTime(0);
                        dateList.Add(date.Date);
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

                return dateList;
            }
        }
        #endregion


    }
}


