namespace COLSTRAT.API.Models
{
    using System.Collections.Generic;
    public class CategoryRockResponse
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<TypeOfRockResponse> TypeOfRocks { get; set; }
    }
}