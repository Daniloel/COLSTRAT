using Plugin.Iconize;
using System.Collections.Generic;

namespace COLSTRAT.Service.Icons
{
    public class COLSTRATIconsCollection
    {

        public static IList<IIcon> Icons { get; } = new List<IIcon>();

        static COLSTRATIconsCollection()
        {
            Icons.Add("icon-plus-squared-alt", '\uf196');
            Icons.Add("icon-left-open", '\uf801');
            Icons.Add("icon-off", '\uf805');
            Icons.Add("icon-history", '\uf1da');
            Icons.Add("icon-right-open", '\uf800');

            Icons.Add("icon-down-open", '\uf802');
            Icons.Add("icon-cancel-circle2", '\uf806');
            Icons.Add("icon-wifi", '\uf1eb');
            Icons.Add("icon-up-open", '\uf803');
            Icons.Add("icon-arrows-cw", '\uf804');
        }
    }
}
