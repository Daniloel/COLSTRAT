using Plugin.Iconize;
using System.Collections.Generic;

namespace COLSTRAT.Service.Icons
{
    public class COLSTRATIconsCollection
    {

        public static IList<IIcon> Icons { get; } = new List<IIcon>();

        static COLSTRATIconsCollection()
        {
            Icons.Add("dt-bit", '\uf001');
            Icons.Add("dt-broken-image", '\uf002');
            Icons.Add("dt-clip-attach", '\uf003');
            Icons.Add("dt-exit", '\uf004');
            Icons.Add("dt-fossil", '\uf005');
            Icons.Add("dt-geology", '\uf006');
            Icons.Add("dt-help", '\uf007');
            Icons.Add("dt-home", '\uf008');
            Icons.Add("dt-navigation-before", '\uf009');
            Icons.Add("dt-no-internet", '\uf010');
            Icons.Add("dt-oil-price", '\uf011');
            Icons.Add("dt-oil-tint", '\uf012');
            Icons.Add("dt-person-pin", '\uf013');
            Icons.Add("dt-pump-oil-2", '\uf014');
            Icons.Add("dt-pump-oil-3", '\uf015');
            Icons.Add("dt-pump", '\uf016');
            Icons.Add("dt-reference", '\uf017');
            Icons.Add("dt-refinery", '\uf018');
            Icons.Add("dt-refresh", '\uf019');
            Icons.Add("dt-reload", '\uf020');
            Icons.Add("dt-rig-1", '\uf021');
            Icons.Add("dt-rig-2", '\uf022');
            Icons.Add("dt-rig-3", '\uf023');
            Icons.Add("dt-rig", '\uf024');
            Icons.Add("dt-valvule-1", '\uf025');
            Icons.Add("dt-valvule-2", '\uf026');
            Icons.Add("dt-valvule-3", '\uf027');
            Icons.Add("dt-valvule-4", '\uf028');
            Icons.Add("dt-chevron-right", '\uf029');
            Icons.Add("dt-person", '\uf030');
        }
    }
}
