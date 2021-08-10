using Shop.Business.ModelsDto;

namespace Shop.Business.Models
{
    public enum OrderBy
    {
        Asc = 0,
        Desc = 1
    }

    public class ParametersList
    {
        private const int MaxPageSize = 20;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public string Genre { get; set; }
        public AgeRatingEnum? AgeRatig { get; set; }
        public OrderBy? TotalRatingOrder { get; set; }
        public OrderBy? PriceOrder { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}