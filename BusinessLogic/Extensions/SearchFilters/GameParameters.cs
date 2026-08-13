namespace BusinessLogic.Extensions.SearchFilters
{
    public record GameParameters
    {
        public uint? MinPrice { get; set; } = 0;
        public uint? MaxPrice { get; set; } = int.MaxValue;
        public string? SearchTerm { get; set; }

        // TODO: Add sortby
        //public string? SortBy { get; set; }
        //public bool SortDescending { get; set; } = false;

        public List<int>? TagIds { get; set; }
        public bool? OnSaleOnly { get; set; } = false;
    }
}
