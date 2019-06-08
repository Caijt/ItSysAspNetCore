
namespace ItSys.Dto
{
    public interface IQueryDto
    {
        int pageSize { get; set; }
        int currentPage { get; set; }
        int noPage { get; set; }
        //string sortProp { get; set; }
        //string sortOrder { get; set; }
        string orderProp { get; set; }
        bool? orderDesc { get; set; }

        bool IsPaged();
    }
}
