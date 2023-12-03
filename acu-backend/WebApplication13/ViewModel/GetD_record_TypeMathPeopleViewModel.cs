using WebApplication13.Model;
using WebApplication13.Service;

namespace WebApplication13.ViewModel
{
    public class GetD_record_TypeMathPeopleViewModel
    {
        //類型(0=無症狀/1=肝腎陰虛型/2=肺陰不足型/3=脾胃溼熱型/4=脾肺伏熱型/5=肝氣鬱結型/6=脾肺虛寒型
        public int None { get; set; }
        public int Liver_kidney_yin_deficiency { get; set; }
        public int Insufficient_lung_yin { get; set; }
        public int Dampness_heat_spleen_stomach { get; set; }
        public int Spleen_lung_latent_heat { get; set; }
        public int Liver_Qi_stagnation { get; set; }
        public int Deficiency_coldness_lungs { get; set; }
    }
}
