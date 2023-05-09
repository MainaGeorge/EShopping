namespace Catalog.Core.Specifications
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, long count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
        public Pagination()
        {
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public long Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
