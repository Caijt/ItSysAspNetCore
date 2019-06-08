

namespace ItSys.Dto
{
    public abstract class QueryDto : IQueryDto
    {
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int noPage { get; set; }
        public string sortProp { get; set; }
        public string sortOrder { get; set; }
        public string orderProp { get; set; }
        public bool? orderDesc { get; set; }

        public bool IsPaged()
        {
            return noPage == 0 && (currentPage != 0 && pageSize != 0);
        }
    }
}
