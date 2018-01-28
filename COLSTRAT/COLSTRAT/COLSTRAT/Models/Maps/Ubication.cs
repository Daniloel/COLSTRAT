using SQLite;

namespace COLSTRAT.Models.Maps
{
    public class Ubication
    {
        #region Properties
        [PrimaryKey]
        public int UbicationId { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; } 
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return UbicationId;
        }
        #endregion
    }
}
