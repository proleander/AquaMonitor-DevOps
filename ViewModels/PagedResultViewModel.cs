namespace AquaMonitor.Api.ViewModels
{
    public class PagedResultViewModel<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; } = new();
    }
}
