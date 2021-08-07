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
    }
}