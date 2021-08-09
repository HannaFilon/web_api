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
        public string Genre { get; set; }
        public AgeRatingEnum? AgeRatig { get; set; }
        public OrderBy? TotalRatingOrder { get; set; }
        public OrderBy? PriceOrder { get; set; }

        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}