using WebApplication13.Model;
using WebApplication13.Service;

namespace WebApplication13.ViewModel
{
    public class GetEye_questionViewModel
    {

        public int Id { get; set; }
        public Guid eye_question_id { get; set; }
        public string eye_question_content { get; set; }
        public string update_id { get; set; }
        public DateTime update_time { get; set; }


    }
}
