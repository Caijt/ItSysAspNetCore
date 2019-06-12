
namespace ItSys.Dto
{
    public interface IQueryDto
    {
        /// <summary>
        /// 每页数量
        /// </summary>
        int pageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        int currentPage { get; set; }
        int noPage { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        string orderProp { get; set; }
        /// <summary>
        /// 是否倒序排序
        /// </summary>
        bool? orderDesc { get; set; }
        bool IsPaged();
    }
}
