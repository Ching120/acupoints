using System.ComponentModel.DataAnnotations;

namespace WebApplication13.ViewModel
{
    public class GetAP_InformationViewModel

    {

        public string acupuncture_points_name { get; set; }//穴道名稱
        public string acupuncture_points_location { get; set; }//穴道位置
        public string acupuncture_points_press { get; set; }//按壓方式
        public string acupuncture_points_detail { get; set; }//穴名介紹
        public string acupuncture_points_content { get; set; }//穴道介紹
        public string update_id { get; set; }
        public DateTime update_time { get; set; }

    }
}
