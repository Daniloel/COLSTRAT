using COLSTRAT.Domain.Menu.Entity.Geology.Rocks;

namespace COLSTRAT.API.Models
{
    public class RockResponse : Rock
    {
        public byte[] ImageArray { get; set; }
    }
}