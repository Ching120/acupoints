﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication13.ViewModel
{
    public class GetAccountViewModel

    {
  
        public string user_account { get; set; }

        public string user_password { get; set; }
        // 姓名
        //public string user_newpassword { get; set; }
        public string user_name { get; set; }
        //性別
        public int  user_gender { get; set; }
        //年齡
        public int  user_age { get; set; }
        // 信箱驗證碼
        public string user_authcode { get; set; }
        public int user_start { get; set; }
        // 管理者
        public bool user_level { get; set; }
        public string update_id { get; set; }
        public DateTime update_time { get; set; }





    }
}
