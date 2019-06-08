
namespace ItSys.Dto
{
    public class SysCompanyQueryDto : QueryDto
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public bool inUserCompany { get; set; }
    }
}
