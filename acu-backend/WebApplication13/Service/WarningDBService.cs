
using WebApplication13.Service;
using System.Data.SqlClient;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplication13.Model;
using System.Net;

namespace DI.Service
{
    public class WarningDBService
    {
        private readonly UserDBService _userService;
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        //發送email的人
        private string gmail_account = "tnny2455@gmail.com"; //Gmail 帳號
        private string gmail_password = "ypkm zvce aapc yfos"; //Gmail 密碼
        private string gmail_mail = "tnny2455@gmail.com"; //Gmail 信箱

        public WarningDBService(UserDBService userdbService, IConfiguration Configuration)
        {
            _userService = userdbService;
            configuration = Configuration;
            connectionString = configuration.GetConnectionString("Local");

        }

        #region 自動發送警示Email
        public string SendWarningEmail()
        {
            // 取得目前日期
            DateTime currentDate = DateTime.Now.Date;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 檢索不同分數範圍的資料，並檢查是否需要發送提醒
                string[] scoreRanges = { "0-12", "13-22", "23-32", "33-100" };
                int[] scoreDays = { 30, 15, 7, 3 };

                for (int i = 0; i < scoreRanges.Length; i++)
                {
                    string[] range = scoreRanges[i].Split('-');
                    int minScore = int.Parse(range[0]);
                    int maxScore = int.Parse(range[1]);
                    string query = $@"SELECT ""user"".user_account, D_Record.D_record_date
                    FROM ""user""
                     LEFT JOIN D_Record ON ""user"".user_id = D_Record.user_id
                     WHERE D_Record.D_record_score >= {minScore}
                     AND D_Record.D_record_score <= {maxScore}
                     AND DATEDIFF(day, D_Record.D_record_date, @currentDate) = {scoreDays[i]}";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@currentDate", currentDate);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string userEmail = reader["user_account"].ToString();
                            string subject = "";
                            string filePath = "";

                            if (minScore == 0 && maxScore == 12)
                            {
                                subject = "與穴共生系統復健提醒通知";
                                filePath = @"View/WarningN.html";
                            }
                            else if (minScore == 13 && maxScore == 22)
                            {
                                subject = "與穴共生系統復健提醒通知";
                                filePath = @"View/WarningMild.html";
                            }
                            else if (minScore == 23 && maxScore == 32)
                            {
                                subject = "與穴共生系統復健提醒通知";
                                filePath = @"View/WarningModerate.html";
                            }
                            else if (minScore == 33 && maxScore == 100)
                            {
                                subject = "與穴共生系統復健提醒通知";
                                filePath = @"View/WarningSevere.html";
                            }
                            string emailContent = System.IO.File.ReadAllText(filePath);
                            string name_sql = $@"SELECT * FROM (""user""  INNER JOIN D_record ON ""user"".user_id=D_record.user_id)";
                            SqlCommand cmd = new SqlCommand(name_sql, conn);
                            SqlDataReader dr = cmd.ExecuteReader();
                            dr.Read();
                            string user_name = dr["user_name"].ToString();
                            string user_account = dr["user_account"].ToString();

                            // 將使用者資料填入信件內容
                            emailContent = emailContent.Replace("{{UserName}}", user_name);
                            MailMessage mail = new MailMessage();
                            mail.Subject = subject;
                            mail.Body = emailContent;
                            mail.IsBodyHtml = true;
                            mail.From = new MailAddress(gmail_mail);
                            mail.To.Add(user_account);
                            // 建立 SMTP 連線
                            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                            smtpClient.Credentials = new NetworkCredential(gmail_account, gmail_password);
                            smtpClient.EnableSsl = true;
                            smtpClient.Send(mail);
                            return "郵件已成功寄出！";
                        }
                    }
                }
            }
            return "沒有需要發送的郵件！";
        }
        #endregion
    }
}



