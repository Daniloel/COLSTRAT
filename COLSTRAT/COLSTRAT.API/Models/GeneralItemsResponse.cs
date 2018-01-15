namespace COLSTRAT.API.Models
{
    using COLSTRAT.Domain.Menu.Entity.Generic;
    public class GeneralItemsResponse : GeneralItem
    {
        public byte[] ImageArray { get; set; }
    }
}