namespace CanddelsBackEnd.Helper
{
    public class ProductParameters
    {
        public int PageIndex { get; set; } = 1;
        public int MaxPageSize = 20;
        public int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<string> Scents { get; set; } = new List<string>();


    }
}
